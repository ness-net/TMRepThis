using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Commonlayer;
using System.Data.Entity;

namespace TraderMarket.Models
{
    public class Products
    {
        public int ProductID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string CategoryID { get; set; }
        public string ImageLink { get; set; }
        public decimal Price { get; set; }
        public string Email { get; set; }
    }

    public class ProductsDBContext : TradersMarketplacedbEntities
    {
        public DbSet<Product> Product { get; set; }
    } 
}