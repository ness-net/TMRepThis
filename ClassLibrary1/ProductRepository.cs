using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commonlayer;
using Commonlayer.Views;

namespace DataAccessLayer
{
    public class ProductRepository : ConnectionClass
    {
        public ProductRepository() : base() { }

        public IQueryable<ProductView> GetProducts()
        {            
            var list = (from p in Entity.Products
                        where p.Name != ""
                        select new ProductView
                        {
                            ProductID = p.ProductID,
                            Name = p.Name,
                            Description = p.Description,
                            CategoryID = p.CategoryID,
                            ImageLink = p.ImageLink,
                            Price = p.Price,
                            Username = p.Username,
                            Stock= p.Stock
                        }
                    ).Distinct();

           return list.AsQueryable();
        
        }

        public IEnumerable<Product> GetProducts1()
        {
            return Entity.Products.AsEnumerable();
        }
      

        public IEnumerable<ProductView> GetProductsList()
        {
            return (from p in Entity.Products
                    where p.Stock > 0
                    select new ProductView
                    {
                        ProductID = p.ProductID,
                        Name = p.Name,
                        Description = p.Description,
                        CategoryID = p.CategoryID,
                        ImageLink = p.ImageLink,
                        Price = p.Price,
                        Username = p.Username,
                        Stock = p.Stock
                    }).ToList();
        }

        public ProductView GetProductV(int id)
        {
            return (from p in Entity.Products
                    where p.ProductID == id
                    select new ProductView
                    {
                        ProductID = p.ProductID,
                        Name = p.Name,
                        Description = p.Description,
                        CategoryID = p.CategoryID,
                        ImageLink = p.ImageLink,
                        Price = p.Price,
                        Username = p.Username,
                        Stock = p.Stock
                    }).FirstOrDefault();
        }

        public Product GetProduct(int id)
        {
            return Entity.Products.SingleOrDefault(x => x.ProductID == id);
        }

        public int GetProductID(string name)
        {
            return (from p in Entity.Products
                    where p.Name == name
                    select p.ProductID
                     ).SingleOrDefault();
        }

        public IQueryable<ProductView> GetProductsAccordingToSubCategory(System.Nullable<int> id)
        {
            var list = (from p in Entity.Products
                        where (p.CategoryID == id) && (p.Stock > 0)
                        select new ProductView
                       {
                           ProductID = p.ProductID,
                           Name = p.Name,
                           Description = p.Description,
                           CategoryID = p.CategoryID,
                           SubCategoryID = Convert.ToInt16(p.SubCategoryID),
                           ImageLink = p.ImageLink,
                           Price = p.Price,
                           Username = p.Username,
                           Stock = Convert.ToInt16(p.Stock)
                       });

            return list.AsQueryable();
        }

        public void ControlStock(int productid, int stock)
        {
            Product ps = GetProduct(productid);
            ps.Stock += stock;
            Entity.SaveChanges();
        }
        


        public bool CheckStock(int productid, int stock)
        {
            Product c = GetProduct(productid);

            if ((c.Stock - stock) <= 0)
            {
                return false;
            }
            else
            {
                return true;
            }

        }

        public int GetStock(int productID)
        {
            var thisc =  (from p in Entity.Products
                    where p.ProductID == productID
                    select p.Stock
                    ).SingleOrDefault();

            return Convert.ToInt16(thisc);

        }

    }
}
