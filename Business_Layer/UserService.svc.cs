using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Commonlayer;
using DataAccessLayer;
using System.Data.Entity;
using Commonlayer.Views;
using System.Security.Cryptography;

namespace Business_Layer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UserService.svc or UserService.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUserService
    {

        public bool isAuthenticationValid(string email, string password)
        {
            return new UserRepository().IsAuthenticationValid(email, HashPassword(password, "HMACSHA256"));
        }


        public void AddUser(string username, string password, string email, string name,
                           string surname, long contactno, bool buyer, bool seller)
        {
            UserRepository ur = new UserRepository();
            RoleRepository rr = new RoleRepository();
            ur.Entity = rr.Entity;


            if (ur.DoesUsernameExist(username) == false)
            {
                if (ur.DoesEmailExist(email) == false)
                {             
                        User u = new User();
                        u.Username = username;
                        u.Password = HashPassword(password, "HMACSHA256");
                        u.Name = name;
                        u.Surname = surname;
                        u.Email = email; 
                        u.ContactNo = contactno.ToString();

                        RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
                        u.PrivateKey = rsa.ToXmlString(true);  // Private Key
                        u.PublicKey = rsa.ToXmlString(false);  // Public Key                 

                        try
                        {
                            ur.Entity.Database.Connection.Open();
                            ur.AddUser(u);
                            if (buyer == false && seller == true)
                            {
                                rr.AllocateRole(u, rr.GetRole(3));
                            }
                            else if (buyer == true && seller == false)
                            {
                                rr.AllocateRole(u, rr.GetRole(4));
                            }
                            else if (buyer == true && seller == true)
                            {
                                rr.AllocateRole(u, rr.GetRole(4));
                                rr.AllocateRole(u, rr.GetRole(3));
                            }                            

                        }
                        catch (Exception ex)
                        {                  
                            throw new Exception("Error Occurred, please try later"+ex.Message);
                        }
                        finally
                        {
                            ur.Entity.Database.Connection.Close();
                        }
                }
                else throw new FaultException("Email already exists");
            }
            else throw new FaultException("Username already exists");
        }

        public IEnumerable<User> GetAllUsers()
        {
            return new UserRepository().GetAllUsers();
        }

        public string GetPrivateKey(string email)
        {
            return new UserRepository().GetPrivateKey(email);
        }

        public string GetPublicKey(string email)
        {
            return new UserRepository().GetPublicKey(email);
        }

        /// <summary>
        /// Checks if the User with the username already exists
        /// </summary>
        /// <param name="username">Passes the username to check</param>
        /// <returns>Returns true if it exists and false if it doesnt</returns>
        public bool DoesUsernameExist(string username)
        {
            return new UserRepository().DoesUsernameExist(username);
        }

        /// <summary>
        /// Checks if the email passed exists in the database
        /// </summary>
        /// <param name="email">Passes the email to check if it exists</param>
        /// <returns>Returns true if it exists and false if it doesnt</returns>
        public bool DoesEmailExist(string email)
        {
            return new UserRepository().DoesEmailExist(email);
        }


        /// <summary>
        /// Gets the roles that the username has.
        /// </summary>
        /// <param name="username">Passes the username to check the roles.</param>
        /// <returns>Returns a list of roles</returns>
        //public IQueryable<Role> GetUserRoles(string username)
        //{
        //    return new RoleRepository().GetUserRoles(username);
        //}

        /// <summary>
        /// Gets the user according to the username that is passed
        /// </summary>
        /// <param name="username">Pass the username to match it with the user</param>
        /// <returns>Returns the single user that matches or null if noone matches</returns>
        public User GetUser(string email)
        {
            return new UserRepository().GetUser(email);
        }

        public void UpdateUser(string username, string email, string name,
                           string surname, long contactno, bool buyer, bool seller)
        {
            try
            {
                User u = GetUser(email);
                u.Username = username;
                u.Name = name;
                u.Surname = surname;
                u.ContactNo = contactno.ToString();

                UserRepository ur = new UserRepository();
                RoleRepository rr = new RoleRepository();
                ur.Entity = rr.Entity;
                ur.UpdateUser(u);

                
                bool prevbuyer = false;
                bool prevseller = false;
                List<Commonlayer.Views.RolesView> roles = rr.GetUserRolesV(u.Email).ToList();
                foreach (Commonlayer.Views.RolesView rol in roles)
                {
                    if (rol.ID == 3)
                    {
                        prevbuyer = true;
                    }
                    else if (rol.ID == 4)
                    {
                        prevseller = true;
                    }
                }


                if (prevbuyer != buyer)
                {
                    if (buyer == true)
                    {
                        rr.AllocateRole(u, rr.GetRole(3));
                    }
                    else if (buyer == false)
                    {
                        rr.RemoveRole(u, rr.GetRole(3));
                    }
                }

                if (prevseller != seller)
                {
                    if (seller == true)
                    {
                        rr.AllocateRole(u, rr.GetRole(4));
                    }
                    else if (seller == false)
                    {
                        rr.RemoveRole(u, rr.GetRole(4));
                    }
                }

            }
            catch(Exception x)
            {
                string ex = x.Message;
                
            }
        }


       

        public IQueryable<Role> GetUserRoles(string email)
        {
            return new RoleRepository().GetUserRoles(email);
        }

        public IQueryable<Commonlayer.Views.CreditCardView> GetCreditCards(string username)
        {
            return new UserRepository().GetCreditCards(username);
        }

        //public string GetUserPassword(string username)
        //{
        //    return new UserRepository().GetUserPassword(username);
        //}

        public IQueryable<RolesView> GetUserRolesV(string email)
        {
            return new RoleRepository().GetUserRolesV(email);
        }

        //public IQueryable<CommissionType> GetCommissionTypes()
        //{
        //    return new UserRepository().GetCommissions();
        //}

        public void AddCreditCard(string username, string creditcardt, string cvv, string holder, decimal number)
        {
            CreditCardRepository cr = new CreditCardRepository();
            CreditCard c = new CreditCard();
            c.CVV = cvv;
            c.CardNumber = number;
            c.CardOwner = holder;
            c.CardType = creditcardt;
            c.Email = username;



                    try
                    {
                        cr.Entity.Database.Connection.Open();
                        cr.AddCreditCard(c);     
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error Occurred, please try later" + ex.Message);
                    }
                    finally
                    {
                        cr.Entity.Database.Connection.Close();
                    }
               
        }

        public string EncodeBase64(string data)
        {
            string s = data.Trim().Replace(" ", "+");
            if (s.Length % 4 > 0)
                s = s.PadRight(s.Length + 4 - s.Length % 4, '=');
            return Encoding.UTF8.GetString(Convert.FromBase64String(s));
        }

        private string HashPassword(string password, string hashingAlgorithm = "HMACSHA256")
        {
            byte[] passwordBytes = Encoding.Unicode.GetBytes(password);
            byte[] saltBytes = Convert.FromBase64String("AbAcAdAe");

            var saltyPasswordBytes = new byte[saltBytes.Length + passwordBytes.Length];

            Buffer.BlockCopy(saltBytes, 0, saltyPasswordBytes, 0, saltBytes.Length);
            Buffer.BlockCopy(passwordBytes, 0, saltyPasswordBytes, saltBytes.Length, passwordBytes.Length);

            switch (hashingAlgorithm)
            {
                case "HMACSHA256":
                    return Convert.ToBase64String(new HMACSHA256(saltBytes).ComputeHash(saltyPasswordBytes));
                default:
                    // Supported types include: SHA1, MD5, SHA256, SHA384, SHA512
                    HashAlgorithm algorithm = HashAlgorithm.Create(hashingAlgorithm);

                    if (algorithm != null)
                    {
                        return Convert.ToBase64String(algorithm.ComputeHash(saltyPasswordBytes));
                    }

                    throw new CryptographicException("Unknown hash algorithm");
            }
        } 
    }
}
