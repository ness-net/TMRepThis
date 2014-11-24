﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Commonlayer;

namespace Business_Layer
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IMenuService" in both code and config file together.
    [ServiceContract]
    public interface IMenuService
    {
        [OperationContract]
        IQueryable<Menu> GetMainMenus1(string username);
        [OperationContract]
        IQueryable<Menu> GetMainMenus2(int roleID);
        [OperationContract]
        IQueryable<Menu> GetSubMenus1(string username, int parentID);
        [OperationContract]
        IQueryable<Menu> GetSubMenus2(int roleID, int parentID);
    }
}