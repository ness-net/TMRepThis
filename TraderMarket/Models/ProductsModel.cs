using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web.Mvc;
using System.Web.Security;
using Commonlayer;
using Commonlayer.Views;

namespace TraderMarket.Models
{
    public class ProductsModel
    {
        [Required]
        [Display(Name = "Name")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string description { get; set; }

        [Required]
        [Display(Name = "Category")]
        public string Category { get; set; }

        [Required]
        [Display(Name = "SubCategory")]
        public string SubCategory { get; set; }

        [Required]
        [Display(Name = "ImageLink")]
        public string ImageLink { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal price { get; set; }

        [Required]
        [Display(Name = "Stock")]
        public decimal stock { get; set; }



	}


}