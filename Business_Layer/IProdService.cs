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
        Product GetProduct(int id);


        [OperationContract]
        IQueryable<ProductView> GetProductsAccordingToSubCategory(System.Nullable<int> CatID);

        [OperationContract]
        IQueryable<CategoryView> GetCategories();
        

        [OperationContract]
        void ControlStock(int productid, int stock);

        [OperationContract]
        void AddProducttoCart(string username, int productId, int qty);

        [OperationContract]
        IQueryable<ShoppingCartView> GetProductsinShoppingCart(string Username);

        [OperationContract]
        void UpdateCart(string username, int productId, int newQty);

        [OperationContract]
        void DecrementCart(string username, int productId);

        [OperationContract]
        void DeleteShoppingCartEntry(string username, int productid);

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

        [OperationContract]
        void AddProduct(string name, string desc, int catid, string imageLink, decimal price, int stock);
        
        [OperationContract]
        void DeleteProduct(int productID);
        
        [OperationContract]
        void UpdateProduct(int prodid, string name, string desc, int catid, string imageLink, decimal price, int stock);
        
        [OperationContract]
        IQueryable<ProductView> GetProductsAccordingToSeller(string username);

        [OperationContract]
        void MarkActive(int productid);

        [OperationContract]
        IQueryable<CategoryView> getSubCategories();

        [OperationContract]
        IQueryable<CategoryView> getMainCategories();

        [OperationContract]
        String getSubCategoryofProduct(int ProductID);
        
    }
}
