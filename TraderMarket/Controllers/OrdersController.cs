using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Commonlayer;
using Commonlayer.Views;
using System.Security.Cryptography;
using System.Diagnostics;
using System.IO;

namespace TraderMarket.Controllers
{
    public class OrdersController : BaseController
    {
        private TradersMarketplacedbEntities db = new TradersMarketplacedbEntities();

        // GET: /Orders/
        [AuthorizeBuyer]
        public ActionResult Index()
        {            
            //var orderdetails = db.OrderDetails.Include(o => o.Order).Include(o => o.OrderStatu).Include(o => o.Product);
            TraderMarket.OrderService.OrderedProducts[] OP = new OrderService.OrderServiceClient().GetProductsOfUser(User.Identity.Name);
            return View(OP.ToList());           
        }

        [AuthorizeBuyer]
        public ActionResult PurchasedIndex()
        {            
                bool paid = true;
                TraderMarket.ProdService.CartView[] carts = new ProdService.ProdServiceClient().GetSCarts(User.Identity.Name);
                foreach (TraderMarket.ProdService.CartView cart in carts)
                {
                    DateTime cdate = Convert.ToDateTime(cart.datepurchased);
                    TimeSpan difference = (DateTime.Now - cdate);
                    if ((difference).TotalMinutes > 2)
                    {
                        paid = false;
                    }
                }
                if (paid == true)
                {
                    string username = User.Identity.Name;
                    try
                    {
                        new OrderService.OrderServiceClient().AddOrder(username);
                        //new OrderService.OrderServiceClient().AddOrder(username);
                        TraderMarket.OrderService.OrderView lastO = new OrderService.OrderServiceClient().LastOrder();
                        int lastorder = lastO.OrderID;

                        foreach (TraderMarket.ProdService.ShoppingCartView s in new ProdService.ProdServiceClient().GetProductsinShoppingCart(User.Identity.Name))
                        {
                            int productid = s.ProductID;
                            //bool checkS = new ProdService.ProdServiceClient().CheckStock(productid, quantity);
                            //if (checkS == true)
                            //{
                            //new ProdService.ProdServiceClient().ControlStock(productid, (0 - quantity));
                            new OrderService.OrderServiceClient().AddOrderDetails(Convert.ToInt16(lastorder), productid);
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
            
            //var orderdetails = db.OrderDetails.Include(o => o.Order).Include(o => o.OrderStatu).Include(o => o.Product);
            TraderMarket.OrderService.OrderedProducts[] OP = new OrderService.OrderServiceClient().GetProductsOfUser(User.Identity.Name);
            return RedirectToAction("Index", "Orders", OP.ToList());
        }

        // GET: /Orders/Details/5
        [AuthorizeBuyer]
        public ActionResult Download(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TraderMarket.OrderService.OrderedProducts prod = new OrderService.OrderServiceClient().GetOrderedProduct(User.Identity.Name, Convert.ToInt16(id));
            if (prod == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SHA1 sha1 = SHA1.Create();
            byte[] productuncrypted = Decrypt(prod.NotSigned, "AbhhcJ");
            byte[] HashValue = sha1.ComputeHash(productuncrypted);
            string publickeyofuser = new UserService.UserServiceClient().GetPublicKey(User.Identity.Name);
            RSACryptoServiceProvider encrypt = new RSACryptoServiceProvider();
            encrypt.FromXmlString(publickeyofuser);
            RSAPKCS1SignatureDeformatter RSADeformatter = new RSAPKCS1SignatureDeformatter(encrypt);
            RSADeformatter.SetHashAlgorithm("SHA1");
            if (RSADeformatter.VerifySignature(HashValue, prod.Signed))
            {                
                return File(productuncrypted, System.Net.Mime.MediaTypeNames.Application.Octet, prod.Name + ".zip");                
            }
            else
            {
                Console.WriteLine("The signature is not valid.");
            }

            return View(prod);
        }

        private static readonly byte[] SALT = new byte[] { 0x26, 0xdc, 0xff, 0x00, 0xad, 0xed, 0x7a, 0xee, 0xc5, 0xfe, 0x07, 0xaf, 0x4d, 0x08, 0x22, 0x3c };

        public static byte[] Decrypt(byte[] cipher, string password)
        {
            MemoryStream memoryStream;
            CryptoStream cryptoStream;
            Rijndael rijndael = Rijndael.Create();
            Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(password, SALT);
            rijndael.Key = pdb.GetBytes(32);
            rijndael.IV = pdb.GetBytes(16);
            memoryStream = new MemoryStream();
            cryptoStream = new CryptoStream(memoryStream, rijndael.CreateDecryptor(), CryptoStreamMode.Write);
            cryptoStream.Write(cipher, 0, cipher.Length);
            cryptoStream.Close();
            return memoryStream.ToArray();
        }

       

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
