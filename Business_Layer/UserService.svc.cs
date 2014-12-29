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

namespace Business_Layer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "UserService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select UserService.svc or UserService.svc.cs at the Solution Explorer and start debugging.
    public class UserService : IUserService
    {

        public bool isAuthenticationValid(string username, string password)
        {
            return new UserRepository().IsAuthenticationValid(username, password);
        }

        //public void AllocateComm(string username, string comm)
        //{
        //    CommissionRepository cr = new CommissionRepository();
        //    UserRepository ur = new UserRepository();
        //    User u = GetUser(username);
        //    Commission c = new Commission();
        //    if (comm == "FixedFee")
        //    {
        //        c = cr.GetComm(3);
        //    }
        //    if (comm == "Percentage")
        //    {
        //        c = cr.GetComm(2);
        //    }

        //    try
        //    {
        //       cr.Entity.Database.Connection.Open();
        //       cr.AllocateCommission(u, c);
                
        //        //ur.Transaction.Commit();
        //    }
        //    catch (Exception ex)
        //    {

        //        //ur.Transaction.Rollback();
        //        //rr.Transaction.Rollback();
        //        throw new Exception("Error Occurred, please try later" + ex.Message);
        //    }
        //    finally
        //    {
        //        cr.Entity.Database.Connection.Close();
        //    }
        //}




        public void AddUser(string username, string password, string email, string name,
                           string surname, string postcode, string town, long contactno, string residence, string street,
                            string countrid, bool handlesdeliver, long accountnumber, string commission)
        {
            UserRepository ur = new UserRepository();
            RoleRepository rr = new RoleRepository();
            CommissionRepository cr = new CommissionRepository();
            ur.Entity = rr.Entity = cr.Entity;


            if (ur.DoesUsernameExist(username) == false)
            {
                if (ur.DoesEmailExist(email) == false)
                {             
                        User u = new User();
                        u.Username = username;
                        u.Password = password;
                        u.Name = name;
                        u.Surname = surname;
                        u.Email = email; 
                        u.PostCode = postcode; 
                        u.Residence = residence;
                        u.Town=town;
                        u.Country = countrid;
                        u.Street = street; 
                        u.ContactNo = contactno.ToString(); 
                        u.HandlesDeliveres = handlesdeliver; 
                        u.AccountNumber = accountnumber;


                        try
                        {
                            ur.Entity.Database.Connection.Open();
                            //rr.Transaction = ur.Transaction = ur.Entity.Database.Connection.BeginTransaction();
                            ur.AddUser(u);
                            if (u.AccountNumber == 0)
                            {
                                rr.AllocateRole(u, rr.GetRole(4));
                            }
                            else {
                                rr.AllocateRole(u, rr.GetRole(3));
                                }

                            if (commission == "Percentage")
                            {
                                cr.AllocateCommission(u, cr.GetComm(2));
                            }
                            else if (commission == "FixedFee")
                            {
                                cr.AllocateCommission(u, cr.GetComm(3));
                            }
                            else if (commission == "both")
                            {
                                cr.AllocateCommission(u, cr.GetComm(2));
                                cr.AllocateCommission(u, cr.GetComm(3));
                            }

                            //ur.Transaction.Commit();
                        }
                        catch (Exception ex)
                        {                  
          
                            //ur.Transaction.Rollback();
                            //rr.Transaction.Rollback();
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
        public User GetUser(string username)
        {
            return new UserRepository().GetUser(username);
        }

        public Commission GetComm(int commid)
        {
            return new CommissionRepository().GetComm(commid);
        }

        public IQueryable<Role> GetUserRoles(string username)
        {
            return new RoleRepository().GetUserRoles(username);
        }

        public IQueryable<Commonlayer.Views.CreditCardView> GetCreditCards(string username)
        {
            return new UserRepository().GetCreditCards(username);
        }

        public string GetUserPassword(string username)
        {
            return new UserRepository().GetUserPassword(username);
        }

        public IQueryable<RolesView> GetUserRolesV(string username)
        {
            return new RoleRepository().GetUserRolesV(username);
        }

        public IQueryable<CommissionType> GetCommissionTypes()
        {
            return new UserRepository().GetCommissions();
        }

        public void AddCreditCard(string username, string creditcardt, string cvv, string holder, decimal number)
        {
            CreditCardRepository cr = new CreditCardRepository();
            CreditCard c = new CreditCard();
            c.CVV = cvv;
            c.CardNumber = number;
            c.CardOwner = holder;
            c.CardType = creditcardt;
            c.Username = username;
           // c.User



                    try
                    {
                        cr.Entity.Database.Connection.Open();
                        cr.AddCreditCard(c);     
                        

                        //ur.Transaction.Commit();
                    }
                    catch (Exception ex)
                    {

                        //ur.Transaction.Rollback();
                        //rr.Transaction.Rollback();
                        throw new Exception("Error Occurred, please try later" + ex.Message);
                    }
                    finally
                    {
                        cr.Entity.Database.Connection.Close();
                    }
               
        }
    }
}
