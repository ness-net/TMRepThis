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
                            Username = o.Username,
                            OrderStatus = o.OrderStatusID
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
                        Username = o.Username,
                        OrderStatus = o.OrderStatusID
                    }).First();
        }

        public int GetOrderID(int productID, string username)
        {
            return (
                from o in Entity.Orders
                join od in Entity.OrderDetails
                on o.OrderID equals od.OrderID
                where od.ProductID == productID && o.Username == username
                select o.OrderID).SingleOrDefault();
        }
    }
}