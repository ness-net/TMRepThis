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
    public class RolesController : BaseController
    {
        private TradersMarketplacedbEntities db = new TradersMarketplacedbEntities();

        // GET: /RoleController1/
        [Authorize(Roles = "Admin")]
        public ActionResult Index(string query)
        {
            if (query == null)
            {
                return View(new RoleService.RoleService1Client().GetAllRolesV());
            }
            else
            {
                return View(new RoleService.RoleService1Client().GetMatchingRoles(query));
            }
        }

        // GET: /RoleController1/Details/5
        [Authorize(Roles = "Admin")]
        public ActionResult Details(int id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Commonlayer.Views.RolesView role = new RoleService.RoleService1Client().GetRoleV(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // GET: /RoleController1/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: /RoleController1/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="RoleID,Role1")] Role role)
        {
            if (ModelState.IsValid)
            {
                new RoleService.RoleService1Client().AddRole(role.Role1);
                return RedirectToAction("Index");
            }

            return View(role);
        }

        // GET: /RoleController1/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: /RoleController1/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="RoleID,Role1")] Role role)
        {
            if (ModelState.IsValid)
            {
                new RoleService.RoleService1Client().UpdateRole(role.RoleID, role.Role1);
                return RedirectToAction("Index");
            }
            return View(role);
        }

        // GET: /RoleController1/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Role role = db.Roles.Find(id);
            if (role == null)
            {
                return HttpNotFound();
            }
            return View(role);
        }

        // POST: /RoleController1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            new RoleService.RoleService1Client().DeleteRole(id);
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
