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
        void AddProducttoCart(string email, int productId);

        [OperationContract]
        IQueryable<ShoppingCartView> GetProductsinShoppingCart(string email);

       
        [OperationContract]
        void DeleteShoppingCartEntry(string email, int productid);

        [OperationContract]
        ProductView GetProductV(int id);

        [OperationContract]
        int GetProductID(string name);
           

        [OperationContract]
        IEnumerable<ProductView> GetProductsList();

        [OperationContract]
        string GetProductImageLi(int ProdID);

        [OperationContract]
        void AddProduct(string name, string desc, int catid, string imageLink, decimal price);
        
        [OperationContract]
        void DeleteProduct(int productID);
        
        [OperationContract]
        void UpdateProduct(int prodid, string name, string desc, int catid, string imageLink, decimal price);
        
        [OperationContract]
        IQueryable<ProductView> GetProductsAccordingToSeller(string email);

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
