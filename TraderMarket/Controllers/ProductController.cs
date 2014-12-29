using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Commonlayer.Views;
using TraderMarket.Models;

namespace TraderMarket.Controllers
{
    public class ProductController : BaseController
    {
        //
        // GET: /Product/
        [Authorize(Roles = "Buyer, Admin")]
        public ActionResult Index(System.Nullable<int> id)
        {
          
                
                ViewBag.Categories = new ProdService.ProdServiceClient().GetCategories();
           

            if (id == null)
            {
                List<ProductView> list = new ProdService.ProdServiceClient().GetProducts().ToList();

                return View("Index", list);
            }
            else
            {
                List<ProductView> list = new ProdService.ProdServiceClient().GetProductsAccordingToSubCategory(id).ToList();
                return View(list);
            }
            
        }

        [Authorize(Roles = "Buyer, Admin")]
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

        [HttpPost]
        public ActionResult AddToCart(int userid, string quantity)
        {
            if (User.Identity.IsAuthenticated)
            {
                try
                {
                    new ProdService.ProdServiceClient().AddProducttoCart(User.Identity.Name.ToString(), userid, Convert.ToInt16(quantity));
                    ViewBag.Message = ("Success");
                    return RedirectToAction("Index", "Home");
                }
                catch (Exception ex)
                {
                    ViewBag.Message= ("Could not add to Cart ");
                    return RedirectToAction("Index", "Home");
                }

            }
            else
            {
                ViewBag.Message = ("Log");
                return RedirectToAction("Index", "Home");


            }
        }


        [Authorize(Roles = "Seller, Admin")]
        public ActionResult SellerIndex()
        {
                List<ProductView> list = new ProdService.ProdServiceClient().GetProductsAccordingToSeller(User.Identity.Name).ToList();
                return View("SellerIndex", list);
        }


        [Authorize(Roles = "Seller, Admin")]
        public ActionResult Modify(int id)
        {
            ProductView e = new ProdService.ProdServiceClient().GetProductV(id);
            ViewBag.Product = e;
            ViewBag.SubC = new ProdService.ProdServiceClient().getSubCategories();
            ViewBag.SubCString = new ProdService.ProdServiceClient().getSubCategoryofProduct(e.ProductID);
            var model = new ProductsModel
            {
                name = e.Name,
                description = e.Description,
                ImageLink = e.ImageLink,
                price = e.Price,
                stock = e.Stock
            };
            return View("Modify", model);
        }


        [Authorize(Roles = "Buyer, Admin")]
        public ActionResult Delete(int id)
        {
            new ProdService.ProdServiceClient().DeleteProduct(id);
            List<ProductView> list = new ProdService.ProdServiceClient().GetProductsAccordingToSeller(User.Identity.Name).ToList();
            return View("SellerIndex", list);
        }


        [Authorize(Roles = "Buyer, Admin")]
        public ActionResult Activate(int id)
        {
            new ProdService.ProdServiceClient().MarkActive(id);
            List<ProductView> list = new ProdService.ProdServiceClient().GetProductsAccordingToSeller(User.Identity.Name).ToList();
            return View("SellerIndex", list);
        }
	}
}