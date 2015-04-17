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
        bool isAuthenticationValid(string email, string password);
        [OperationContract]
        void AddUser(string username, string password, string email, string name,
                           string surname, long contactno, bool buyer, bool seller);
        [OperationContract]
        bool DoesUsernameExist(string username);
        [OperationContract]
        bool DoesEmailExist(string email);
        [OperationContract]
        User GetUser(string email);
        [OperationContract]
        IEnumerable<User> GetAllUsers();

        [OperationContract]
        void UpdateUser(string username, string email, string name,
                           string surname, long contactno, bool buyer, bool seller);

        [OperationContract]
        string GetPublicKey(string email);

        //[OperationContract]
        //IQueryable<CreditCardView> GetCreditCards(string email);
        

        [OperationContract]
        IQueryable<Role> GetUserRoles(string email);
        [OperationContract]
        IQueryable<RolesView> GetUserRolesV(string email);
        //[OperationContract]
        //void AddCreditCard(string email, string creditcardt, string cvv, string holder, decimal number);

        [OperationContract]
        string GetPrivateKey(string email);
        
    }
}
