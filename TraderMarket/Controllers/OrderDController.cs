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
    public class OrderDController : BaseController
    {
        private TradersMarketplacedbEntities db = new TradersMarketplacedbEntities();

        // GET: /OrderD/
        [AuthorizeSeller]
        public ActionResult Index()
        {
            var orderdetails = db.OrderDetails.Include(o => o.Order).Include(o => o.Product).Where(o => o.Product.Email == User.Identity.Name);
            return View(orderdetails.ToList());
        }

        // GET: /OrderD/Details/5
        [AuthorizeSeller]
        public ActionResult Details(int? orderid, int? prodid)
        {
            if ((orderid == null) && (prodid == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderdetail = db.OrderDetails.Find(orderid, prodid);
            if (orderdetail == null)
            {
                return HttpNotFound();
            }
            return View(orderdetail);
        }



        // GET: /OrderD/Edit/5
        [AuthorizeSeller]
        public ActionResult Edit(int? orderid, int? prodid)
        {
            if ((orderid == null) || (prodid == null))
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderDetail orderdetail = db.OrderDetails.Find(orderid, prodid);
            if ((orderid == null) || (prodid == null))
            {
                return HttpNotFound();
            }
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "Date", orderdetail.OrderID);
            //ViewBag.OrderStatusID = new SelectList(db.OrderStatus, "OrderStatusID", "Status");
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", orderdetail.ProductID);
            return View(orderdetail);
        }

        // POST: /OrderD/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [AuthorizeSeller]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="OrderID,ProductID,Quantity")] OrderDetail orderdetail)
        {
            if (ModelState.IsValid)
            {

                db.Entry(orderdetail).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.OrderID = new SelectList(db.Orders, "OrderID", "Date", orderdetail.OrderID);
            //ViewBag.OrderStatusID = new SelectList(db.OrderStatus, "OrderStatusID", "Status", orderdetail.OrderStatusID);
            ViewBag.ProductID = new SelectList(db.Products, "ProductID", "Name", orderdetail.ProductID);
            return View(orderdetail);
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
