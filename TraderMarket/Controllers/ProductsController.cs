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
        [Authorize(Roles = "Seller")]
        public ActionResult Index()
        {
            var products = db.Products.Include(p => p.Category).Include(p => p.User).Where(u => u.Username == User.Identity.Name);
            return View(products.ToList());
        }

        // GET: /Products/Details/5
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
        public ActionResult Create([Bind(Include="ProductID,Name,Description,CategoryID,ImageLink,Price,Stock,isActive")] Product product)
        {
            if (ModelState.IsValid)
            {
                product.Username = User.Identity.Name;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CategoryID = new SelectList(db.Categories, "CategoryID", "Name", product.CategoryID);
            ViewBag.Username = new SelectList(db.Users, "Username", "Password", product.Username);
            return View(product);
        }

        // GET: /Products/Edit/5
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

        // POST: /Products/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ProductID,Name,Description,CategoryID,ImageLink,Price,Username,Stock,isActive")] Product product)
        {
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
