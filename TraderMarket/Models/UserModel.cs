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
          public string Residence { get; set; }   
          public string Street { get; set; }   
          public string Town { get; set; }
          public string PostCode { get; set; }
          public string Country { get; set; }        
          public string HandlesDeliveries { get; set; }
        }

        public class UserDBContext : TradersMarketplacedbEntities
        {
            public DbSet<User> User { get; set; }
        } 
    
}