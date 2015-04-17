using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Commonlayer;
using Commonlayer.Views;
using System.Data.Entity.Infrastructure;
using DataAccessLayer.Observer;
using System.Data.Entity;

namespace DataAccessLayer
{
    public class ProductRepository : ConnectionClass
    {
        public ProductRepository() : base() { }

        public IQueryable<ProductView> GetProducts()
        {            
            var list = (from p in Entity.Products
                        where p.isActive == true
                        select new ProductView
                        {
                            ProductID = p.ProductID,
                            Name = p.Name,
                            Description = p.Description,
                            //CategoryID = p.CategoryID,
                            ImageLink = p.ImageLink,
                            Price = p.Price,
                            Email = p.Email,
                            isActive = p.isActive
                        }
                    ).Distinct().Take(10);

           return list.AsQueryable();
        
        }

        public Product LastProduct()
        {
            var list = (from p in Entity.Products
                    select new Product{}).Distinct();

            var myprod = list.OrderBy(item => item.ProductID).Last();
            return myprod;

        }

        public IQueryable<CategoryView> getCategories()
        {
            var list = (from p in Entity.Categories
                        select new CategoryView
                        {
                            CategoryID = p.CategoryID,
                            Name = p.Name,
                            ParentID = p.ParentID
                        }
                  ).Distinct();

            return list.AsQueryable();
        }

        public IQueryable<CategoryView> getSubCategories()
        {
            var list = (from p in Entity.SubCategories
                        select new CategoryView
                        {
                            CategoryID = p.SubCategoryID,
                            Name = p.Name,
                            ParentID = p.ParentCategoryID
                        }
                  ).Distinct();

            return

                list.AsQueryable();
        }

        public String getSubCategoryofProduct(int ProductID)
        {
            return (from p in Entity.Products
                    join ps in Entity.Categories
                    on p.CategoryID equals ps.CategoryID
                    where (p.ProductID == ProductID) && (ps.ParentID != null)
                    select ps.Name).SingleOrDefault();
        }

        public IQueryable<CategoryView> getMainCategories()
        {
            var list = (from p in Entity.Categories
                        select new CategoryView
                        {
                            CategoryID = p.CategoryID,
                            Name = p.Name
                        }
                  ).Distinct();

            return list.AsQueryable();
        }


