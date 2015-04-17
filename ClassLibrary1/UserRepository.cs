using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commonlayer;
using System.Data.Entity;
using System.Data.Objects;
using Commonlayer.Views;
using System.Data.Entity.Infrastructure;

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

        public User GetUser(string email)
        {
            //return Entity.Users.SingleOrDefault(u => u.Username == username);
            return Entity.Users.AsNoTracking().SingleOrDefault(u => u.Email == email);
        }

        public string GetPrivateKey(string email)
        {
            User u = Entity.Users.SingleOrDefault(e => e.Email == email);
            return u.PrivateKey;
        }

        //public UsersView GetUserByUsername(string username)
        //{
        //    User u = Entity.Users.SingleOrDefault(e => e.Username == username);
        //    UsersView usv = new UsersView
        //    return u;
        //}

        public string GetPublicKey(string email)
        {
            User u = Entity.Users.SingleOrDefault(e => e.Email == email);
            return u.PublicKey;
        }

        //public string GetUserPassword(string email)
        //{
        //    var list = (from u in Entity.Users
        //                where u.Username == username
        //                select u
        //            ).FirstOrDefault();

        //    return list.Password;
        //}

        //public void AllocateUser(User user, Commission comm)
        //{
            
        //   // comm.Users.
        //    comm.Users.Add(user);
        //    //user.Commissions.Add(comm);
        //    Entity.SaveChanges();

        //}

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

        public void UpdateUser(User usertoUpdate)
        {
            try
            {
                Entity.Entry(usertoUpdate).State = EntityState.Modified;
                Entity.SaveChanges();
                //User originalUser = GetUser(usertoUpdate.Email);
                //Entity.Users.Attach(usertoUpdate);
                //((IObjectContextAdapter)Entity).ObjectContext.ApplyCurrentValues("Users", originalUser);
                //Entity.SaveChanges();
            } catch(Exception ex)
            {
                string meth = ex.Message;
            }
        }

        public bool IsAuthenticationValid(string email, string password)
        {
            User u = GetUser(email.ToLower());
            if (u != null)
            {
                if (password != "Invalid")
                {
                    if ((u.Email.ToLower() == email.ToLower()) && (u.Password == password))
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



        //public IQueryable<CommissionType> GetCommissions()
        //{
        //    var list = (from c in Entity.Commissions
        //                orderby c.CommissionID
        //                select new CommissionType
        //                {
        //                    Title = c.TypeOFCommission,
        //                    fee = c.Amount
        //                }
        //            ).Distinct();

        //    return list.AsQueryable();
        //}

        //public IQueryable<CreditCardView> GetCreditCards(string email)
        //{
        //    var list = (from c in Entity.CreditCards
        //                where c.Email == email
        //                select new CreditCardView
        //                {
        //                    CardOwner = c.CardOwner,
        //                    CardT = c.CardType,
        //                    CVV = c.CVV,
        //                    Number = c.CardNumber,
        //                    email = c.Email
        //                }
        //            ).Distinct();

        //    return list.AsQueryable();
        //}



    }
}
