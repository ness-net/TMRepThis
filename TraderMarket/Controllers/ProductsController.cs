using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Commonlayer;

namespace TraderMarket.Controllers
{
    public class ProductsController : BaseController
    {
        private TradersMarketplacedbEntities db = new TradersMarketplacedbEntities();

        // GET: /Products/
        [Authorize(Roles = "Seller, Admin")]
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.User).Where(u => u.Username == User.Identity.Name);
            return View(products.ToList());
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

        // GET: /Products/Create
        [Authorize(Roles = "Seller, Admin")]
        public ActionResult Create()
        {
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name");
            ViewBag.Username = new SelectList(db.Users, "Username", "Password");
            return View();
        }

        // POST: /Products/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ProductID,Name,Description,CategoryID,Price,Stock,isActive")] Product product, HttpPostedFileBase file)
        {
            string filename = "";
            if (file != null && file.ContentLength > 0)
            {
                string absolutePathOfImagesFolder = Server.MapPath("\\Content");
                filename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
                file.SaveAs(absolutePathOfImagesFolder + "\\" + filename);
            }

            if (ModelState.IsValid)
            {
                product.ImageLink = (("../../Content/")+filename).ToString();
                product.Username = User.Identity.Name;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", product.CategoryID);
            ViewBag.Username = new SelectList(db.Users, "Username", "Password", product.Username);
            return View(product);
        }

        public Product CreateTestStub(Product product)
        {
                db.Products.Add(product);
                db.SaveChanges();
            
            return (product);
        }

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
            ViewBag.Username = new SelectList(db.Users, "Username", "Password", product.Username);
            return View(product);
        }

        //Used for unit testing
        public Product EditUnitTEdit(Product product)
        {     
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
            
            //ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", product.CategoryID);
            //ViewBag.Username = new SelectList(db.Users, "Username", "Password", product.Username);
            return product;
        }

        // POST: /Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ProductID,Name,Description,CategoryID,Price,Username,Stock,isActive")] Product product, HttpPostedFileBase file)
        {
            string prevImageLink = new ProdService.ProdServiceClient().GetProductImageLi(product.ProductID);
            string filename = "";
            if (file != null && file.ContentLength > 0)
            {
                string absolutePathOfImagesFolder = Server.MapPath("\\Content");
                filename = Guid.NewGuid().ToString() + System.IO.Path.GetExtension(file.FileName);
                file.SaveAs(absolutePathOfImagesFolder + "\\" + filename);
                product.ImageLink = (("../../Content/") + filename).ToString();
            }
            else
            {
                product.ImageLink = prevImageLink;
            }

            product.Username = User.Identity.Name;
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", product.CategoryID);
            ViewBag.Username = new SelectList(db.Users, "Username", "Password", product.Username);
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

        //// POST: /Products/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public ActionResult DeleteConfirmed(int id)
        //{
        //    Product product = db.Products.Find(id);
        //    db.Products.Remove(product);
        //    db.SaveChanges();
            
        //}

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
