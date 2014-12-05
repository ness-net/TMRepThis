using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commonlayer;
using Commonlayer.Views;

namespace DataAccessLayer
{
    public class RoleRepository: ConnectionClass
    {
        public RoleRepository() : base() { }

        public void AllocateRole(User user, Role role)
        {
            user.Roles.Add(role);
            Entity.SaveChanges();

        }

        public Role GetDefaultRole()
        {
            return Entity.Roles.SingleOrDefault(r => r.RoleID == 1);
        }

        public IQueryable<Role> GetUserRoles(string username)
        {
            UserRepository ur = new UserRepository();
            User u = ur.GetUser(username);
            return u.Roles.AsQueryable();
        }

        public IQueryable<RolesView> GetUserRolesV(string username)
        {
            UserRepository ur = new UserRepository();
            User u = ur.GetUser(username);
            List<RolesView> list = new List<RolesView>();

            foreach (Role r in u.Roles)
            {
               RolesView rv = new RolesView();
               rv.ID = r.RoleID;
               rv.Name = r.Role1;
            }
            return list.AsQueryable();
        }

       

    }
}
