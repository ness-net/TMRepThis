using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Commonlayer;
using System.IO;
using System.Security.Cryptography;

namespace TraderMarket.Controllers
{
    public class ProductsController : BaseController
    {
        private TradersMarketplacedbEntities db = new TradersMarketplacedbEntities();

        // GET: /Products/
        [Authorize(Roles = "Seller, Admin")]
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.User).Where(u => u.Email == User.Identity.Name);
            return View(products.ToList());

            ViewBag.MessageSoftware = "";
        }

        // GET: /Products/Details/5
        [Authorize(Roles = "Seller, Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        

        [Authorize(Roles = "Seller, Admin")]
        public ActionResult CreateSoft()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            ViewBag.Username = new SelectList(db.Users, "Email", "Password");
            return View();
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateSoft([Bind(Include = "ProductID,Name,Description,CategoryID,Price,isActive")] Product product, HttpPostedFileBase file, HttpPostedFileBase filesoft)
        {
            //Validations First of Images
            string[] formatspic = new string[] { ".jpg", ".png", ".gif", ".jpeg" };
            if(formatspic.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase)) == false)
            {
                ViewBag.MessageSoftware = "Wrong Image File type! Allowed file types: .jpg / .png / .gif / .jpeg";
                ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
                ViewBag.Username = new SelectList(db.Users, "Email", "Password");
                return View();
            }

            Dictionary<string, byte[]> imageHeader = new Dictionary<string, byte[]>();
            imageHeader.Add("JPG", new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 });
            imageHeader.Add("JPEG", new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 });
            imageHeader.Add("PNG", new byte[] { 0x89, 0x50, 0x4E, 0x47 });
            imageHeader.Add("GIF", new byte[] { 0x47, 0x49, 0x46, 0x38 });
            string fileExt  = file.FileName.Substring(file.FileName.LastIndexOf('.') + 1).ToUpper();
           
            byte[] tmp = imageHeader[fileExt];
            byte[] header = new byte[tmp.Length];

            file.InputStream.Read(header, 0, header.Length);
            //file.FileContent.Read(header, 0, header.Length);

            if (!CompareArray(tmp, header))
            {
                ViewBag.MessageSoftware = "Image is not allowed";
                ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
                ViewBag.Username = new SelectList(db.Users, "Email", "Password");
                return View();
            }

            string filename = "";
            if (file != null && file.ContentLength > 0)
            {
                string absolutePathOfImagesFolder = Server.MapPath("\\Content");
                filename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
                file.SaveAs(absolutePathOfImagesFolder + "\\" + filename);
            }
            
            string[] formatsoft = new string[] { ".zip" };
            if(formatsoft.Any(item => filesoft.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase)) == false)
            {
                ViewBag.MessageSoftware = "Wrong File type of software! Allowed file types: .zip";
                return View();
            }

            Dictionary<string, byte[]> zipHeader = new Dictionary<string, byte[]>();
            zipHeader.Add("ZIP", new byte[] { 0x50, 0x4B, 0x03, 0x04, 0x14, 0x00 });
            //zipHeader.Add("RAR", new byte[] { 0x52, 0x61, 0x72, 0x21, 0x1A, 0x07 });

            string fileExt2 = filesoft.FileName.Substring(filesoft.FileName.LastIndexOf('.') + 1).ToUpper();

            byte[] tmp1 = zipHeader[fileExt2];
            byte[] header1 = new byte[tmp1.Length];

            filesoft.InputStream.Read(header1, 0, header1.Length);
            //file.FileContent.Read(header, 0, header.Length);

            if (!CompareArray(tmp1, header1))
            {
                ViewBag.MessageSoftware = "Software is not allowed";
                return View();
            }

            //Creating Byte Array
            MemoryStream target = new MemoryStream();
            filesoft.InputStream.CopyTo(target);            
            byte[] data = target.ToArray();

            SHA1 sha1 = SHA1.Create();
            byte[] HashValue = sha1.ComputeHash(data);

            //SIGN IT
            string privatekeyofuser = new UserService.UserServiceClient().GetPrivateKey(User.Identity.Name);
            RSACryptoServiceProvider encrypt = new RSACryptoServiceProvider();
            encrypt.FromXmlString(privatekeyofuser);
            RSAPKCS1SignatureFormatter RSAFormatter = new RSAPKCS1SignatureFormatter(encrypt);
            RSAFormatter.SetHashAlgorithm("SHA1");
            byte[] SignedData = RSAFormatter.CreateSignature(HashValue);            
            

            if (ModelState.IsValid)
            {
                product.SoftwareBytesS = data;
                product.SoftwareBytesSigned = SignedData;
                product.ImageLink = (("../../Content/") + filename).ToString();
                product.Email = User.Identity.Name;
                db.Products.Add(product);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception c) { string message = c.Message; }
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", product.CategoryID);
            ViewBag.Username = new SelectList(db.Users, "Email", "Password", product.Email);
            return View(product);
        }

        //public Product CreateTestStub(Product product)
        //{
        //        db.Products.Add(product);
        //        db.SaveChanges();
            
        //    return (product);
        //}

        // GET: /Products/Edit/5
        [Authorize(Roles = "Seller, Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", product.CategoryID);
            ViewBag.Username = new SelectList(db.Users, "Email", "Password", product.Email);
            return View(product);
        }

        //Used for unit testing
        public Product EditUnitTEdit(Product product)
        {     
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            
            return product;
        }

        // POST: /Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ProductID,Name,Description,CategoryID,Price,Email,isActive")] Product product, HttpPostedFileBase file)
        {
            string prevImageLink = new ProdService.ProdServiceClient().GetProductImageLi(product.ProductID);
            string filename = "";
            if (file != null && file.ContentLength > 0)
            {
                string[] formatspic = new string[] { ".jpg", ".png", ".gif", ".jpeg" };
                if (formatspic.Any(item => file.FileName.EndsWith(item, StringComparison.OrdinalIgnoreCase)) == false)
                {
                    ViewBag.MessageSoftware = "Wrong Image File type! Allowed file types: .jpg / .png / .gif / .jpeg";
                    return View();
                }

                Dictionary<string, byte[]> imageHeader = new Dictionary<string, byte[]>();
                imageHeader.Add("JPG", new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 });
                imageHeader.Add("JPEG", new byte[] { 0xFF, 0xD8, 0xFF, 0xE1 });
                imageHeader.Add("PNG", new byte[] { 0x89, 0x50, 0x4E, 0x47 });
                imageHeader.Add("GIF", new byte[] { 0x47, 0x49, 0x46, 0x38 });
                string fileExt = file.FileName.Substring(file.FileName.LastIndexOf('.') + 1).ToUpper();

                byte[] tmp = imageHeader[fileExt];
                byte[] header = new byte[tmp.Length];

                file.InputStream.Read(header, 0, header.Length);
                //file.FileContent.Read(header, 0, header.Length);

                if (!CompareArray(tmp, header))
                {
                    ViewBag.MessageSoftware = "Image is not allowed";
                    return View();
                }
                                
                string absolutePathOfImagesFolder = Server.MapPath("\\Content");
                filename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
                file.SaveAs(absolutePathOfImagesFolder + "\\" + filename);
                product.ImageLink = (("../../Content/") + filename).ToString();
            }
            else
            {
                product.ImageLink = prevImageLink;
            }

            product.Email = User.Identity.Name;
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", product.CategoryID);
            ViewBag.Username = new SelectList(db.Users, "Email", "Password", product.Email);
            return View(product);
        }

        // GET: /Products/Delete/5
        [Authorize(Roles = "Seller, Admin")]
        public ActionResult Delete(int id)
        {
            new ProdService.ProdServiceClient().DeleteProduct(id);
            Product product = db.Products.Find(id);
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool CompareArray(byte[] a1, byte[] a2)
        {

            if (a1.Length != a2.Length)

                return false;



            for (int i = 0; i < a1.Length; i++)
            {

                if (a1[i] != a2[i])

                    return false;

            }



            return true;

        }
    }
}
