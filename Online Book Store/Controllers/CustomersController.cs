using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Online_Book_Store.Models;

namespace Online_Book_Store.Controllers
{
    public class CustomersController : Controller
    {
        private OnlineBookStoreContext db = new OnlineBookStoreContext();

        // GET: Customers
        public ActionResult Index()
        {
            TempData.Keep();
            var customers = db.Customers.Find((int)TempData["CurrentUserId"]);
            return View(customers);
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            //ViewBag.UserId = new SelectList(db.Carts, "UserId", "UserId");
            //Customer customer = new Customer();
            return View(/*customer*/);
        }

        // POST: Customers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "UserId,Name,Username,Password,Email,MobileNumber")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                TempData.Keep();
                db.Customers.Add(customer);
                db.SaveChanges();
                db.Carts.Add(new Cart() { CartId = customer.UserId, UserId = db.Customers.ToList()[db.Customers.ToList().Count() - 1].UserId, CartSize = 0 });
                db.SaveChanges();
                TempData["CurrentUserId"] = customer.UserId;
                TempData["CurrentUserName"] = customer.Name;
                return RedirectToAction("Home","Shop");
            }

            //ViewBag.UserId = new SelectList(db.Carts, "UserId", "UserId", customer.UserId);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            //ViewBag.UserId = new SelectList(db.Carts, "UserId", "UserId", customer.UserId);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "UserId,Name,Username,Password,Email,MobileNumber")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Entry(customer).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.UserId = new SelectList(db.Carts, "UserId", "UserId", customer.UserId);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
