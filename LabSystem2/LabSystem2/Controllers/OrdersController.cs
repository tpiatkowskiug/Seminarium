using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LabSystem2.Models;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using LabSystem2.Infrastructure;
using LabSystem2.ViewModels;

namespace LabSystem2.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();


        // GET: Orders
        public ActionResult Index(string searchQuery = null)
        {
            var orders = db.Orders.Include(o => o.Employee).Include(o => o.Customer);
           // return View(orders.ToList());

            IEnumerable<Order> personList;

            if (searchQuery != null)
                personList = db.Orders.ToList().Where(p => p.Customer.NameAndSurname.Contains(searchQuery) || p.Customer.NameAndSurname.Contains(searchQuery) || searchQuery == p.Customer.NameAndSurname + " " + p.Customer.NameAndSurname).ToArray();
            else
            {
                personList = db.Orders.ToList().ToArray();
            }

            if (Request.IsAjaxRequest())
            {
                return PartialView("_PersonList", personList);
            }
            return View(personList);

        }

        public ActionResult PersonSuggestions(string term)
        {
            var personList = db.Orders.ToList().Where(p => p.Customer.NameAndSurname.Contains(term) || p.Customer.NameAndSurname.Contains(term)).Take(5).Select(p => new { label = p.Customer.NameAndSurname + " " + p.Customer.NameAndSurname });

            return Json(personList, JsonRequestBehavior.AllowGet);
        }



        // GET: Orders/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // GET: Orders/Create
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "NameAndSurname");
            ViewBag.EmployeeId = new SelectList(db.Customers, "CustomerId", "NameAndSurnam");
            return View();
        }

        // POST: Orders/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,CustomerId,EmployeeId,DateCreated,Comment,OrderState,TotalPrice")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "NameAndSurname", order.EmployeeId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "NameAndSurnam", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "NameAndSurname", order.EmployeeId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "NameAndSurnam", order.CustomerId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,CustomerId,EmployeeId,DateCreated,Comment,OrderState,TotalPrice")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "NameAndSurname", order.EmployeeId);
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "NameAndSurnam", order.CustomerId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Order order = db.Orders.Find(id);
            if (order == null)
            {
                return HttpNotFound();
            }
            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Order order = db.Orders.Find(id);
            db.Orders.Remove(order);
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
