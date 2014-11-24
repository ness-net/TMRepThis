using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Commonlayer;
using DataAccessLayer;
using System.Data.Entity;

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


        public void AddUser(string username, string password, string email, string name,
                           string surname, string postcode, string town, long contactno, string residence, string street,
                            string countrid, bool handlesdeliver, long accountnumber)
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
                        u.Password = password;
                        u.Name = name;
                        u.Surname = surname;
                        u.Email = email; 
                        u.PostCode = postcode; 
                        u.Residence = residence;
                        u.Town=town;
                        u.Country = countrid;
                        u.Street = street; 
                        u.ContactNo = contactno; 
                        u.HandlesDeliveres = handlesdeliver; 
                        u.AccountNumber = accountnumber;


                        try
                        {
                            //ur.Entity.Connection.Open();
                            //rr.Transaction = ur.Transaction = ur.Entity.Connection.BeginTransaction();
                            ur.AddUser(u);

                            rr.AllocateRole(u, rr.GetDefaultRole());
                            ur.Transaction.Commit();
                        }
                        catch
                        {
                            ur.Transaction.Rollback();
                            rr.Transaction.Rollback();
                            throw new Exception("Error Occurred, please try later");
                        }
                        finally
                        {
                            //ur.Entity.Connection.Close();
                        }
                }
                else throw new FaultException("Email already exists");
            }
            else throw new FaultException("Username already exists");
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

        public IQueryable<Role> GetUserRoles(string username)
        {
            return new RoleRepository().GetUserRoles(username);
        }
    }
}
