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
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IProdService" in both code and config file together.
    [ServiceContract]
    public interface IProdService
    {
        [OperationContract]
        IQueryable<ProductView> GetProducts();
        [OperationContract]
        IEnumerable<Product> GetProducts1();

        [OperationContract]
        Product GetProduct(int id);


        [OperationContract]
        IQueryable<ProductView> GetProductsAccordingToSubCategory(System.Nullable<int> CatID);

        [OperationContract]
        void ControlStock(int productid, int stock);

        [OperationContract]
        ProductView GetProductV(int id);

        [OperationContract]
        int GetProductID(string name);


        [OperationContract]
        bool CheckStock(int productid, int stock);

        [OperationContract]
        int GetStock(int productID);

        [OperationContract]
        IEnumerable<ProductView> GetProductsList();
    }
}
