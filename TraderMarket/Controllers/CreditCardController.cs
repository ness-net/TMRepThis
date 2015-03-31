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
    public class CreditCardController : BaseController
    {
        private TradersMarketplacedbEntities db = new TradersMarketplacedbEntities();

        // GET: /CreditCard/
        [Authorize(Roles = "Buyer, Admin")]
        public ActionResult Index()
        {
            var creditcards = db.CreditCards.Include(c => c.User).Where(u => u.Email == User.Identity.Name);
            return View(creditcards.ToList());
        }

        // GET: /CreditCard/Details/5
        [Authorize(Roles = "Buyer, Admin")]
        public ActionResult Details(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditcard = db.CreditCards.Find(id);
            if (creditcard == null)
            {
                return HttpNotFound();
            }
            return View(creditcard);
        }

        // GET: /CreditCard/Create
        [Authorize(Roles = "Buyer, Admin")]
        public ActionResult Create()
        {
            ViewBag.Username = new SelectList(db.Users, "Email", "Password");
            return View();
        }

        // POST: /CreditCard/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="CardType,CardNumber,CardOwner,CVV")] CreditCard creditcard)
        {
            if (ModelState.IsValid)
            {
                creditcard.Email = User.Identity.Name.ToString();
                db.CreditCards.Add(creditcard);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Username = new SelectList(db.Users, "Email", "Password", creditcard.Email);
            return View(creditcard);
        }

        // GET: /CreditCard/Edit/5
        [Authorize(Roles = "Buyer, Admin")]
        public ActionResult Edit(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditcard = db.CreditCards.Find(id);
            if (creditcard == null)
            {
                return HttpNotFound();
            }
            ViewBag.Username = new SelectList(db.Users, "Email", "Password", creditcard.Email);
            return View(creditcard);
        }

        // POST: /CreditCard/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="CardType,CardNumber,CardOwner,CVV,Email")] CreditCard creditcard)
        {
            if (ModelState.IsValid)
            {
                db.Entry(creditcard).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Username = new SelectList(db.Users, "Email", "Password", creditcard.Email);
            return View(creditcard);
        }

        // GET: /CreditCard/Delete/5
        [Authorize(Roles = "Buyer, Admin")]
        public ActionResult Delete(decimal id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CreditCard creditcard = db.CreditCards.Find(id);
            if (creditcard == null)
            {
                return HttpNotFound();
            }
            return View(creditcard);
        }

        // POST: /CreditCard/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(decimal id)
        {
            CreditCard creditcard = db.CreditCards.Find(id);
            db.CreditCards.Remove(creditcard);
            db.SaveChanges();
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
    }
}
