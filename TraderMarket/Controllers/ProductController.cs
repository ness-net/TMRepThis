using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Commonlayer.Views;

namespace TraderMarket.Controllers
{
    public class ProductController : BaseController
    {
        //
        // GET: /Product/
        public ActionResult Index()
        {
            try
            {
                //ViewBag.prod = new ProdService.ProdServiceClient().GetProducts1();
                List<ProductView> list = new ProdService.ProdServiceClient().GetProducts().ToList();
                return View("Index", list);
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message + " " + ex.InnerException;
                return View();
            }
            
        }

        public ActionResult Details(int id)
        {
            ProductView e = new ProdService.ProdServiceClient().GetProductV(id);
            //if (new ProductService.ProductsServiceClient().CheckIfPurchased(User.Identity.Name, id) != 0)
            //{
            //    ViewBag.ProductPurchased = "Yes";
            //}
            //else
            //{
            //    ViewBag.ProductPurchased = "No";
            //}
                       
            return View("Details", e);
        }
	}
}