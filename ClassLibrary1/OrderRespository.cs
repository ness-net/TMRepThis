using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using Commonlayer.Views;
using Commonlayer;

namespace DataAccessLayer
{
    public class OrderRespository: ConnectionClass
    {
        public OrderRespository()
            : base()
        {
        }

        public void AddOrder(Order newOrder)
        {
            Entity.Orders.Add(newOrder);
            Entity.SaveChanges();
        }

        public IQueryable<OrderedProducts> GetProductsOfUser(string email)
        {
            var list = (from p in Entity.Products
                        join od in Entity.OrderDetails
                        on p.ProductID equals od.ProductID
                        join o in Entity.Orders
                        on od.OrderID equals o.OrderID
                        where(o.Email == email)
                        select new OrderedProducts
                        {
                            ProductID = p.ProductID,
                            Name = p.Name,
                            OrderedDate = o.Date,
                            Description = p.Description
                        }
                    ).Distinct();

            return list.AsQueryable();

        }

        public OrderedProducts GetOrderedProduct(string email, int productid)
        {
            return (from p in Entity.Products
                        join od in Entity.OrderDetails
                        on p.ProductID equals od.ProductID
                        join o in Entity.Orders
                        on od.OrderID equals o.OrderID
                        where (o.Email == email) && (p.ProductID == productid)
                        select new OrderedProducts
                        {
                            ProductID = p.ProductID,
                            Name = p.Name,
                            OrderedDate = o.Date,
                            Description = p.Description,
                            NotSigned = p.SoftwareBytesS,
                            Signed = p.SoftwareBytesSigned
                        }
                    ).SingleOrDefault();        }


        //public bool userBought(int ProductID, string email)
        //{
        //    var list = (from p in Entity.Products
        //                join od in Entity.OrderDetails
        //                on p.ProductID equals od.ProductID
        //                join o in Entity.Orders
        //                on od.OrderID equals o.OrderID
        //                where (o.Email == email) && (p.ProductID == ProductID)
        //                select new OrderedProducts
        //                {
        //                    ProductID = p.ProductID,
        //                    Name = p.Name,
        //                    OrderedDate = o.Date,
        //                    Description = p.Description
        //                }
        //            ).Distinct();

        //    if(list.Count() == null)
        //    {
        //        return false;
        //    }
        //    else
        //    {
        //        return true;
        //    }
        //}

        public void AddOrderDetails(OrderDetail newOrderDetail)
        {
            Entity.OrderDetails.Add(newOrderDetail);
            Entity.SaveChanges();
        }

        public OrderView LastOrder()
        {
            return (from o in Entity.Orders
                        orderby o.Date descending
                        select new OrderView
                        {
                            OrderID = o.OrderID,
                            Date = o.Date,
                            Email = o.Email
                        }).First();

        }

        public OrderView GetOrder(int id)
        {
            return (from o in Entity.Orders
                    where o.OrderID == id
                    select new OrderView
                    {
                        OrderID = o.OrderID,
                        Date = o.Date,
                        Email = o.Email
                    }).First();
        }

        public int GetOrderID(int productID, string email)
        {
            return (
                from o in Entity.Orders
                join od in Entity.OrderDetails
                on o.OrderID equals od.OrderID
                where od.ProductID == productID && o.Email == email
                select o.OrderID).SingleOrDefault();
        }
    }
}