        public IEnumerable<ProductView> GetProductsList()
        {
            return (from p in Entity.Products
                    where (p.isActive == true)
                    select new ProductView
                    {
                        ProductID = p.ProductID,
                        Name = p.Name,
                        Description = p.Description,
                        CategoryID = p.CategoryID,
                        ImageLink = p.ImageLink,
                        Price = p.Price,
                        Email = p.Email,
                        isActive = p.isActive
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
                        Email = p.Email,
                        isActive = p.isActive
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

        public string GetProductImageLi(int ProdID)
        {
            return (from p in Entity.Products
                    where p.ProductID == ProdID
                    select p.ImageLink
                     ).SingleOrDefault();
        }

        public IQueryable<ProductView> GetProductsAccordingToSubCategory(System.Nullable<int> id)
        {
            var list = (from p in Entity.Products
                        where (p.CategoryID == id) && (p.isActive == true)
                        select new ProductView
                       {
                           ProductID = p.ProductID,
                           Name = p.Name,
                           Description = p.Description,
                           CategoryID = p.CategoryID,
                           ImageLink = p.ImageLink,
                           Price = p.Price,
                           Email = p.Email,
                           isActive = p.isActive
                       });

            return list.AsQueryable();
        }

        public IQueryable<ProductView> GetProductsAccordingToSeller(string email)
        {
            var list = (from p in Entity.Products
                        where p.Email == email
                        select new ProductView
                        {
                            ProductID = p.ProductID,
                            Name = p.Name,
                            Description = p.Description,
                            CategoryID = p.CategoryID,
                            ImageLink = p.ImageLink,
                            Price = p.Price,
                            Email = p.Email,
                            isActive = p.isActive
                        });

            return list.AsQueryable();
        }

        //public void ControlStock(int productid, int stock)
        //{
        //    Product ps = GetProduct(productid);
        //    ps.Stock += stock;
        //    Entity.SaveChanges();
        //    int currentstock = GetStock(productid);
        //    User u = new UserRepository().GetUser(ps.Username);
        //        Subject subject = new Subject();
        //        ObserverC observer1 = new ObserverC(ps.Name, u.Email);
        //        subject.Subscribe(observer1);
        //        subject.CurrentS = currentstock;
        //        subject.Stock = currentstock;
        //}
        


        //public bool CheckStock(int productid, int stock)
        //{
        //    Product c = GetProduct(productid);

        //    if ((c.Stock - stock) <= 0)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }

        //}

        //public int GetStock(int productID)
        //{
        //    var thisc =  (from p in Entity.Products
        //            where p.ProductID == productID
        //            select p.Stock
        //            ).SingleOrDefault();

        //    return Convert.ToInt16(thisc);

        //}

        public void AddToCart(Cart cart)
        {
            Entity.Carts.Add(cart);
            Entity.SaveChanges();
        }

        public Cart GetShoppingCart(string email, int productId)
        {
            return Entity.Carts.SingleOrDefault(x => x.Email == email && x.ProductID == productId);
        }

        public void UpdateCart(string email, string paykey)
        {
            IQueryable<Cart> carts = GetCarts(email);
            foreach (Cart sc in carts)
            {
                try
                {
                    TradersMarketplacedbEntities db = new TradersMarketplacedbEntities();
                    db.Carts.Single(x => x.Email == email && x.ProductID == sc.ProductID).datepurchased = DateTime.Now;
                    db.SaveChanges();
                }catch(Exception x)
                {
                    string message = x.Message;
                }
            }
        }

        //public void DecrementCart(string email, int productId)
        //{
        //    Cart sc = GetShoppingCart(email, productId);
        //    sc.Quantity = sc.Quantity - 1;
        //    Entity.SaveChanges();
        //}

        public IQueryable<Cart> GetCarts(string email)
        {
            return (from sc in Entity.Carts
                    where sc.Email == email
                    select sc
                    );
        }

        public IQueryable<CartView> GetSCarts(string email)
        {
            return (from sc in Entity.Carts
                    where sc.Email == email
                    select new CartView
                        {
                            email = email,
                            prodi = sc.ProductID,
                            datepurchased = sc.datepurchased
                        });
        }

        public IQueryable<ShoppingCartView> GetProductsinShoppingCart(string email)
        {
            return (from sc in Entity.Carts
                    join p in Entity.Products
                    on sc.ProductID equals p.ProductID
                    where sc.Email == email
                    select new ShoppingCartView
                    {
                        ProductID = p.ProductID,
                        Name = p.Name,
                        Price = p.Price,
                        ImageLink = p.ImageLink
                    }
                    );
        }

        public decimal GetPriceOfCart(string email)
        {
            decimal price = 0;
            IQueryable<ShoppingCartView> sc =GetProductsinShoppingCart(email);
            foreach(ShoppingCartView s in sc)
            {
                price = price + s.Price;
            }
            return price;
        }

        public void DeleteShoppingCartEntry(Cart sc)
        {
            Entity.Carts.Remove(sc);
            Entity.SaveChanges();
        }

        //public int GetNoOfItemsInShoppingCartEntry(string username, int productid)
        //{
        //    if (Entity.Carts.Where(s => s.Username == username && s.ProductID == productid).Count() == 0)
        //        return 0;
        //    else return Entity.Carts.Where(s => s.Username == username && s.ProductID == productid).Sum(x => x.Quantity);
        //}

        public void AddProduct(Product newProduct)
        {
            Entity.Products.Add(newProduct);
            Entity.SaveChanges();
        }

        public void UpdateProduct(Product PtoUpdate)
        {
            Product originalprod = GetProduct(PtoUpdate.ProductID);
            Entity.Products.Attach(originalprod);
            ((IObjectContextAdapter)Entity).ObjectContext.ApplyCurrentValues("Products", originalprod);
            Entity.SaveChanges();
        }

        public void DeleteProduct(int productid)
        {
            Product thisp = GetProduct(productid);
            Product updatedp = thisp;
            if (thisp.isActive == false)
            {
                updatedp.isActive = true;
            }
            else { 
            updatedp.isActive = false;
            }
            Entity.Entry(thisp).CurrentValues.SetValues(thisp);
            Entity.SaveChanges();
        }

        public void MarkActive(int productid)
        {
            Product thisp = GetProduct(productid);
            Product updatedp = thisp;
            updatedp.isActive = true;
            Entity.Entry(thisp).CurrentValues.SetValues(thisp);
            Entity.SaveChanges();
        }




    }
}
