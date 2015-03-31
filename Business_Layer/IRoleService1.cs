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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IRoleService1" in both code and config file together.
    [ServiceContract]
    public interface IRoleService1
    {
        [OperationContract]
        void AddRole(string name);

        [OperationContract]
        Role GetRoles(string roleN);

        [OperationContract]
        void DeleteRole(int id);


        [OperationContract]
        IEnumerable<RolesView> GetMatchingRoles(string roles);

        [OperationContract]
        Role GetRole(int roleID);

        [OperationContract]
        IEnumerable<Role> GetAllRoles();

        [OperationContract]
        void UpdateRole(int roleid, string name);

        [OperationContract]
        IEnumerable<RolesView> GetAllRolesV();

        [OperationContract]
        RolesView GetRoleV(int roleid);

        [OperationContract]
        IQueryable<RolesView> GetUserRolesV(string email);

        [OperationContract]
        void AllocateRole(User user, Role role);

    }
}
