using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Commonlayer.Views;

namespace TraderMarket.Controllers
{
    [RequireHttps]
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {
            List<TraderMarket.ProdService.ProductView> list = new ProdService.ProdServiceClient().GetProducts().ToList();

            return View("Index", list);
        }

       
    }
}