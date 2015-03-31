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
        [Authorize(Roles = "Buyer, Admin")]
        public ActionResult Index()
        {
            //var orderdetails = db.OrderDetails.Include(o => o.Order).Include(o => o.OrderStatu).Include(o => o.Product);
            OrderedProducts[] OP = new OrderService.OrderServiceClient().GetProductsOfUser(User.Identity.Name);
            return View(OP.ToList());
           
        }

        // GET: /Orders/Details/5
        public ActionResult Download(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderedProducts prod = new OrderService.OrderServiceClient().GetOrderedProduct(User.Identity.Name, Convert.ToInt16(id));
            if (prod == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            SHA1 sha1 = SHA1.Create();
            byte[] HashValue = sha1.ComputeHash(prod.NotSigned);
            string publickeyofuser = new UserService.UserServiceClient().GetPublicKey(User.Identity.Name);
            RSACryptoServiceProvider encrypt = new RSACryptoServiceProvider();
            encrypt.FromXmlString(publickeyofuser);
            RSAPKCS1SignatureDeformatter RSADeformatter = new RSAPKCS1SignatureDeformatter(encrypt);
            RSADeformatter.SetHashAlgorithm("SHA1");
            if (RSADeformatter.VerifySignature(HashValue, prod.Signed))
            {
                //Console.WriteLine("The signature is valid.");
                byte[] product = prod.NotSigned;
                // get a valid temporary file name and change the extension to PDF
                return File(product, System.Net.Mime.MediaTypeNames.Application.Octet, prod.Name+".zip");
                //var tempFileName = Path.ChangeExtension(Path.GetTempFileName(), "ZIP");
                
                //System.IO.File.WriteAllBytes(tempFileName, product);
                //Process.Start(tempFileName); 
                
               
                // Clean up our temporary file...
                //Process.Exited += (s, e) => System.IO.File.Delete(filename); 
            }
            else
            {
                Console.WriteLine("The signature is not valid.");
            }

            return View(prod);
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
