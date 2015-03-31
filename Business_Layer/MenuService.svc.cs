using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Commonlayer;
using Commonlayer.Views;
using DataAccessLayer;

namespace Business_Layer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "MenuService" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select MenuService.svc or MenuService.svc.cs at the Solution Explorer and start debugging.
    public class MenuService : IMenuService
    {
        /// <summary>
        /// Returns the MainMenus according to what a user  can see
        /// </summary>
        /// <param name="username">Passes the username as to check the role and what a user can see in the menu</param>
        /// <returns>Returns MenuItems that a user can see, depending on his role</returns>        
        public IQueryable<MenusView> GetMainMenus1(string email)
        {
            return new MenuRepository().GetMainMenus(email);
        }

        /// <summary>
        /// Returns all the MainMenus, this time depending on the roleID directly
        /// </summary>
        /// <param name="roleID">Passes the roleID to return all the menuItems that the user can see</param>
        /// <returns>Returns MenuItems that a roleID can see.</returns>
        public IQueryable<Menu> GetMainMenu(int roleID)
        {
            return new MenuRepository().GetMainMenu(roleID);
        }

        public IQueryable<MenusView> GetMainMenuV(int roleID)
        {
            return new MenuRepository().GetMainMenuV(roleID);
        }

        /// <summary>
        /// Returns all the SubMenus that match the username to get the roleID and depending on the parentID
        /// </summary>
        /// <param name="username">Gives username to get the roleID and returns menuItems according to this</param>
        /// <param name="parentID">Passes the parentID as to match all the SubMenus to their Main Menu Items according to the database</param>
        /// <returns>Returns a list of MenuItems</returns>
        public IQueryable<Menu> GetSubMenus1(string email, int parentID)
        {
            return new MenuRepository().GetSubMenus(email, parentID);
        }

        /// <summary>
        /// Returns all the SubMenus that match the roleID and depends on the ParentID
        /// </summary>
        /// <param name="roleID">The roleID to get the menuItems according to the MenuRoles table</param>
        /// <param name="parentID">Passes the parentID as to match all the SubMenus to their Main Menu Items according to the database</param>
        /// <returns>Returns a list of MenuItems</returns>
        public IQueryable<Menu> GetSubMenus2(int roleID, int parentID)
        {
            return new MenuRepository().GetSubMenu(roleID, parentID);
        }

    }
}
