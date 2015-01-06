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
    public class RoleRepository: ConnectionClass
    {
        public RoleRepository() : base() { }

        public void AllocateRole(User user, Role role)
        {
            user.Roles.Add(role);
            Entity.SaveChanges();

        }

        public IEnumerable<Role> GetAllRoles()
        {
            return Entity.Roles.AsEnumerable();
        }

        public IEnumerable<RolesView> GetAllRolesV()
        {
            return (from p in Entity.Roles
                    select new RolesView
                    {
                        ID = p.RoleID,
                        Name = p.Role1
                    }).ToList();
        
        }

        public IEnumerable<RolesView> GetMatchingRoles(string roles)
        {
            return (from r in Entity.Roles
                    where r.Role1.StartsWith(roles)
                    select new RolesView()
                        {                        
                                ID = r.RoleID,
                                Name = r.Role1
                        }
                    ); 

        }

        public void UpdateRole(Role RToUpdate)
        {
            try
            {
                Role originalRole = GetRole(RToUpdate.RoleID);
                Entity.Roles.Attach(originalRole);
                ((IObjectContextAdapter)Entity).ObjectContext.ApplyCurrentValues("Roles", RToUpdate);
                Entity.SaveChanges();
            } catch (NullReferenceException Exception){

                throw new NullReferenceException();
            }
            catch (Exception ex)
            {

            }
        }

        public void AddRole(Role newRole)
        {
            try
            {
                if (newRole.Role1 != null)
                {
                    Entity.Roles.Add(newRole);
                    Entity.SaveChanges();
                }
                else
                {

                }
            } catch (Exception ex)
            {

            }
        }

        public Role GetRole(int role)
        {
            return Entity.Roles.SingleOrDefault(r => r.RoleID == role);
        }

        public Role GetRoles(string roleN)
        {
            return Entity.Roles.SingleOrDefault(r => r.Role1 == roleN);
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
               list.Add(rv);
            }
            return list.AsQueryable();
        }

        public RolesView GetRoleV(int id)
        {
            return (from p in Entity.Roles
                    where p.RoleID == id
                    select new RolesView
                    {
                        ID = p.RoleID,
                        Name = p.Role1
                    }).SingleOrDefault(); 
        }

        public void DeleteRole(int id)
        {
            try
            {
                Role role = GetRole(id);
                Entity.Roles.Remove(role);
                Entity.SaveChanges();
            }
            catch (Exception ex)
            {

            }
        }
       

    }
}
