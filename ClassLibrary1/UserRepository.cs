using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commonlayer;
using System.Data.Entity;
using System.Data.Objects;
using Commonlayer.Views;

namespace DataAccessLayer
{
    public class UserRepository : ConnectionClass
    {



        public UserRepository() : base() {}


        public IEnumerable<User> GetAllUsers()
        {
            return Entity.Users.AsEnumerable();
        }


        public void AddUser(User myNewUser)
        {
            Entity.Users.Add(myNewUser);
            Entity.SaveChanges();
        }

        public User GetUser(string username)
        {
            return Entity.Users.SingleOrDefault(u => u.Username == username);
        }

        public void AllocateUser(User user, Commission comm)
        {
            
           // comm.Users.
            comm.Users.Add(user);
            //user.Commissions.Add(comm);
            Entity.SaveChanges();

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



        public IQueryable<CommissionType> GetCommissions()
        {
            var list = (from c in Entity.Commissions
                        orderby c.CommissionID
                        select new CommissionType
                        {
                            Title = c.TypeOFCommission,
                            fee = c.Amount
                        }
                    ).Distinct();

            return list.AsQueryable();
        }

        public IQueryable<CreditCardView> GetCreditCards(string username)
        {
            var list = (from c in Entity.CreditCards
                        where c.Username == username
                        select new CreditCardView
                        {
                            CardOwner = c.CardOwner,
                            CardT = c.CardType,
                            CVV = c.CVV,
                            Number = c.CardNumber,
                            username = c.Username
                        }
                    ).Distinct();

            return list.AsQueryable();
        }



    }
}
