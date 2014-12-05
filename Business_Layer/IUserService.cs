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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IUserService" in both code and config file together.
    [ServiceContract]
    public interface IUserService
    {
        [OperationContract]
        bool isAuthenticationValid(string username, string password);
        [OperationContract]
        void AddUser(string username, string password, string email, string name,
                           string surname, string postcode, string town, long contactno, string residence, string street,
                            string countrid, bool handlesdeliver, long accountnumber, string commission);
        [OperationContract]
        bool DoesUsernameExist(string username);
        [OperationContract]
        bool DoesEmailExist(string email);
        [OperationContract]
        User GetUser(string username);
        [OperationContract]
        IEnumerable<User> GetAllUsers();
        

        [OperationContract]
        IQueryable<Role> GetUserRoles(string username);
        [OperationContract]
        IQueryable<RolesView> GetUserRolesV(string username);
        [OperationContract]
        void AddCreditCard(string username, string creditcardt, string cvv, string holder, decimal number);
        
    }
}
