using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Commonlayer;
using Commonlayer.Views;
using TraderMarket.Models;
using PayPal.AdaptivePayments;
using TraderMarket.Payments;
using PayPal.AdaptivePayments.Model;
using System.Configuration;

namespace TraderMarket.Controllers
{
    public class CartController : BaseController
    {
        
        public PaymentsManager PaymentsManage { get; set; }

        
        //
        // GET: /Cart/
        [AuthorizeBuyer]
        public ActionResult Index()
        {
            var cart = new ProdService.ProdServiceClient().GetProductsinShoppingCart(User.Identity.Name.ToString());
            ViewBag.NICK = User.Identity.Name.ToString();
            return View(cart);
        }

        [AuthorizeBuyer]
        public ActionResult Unsuccessful()
        {
            ViewBag.Message = "Could not complete payment.";
            return RedirectToAction("Index", "Cart");
        }
     
        [HttpPost]
        [AuthorizeBuyer]
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

        [AuthorizeBuyer]
        public ActionResult CheckOut()
        {
            ViewBag.Message = "Check Out";
            ViewBag.Price = new ProdService.ProdServiceClient().GetPriceOfCart(User.Identity.Name);
            //ViewBag.Cards = new UserService.UserServiceClient().GetCreditCards(User.Identity.Name.ToString());
            return View();
        }

        /// <summary>
        /// This is ran  when the user checks out as to add it to the database
        /// </summary>
        /// <param name="data">Data that is written in the form</param>
        /// <returns>ActionResult</returns>
        [HttpPost]
        [AuthorizeBuyer]
        public ActionResult CheckOut(CheckOutModel Model)
        {
            //PayPal.AdaptivePayments.AdaptivePaymentsService _paysvc = new PayPal.AdaptivePayments.AdaptivePaymentsService();
            Uri redirectUrl = new Uri(ConfigurationManager.AppSettings["REDIRECT-URL"]);
            Random rnd = new Random();
            int random = rnd.Next(1, 5000);
            string returnUrl = "https://localhost:44301/Orders/PurchasedIndex/";
           
            new ProdService.ProdServiceClient().UpdateCart(User.Identity.Name, random.ToString());
            Uri cancelUrl = new Uri(ConfigurationManager.AppSettings["PAYPAL-CANCELURI"]);
            string seller = ConfigurationManager.AppSettings["PAYPAL-SELLER"];
            //string paymentref = "123";
            string currency = "EUR";//modified
            string actionType = "PAY";
            decimal ammountp = new ProdService.ProdServiceClient().GetPriceOfCart(User.Identity.Name);
            ReceiverList receivers = new ReceiverList();
            Receiver r1 = new Receiver(ammountp);
            r1.email = "vmangion@gmail.com";//modified
            receivers.receiver.Add(r1);

            RequestEnvelope requestEnveloper = new RequestEnvelope("en_US");

            PayRequest payRequest = new PayRequest(requestEnveloper, actionType, cancelUrl.ToString(), currency, receivers, returnUrl.ToString());


            payRequest.ipnNotificationUrl = ("https://localhost:44301/Cart/CheckOut");

            Dictionary<String, String> sdkConfig = new Dictionary<String, String>();
            sdkConfig.Add("mode", "sandbox");
            sdkConfig.Add("account1.apiUsername", "vmangion92-facilitator_api1.gmail.com");
            sdkConfig.Add("account1.apiPassword", "MZAPWF68H7NPRC55");
            sdkConfig.Add("account1.apiSignature", "AiPC9BjkCyDFQXbSkoZcgqH3hpacAuoOnDnanEaMj4a.FGc.73mpXurQ");
            sdkConfig.Add("account1.applicationId", "APP-80W284485P519543T");

            AdaptivePaymentsService adap = new AdaptivePaymentsService(sdkConfig);
            try
            {
                PayResponse payr = adap.Pay(payRequest);
                return Redirect("https://www.sandbox.paypal.com/us/cgi-bin/webscr?cmd=_ap-payment&paykey=" + payr.payKey);
            }
            catch(Exception ex)
            {
                string mess = ex.Message;
            }            
            return View();
            //return View();
            ////IPayPalAdaptivePaymentService 
            //int index = model.Card.IndexOf(" ");
            //decimal number = Convert.ToDecimal(model.Card.Substring(0, index));

            //string username = User.Identity.Name;
            //try
            //{
            //    new OrderService.OrderServiceClient().AddOrder(username, number);
            //    TraderMarket.OrderService.OrderView lastO = new OrderService.OrderServiceClient().LastOrder();
            //    ViewBag.OrderID = lastO.OrderID;

            //    foreach (TraderMarket.ProdService.ShoppingCartView s in new ProdService.ProdServiceClient().GetProductsinShoppingCart(User.Identity.Name))
            //    {
            //        int productid = s.ProductID;
            //        //bool checkS = new ProdService.ProdServiceClient().CheckStock(productid, quantity);
            //        //if (checkS == true)
            //        //{
            //            //new ProdService.ProdServiceClient().ControlStock(productid, (0 - quantity));
            //            new OrderService.OrderServiceClient().AddOrderDetails(Convert.ToInt16(ViewBag.OrderID), productid);
            //            new ProdService.ProdServiceClient().DeleteShoppingCartEntry(username, productid);
            //        //}
            //        //else
            //        //{
            //        //    int stk = new ProdService.ProdServiceClient().GetStock(productid);
            //        //    new ProdService.ProdServiceClient().ControlStock(productid, (0 - stk));
            //        //    new OrderService.OrderServiceClient().AddOrderDetails(Convert.ToInt16(ViewBag.OrderID), productid, stk);
            //        //    new ProdService.ProdServiceClient().DeleteShoppingCartEntry(username, productid);
            //        //}
                    

            //    }
            //    return RedirectToAction("Index", "Home");
            //}
            //catch (Exception ex)
            //{
            //    ViewBag.Message = ("Could not add to Cart ");
            //    return RedirectToAction("Index", "Home");
            //}
        }

       
	}
}