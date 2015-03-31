using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Commonlayer;
using Commonlayer.Views;

namespace Business_Layer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMenuService" in both code and config file together.
    [ServiceContract]
    public interface IMenuService
    {
        [OperationContract]
        IQueryable<MenusView> GetMainMenus1(string email);
        [OperationContract]
        IQueryable<Menu> GetMainMenu(int roleID);
        [OperationContract]
        IQueryable<Menu> GetSubMenus1(string email, int parentID);
        [OperationContract]
        IQueryable<Menu> GetSubMenus2(int roleID, int parentID);
        [OperationContract]
        IQueryable<MenusView> GetMainMenuV(int roleID);
    }
}
