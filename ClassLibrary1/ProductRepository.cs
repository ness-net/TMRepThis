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

        public IEnumerable<ProductView> GetProducts()
        {            
            var list = (from p in Entity.Products
                        orderby p.ProductID
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
                            Stock= Convert.ToInt16(p.Stock)
                        }
                    ).Distinct();

           return list.AsQueryable();
        
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
                        SubCategoryID = Convert.ToInt16(p.SubCategoryID),
                        ImageLink = p.ImageLink,
                        Price = p.Price,
                        Username = p.Username,
                        Stock = Convert.ToInt16(p.Stock)
                    }).ToList();
        }

        public Product GetProduct(int id)
        {
            return Entity.Products.SingleOrDefault(x => x.ProductID == id);
        }

        //public IEnumerable<Product> SearchProduct(string keyword)
        //{
        //    return Entity.Products.Where(x => x.Name.Contains(keyword));
        //}

        public int GetProductID(string name)
        {
            return (from p in Entity.Products
                    where p.Name == name
                    select p.ProductID
                     ).SingleOrDefault();
        }

        //public void AddToCart(Cart cart)
        //{
        //    Entity.Carts.AddObject(cart);
        //    Entity.SaveChanges();
        //}

        //public Cart GetShoppingCart(string username, int productId)
        //{
        //    return Entity.Carts.SingleOrDefault(x => x.Username == username && x.ProductID == productId);
        //}

        //public void UpdateCart(string username, int productId, int newQty)
        //{
        //    Cart sc = GetShoppingCart(username, productId);
        //    sc.Quantity += newQty;
        //    Entity.SaveChanges();
        //}

        //public void DecrementCart(string username, int productId)
        //{
        //    Cart sc = GetShoppingCart(username, productId);
        //    sc.Quantity = sc.Quantity - 1;
        //    Entity.SaveChanges();
        //}

        //public IQueryable<ShoppingCartView> GetProductsinShoppingCart(string Username)
        //{
        //    return (from sc in Entity.Carts
        //            join p in Entity.Products
        //            on sc.ProductID equals p.ProductID
        //            where sc.Username == Username
        //            select new ShoppingCartView
        //            {
        //                ProductID = p.ProductID,
        //                Name = p.Name,
        //                Price = p.Price,
        //                Quantity = sc.Quantity,
        //                ImageLink = p.ImageLink
        //            }
        //            );
        //}


        //public int GetNoOfItemsInShoppingCartEntry(string username, int productid)
        //{
        //    if (Entity.Carts.Where(s => s.Username == username && s.ProductID == productid).Count() == 0)
        //        return 0;
        //    else return Entity.Carts.Where(s => s.Username == username && s.ProductID == productid).Sum(x => x.Quantity);
        //}

        //public void DeleteShoppingCartEntry(Cart sc)
        //{
        //    Entity.Carts.DeleteObject(sc);
        //    Entity.SaveChanges();
        //}

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

        //public System.Nullable<int> CheckIfPurchased(string username, int productID)
        //{
        //    return (from od in Entity.OrderDetails
        //            join o in Entity.Orders
        //            on od.OrderID equals o.OrderID
        //            where od.ProductID == productID && o.Username == username
        //            select od.Quantity).SingleOrDefault();
        //}




        //public IQueryable<ItemsPView> GetItemsPurchased(string Username)
        //{
        //    return (from od in Entity.OrderDetails
        //            join p in Entity.Products
        //            on od.ProductID equals p.ProductID
        //            join o in Entity.Orders
        //            on od.OrderID equals o.OrderID
        //            where o.Username == Username
        //            select new ItemsPView
        //            {
        //                Features = p.Features,
        //                Price = p.Price,
        //                ProductID = p.ProductID,
        //                Purchased = o.Date,
        //                OrderID = o.OrderID,
        //                Name = p.Name,
        //                ImageLink = p.ImageLink
        //            }
        //           );
        //}

        //public IQueryable<string> GetItemsPurchasedNames(string Username)
        //{
        //    return (from od in Entity.OrderDetails
        //            join p in Entity.Products
        //            on od.ProductID equals p.ProductID
        //            join o in Entity.Orders
        //            on od.OrderID equals o.OrderID
        //            where o.Username == Username
        //            select p.Name
        //           );
        //}

        //public String GetStatusofItem(int productID, int OrderID)
        //{
        //    FaultRepository FR = new FaultRepository();
        //    if (FR.CheckIfItemAlreadyReported(OrderID, productID) == null)
        //    {
        //        return (from f in Entity.Faults
        //                join fd in Entity.FaultDetails
        //                on f.FaultID equals fd.FaultID
        //                join fs in Entity.Status
        //                on fd.FaultStatusID equals fs.StatusID
        //                orderby fd.Date descending
        //                where f.ProductID == productID && f.OrderID == OrderID
        //                select fs.Status1
        //               ).SingleOrDefault();
        //    }
        //    else
        //    {
        //        return (from f in Entity.Faults
        //                join fd in Entity.FaultDetails
        //                on f.FaultID equals fd.FaultID
        //                join fs in Entity.Status
        //                on fd.FaultStatusID equals fs.StatusID
        //                orderby fd.Date descending
        //                where f.ProductID == productID && f.OrderID == OrderID
        //                select fs.Status1
        //               ).First();
        //    }
        //}

        //public ItemsPView GetItemDetails(int OrderID, int ProductID)
        //{
        //    return (from od in Entity.OrderDetails
        //            join o in Entity.Orders
        //            on od.OrderID equals o.OrderID
        //            join p in Entity.Products
        //            on od.ProductID equals p.ProductID
        //            where o.OrderID == OrderID && od.ProductID == ProductID
        //            select new ItemsPView
        //            {
        //                Features = p.Features,
        //                Price = p.Price,
        //                ProductID = p.ProductID,
        //                Purchased = o.Date,
        //                OrderID = o.OrderID,
        //                Name = p.Name,
        //                ImageLink = p.ImageLink
        //            }
        //           ).SingleOrDefault();
        //}

        

        //public int GetPurchasedTimes(int productid)
        //{
        //    return (from p in Entity.Products
        //            join o in Entity.OrderDetails
        //            on p.ProductID equals o.ProductID
        //            where p.ProductID == productid
        //            select o.Quantity).DefaultIfEmpty(0).Sum();
        //}

        


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
