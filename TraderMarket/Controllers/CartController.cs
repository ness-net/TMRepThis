using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Commonlayer;
using Commonlayer.Views;
using TraderMarket.Models;

namespace TraderMarket.Controllers
{
    public class CartController : BaseController
    {
        //
        // GET: /Cart/
        [Authorize(Roles = "Buyer, Admin")]
        public ActionResult Index()
        {
            var cart = new ProdService.ProdServiceClient().GetProductsinShoppingCart(User.Identity.Name.ToString());
            ViewBag.NICK = User.Identity.Name.ToString();
            return View(cart);
        }


        [HttpPost]
        public ActionResult DeleteCartEntry(int productID)
        {
            try
            {
                new ProdService.ProdServiceClient().DeleteShoppingCartEntry(User.Identity.Name.ToString(), productID);
                return RedirectToAction("Index", "Cart");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ("Could not Delete");
                return RedirectToAction("Index", "Cart");
            }
        }

        //[HttpPost]
        //public ActionResult Decrement(int productID)
        //{
        //    try
        //    {
        //        new ProdService.ProdServiceClient().DecrementCart(User.Identity.Name.ToString(), productID);
        //        return RedirectToAction("Index", "Cart");
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Message = ("Could not Delete");
        //        return RedirectToAction("Index", "Cart");
        //    }
        //}

        [Authorize(Roles = "Buyer, Admin")]
        public ActionResult CheckOut()
        {
            ViewBag.Message = "Check Out";
            ViewBag.Cards = new UserService.UserServiceClient().GetCreditCards(User.Identity.Name.ToString());
            return View();
        }

        /// <summary>
        /// This is ran  when the user checks out as to add it to the database
        /// </summary>
        /// <param name="data">Data that is written in the form</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        public ActionResult CheckOut(CheckOutModel model)
        {
            int index = model.Card.IndexOf(" ");
            decimal number = Convert.ToDecimal(model.Card.Substring(0, index));

            string username = User.Identity.Name;
            try
            {
                new OrderService.OrderServiceClient().AddOrder(username, number);
                OrderView lastO = new OrderService.OrderServiceClient().LastOrder();
                ViewBag.OrderID = lastO.OrderID;

                foreach (ShoppingCartView s in new ProdService.ProdServiceClient().GetProductsinShoppingCart(User.Identity.Name))
                {
                    int productid = s.ProductID;
                    //bool checkS = new ProdService.ProdServiceClient().CheckStock(productid, quantity);
                    //if (checkS == true)
                    //{
                        //new ProdService.ProdServiceClient().ControlStock(productid, (0 - quantity));
                        new OrderService.OrderServiceClient().AddOrderDetails(Convert.ToInt16(ViewBag.OrderID), productid);
                        new ProdService.ProdServiceClient().DeleteShoppingCartEntry(username, productid);
                    //}
                    //else
                    //{
                    //    int stk = new ProdService.ProdServiceClient().GetStock(productid);
                    //    new ProdService.ProdServiceClient().ControlStock(productid, (0 - stk));
                    //    new OrderService.OrderServiceClient().AddOrderDetails(Convert.ToInt16(ViewBag.OrderID), productid, stk);
                    //    new ProdService.ProdServiceClient().DeleteShoppingCartEntry(username, productid);
                    //}
                    

                }
                return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Message = ("Could not add to Cart ");
                return RedirectToAction("Index", "Home");
            }
        }
	}
}