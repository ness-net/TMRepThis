using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commonlayer;

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

    }
}
