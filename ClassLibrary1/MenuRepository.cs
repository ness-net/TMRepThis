using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Commonlayer;
using Commonlayer.Views;

namespace DataAccessLayer
{
    public class MenuRepository : ConnectionClass
    {
        public MenuRepository()
            : base()
        {

        }

        public IQueryable<Menu> GetMainMenu(int roleID)
        {
            var list = (
                 from r in Entity.Roles
                 from m in r.Menus
                 where m.ParentID == null && r.RoleID == roleID
                 select m
                 ).Distinct();

            return list.AsQueryable();
        }

        public IQueryable<MenusView> GetMainMenuV(int roleID)
        {
            var list =  (from r in Entity.Roles
                     from m in r.Menus
                    where m.ParentID == null && r.RoleID == roleID
                    select new MenusView
                    {
                       Title = m.Title,
                       URL = m.URL
                    }
                    ).Distinct();

            return list.AsQueryable();
        }

        public IQueryable<Menu> GetSubMenu(int roleID, int parentID)
        {

            var list = (

              from r in Entity.Roles
              from m in r.Menus
              where m.ParentID == parentID && r.RoleID == roleID
              select m

              ).Distinct();

            return list.AsQueryable();

        }

        //public IQueryable<Menu> GetMainMenus(string username)
        //{
        //    return (
        //        from u in Entity.Users
        //        from r in u.Roles
        //        from m in r.Menus
        //        where u.Username == username && m.ParentID == null
        //        orderby m.Position
        //        select m
        //    ).Distinct();

        //}

        public IQueryable<MenusView> GetMainMenus(string username)
        {
            var list = (from u in Entity.Users
                        from r in u.Roles
                        from m in r.Menus
                        where u.Username == username && m.ParentID == null
                        orderby m.Position
                        select new MenusView
                        {
                            Title = m.Title,
                            URL = m.URL,
                            Position = m.Position
                        }
                    ).Distinct().OrderBy(u => u.Position);

            return list.AsQueryable();
        }

        public IQueryable<Menu> GetSubMenus(string username, int parentID)
        {
            return (
                from u in Entity.Users
                from r in u.Roles
                from m in r.Menus
                where u.Username == username && m.ParentID == parentID
                orderby m.Position
                select m
                ).Distinct();
        }
    }
}

