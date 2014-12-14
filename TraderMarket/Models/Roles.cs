using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Commonlayer;
using System.Data.Entity;

namespace TraderMarket.Models
{
    public class Roles
    {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        public class RolesDBContext : TradersMarketplacedbEntities
        {
            public DbSet<Role> Role { get; set; }
        } 
    
}