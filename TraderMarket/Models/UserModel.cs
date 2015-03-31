using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Commonlayer;
using System.Data.Entity;

namespace TraderMarket.Models
{
    public class UserModel
    {
          public int Username { get; set; }
          public string Password { get; set; }        
          public string Name { get; set; }        
          public string Surname { get; set; }        
          public long ContactNo { get; set; }        
          public string Email { get; set; }     
          //public bool Buyer { get; set; }
          //public bool Seller { get; set; }
        }

        public class UserDBContext : TradersMarketplacedbEntities
        {
            public DbSet<User> User { get; set; }           
        } 
    
}