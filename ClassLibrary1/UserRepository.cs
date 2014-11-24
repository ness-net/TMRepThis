using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commonlayer;
using System.Data.Entity;
using System.Data.Objects;

namespace DataAccessLayer
{
    public class UserRepository : ConnectionClass
    {



        public UserRepository() : base() {}
                

        public void AddUser(User myNewUser)
        {
            Entity.Users.Add(myNewUser);
            Entity.SaveChanges();
        }

        public User GetUser(string username)
        {
            return Entity.Users.SingleOrDefault(u => u.Username == username);
        }

        public bool DoesUsernameExist(string username)
        {
            if (GetUser(username) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public User GetEmail(string email)
        {
            return Entity.Users.SingleOrDefault(u => u.Email == email);

        }

        public bool DoesEmailExist(string email)
        {
            if (GetEmail(email) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool IsAuthenticationValid(string username, string password)
        {
            User u = GetUser(username.ToLower());
            if (u != null)
            {
                if (password != "Invalid")
                {
                    if ((u.Username.ToLower() == username.ToLower()) && (u.Password == password))
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }      



    }
}
