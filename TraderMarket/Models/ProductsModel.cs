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
       public Product MyProducts { get; set; }
        //a variable to return a selected list of the categories
       public SelectList CategoryList { get; set; }


        //constructor
        //public ProductsModel()
        //{            
        //    List<bool> myList = new List<bool>();
        //    myList.Add(true);
        //    myList.Add(false);

        //    //list the categories from the category table
        //    CategoryList = 
        //        new SelectList(new CategoryService.CategoriesServicesClient().getMainCategories(), 
        //            "ID", "Category"); 
        //}


    
	}
}