﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LabSystem2.Models;

namespace LabSystem2.Controllers
{
    public class OrdersController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Orders
        public ActionResult Index()
        {
            var orders = db.Orders.Include(o => o.Customer).Include(o => o.Employee).Include(o => o.Genre).Include(o => o.Product);
            return View(orders.ToList());
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
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name");
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "NameAndSurname");
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name");
            ViewBag.ProductId = new SelectList(db.Productus, "ProductId", "ProductTitle");
            return View();
        }

        // POST: Orders/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,CustomerId,GenreId,ProductId,QuantitySample,MarkingSample,TotalPrice,OrderCreationDate,Comment,OrderState,EmployeeId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", order.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "NameAndSurname", order.EmployeeId);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", order.GenreId);
            ViewBag.ProductId = new SelectList(db.Productus, "ProductId", "ProductTitle", order.ProductId);
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
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", order.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "NameAndSurname", order.EmployeeId);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", order.GenreId);
            ViewBag.ProductId = new SelectList(db.Productus, "ProductId", "ProductTitle", order.ProductId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,CustomerId,GenreId,ProductId,QuantitySample,MarkingSample,TotalPrice,OrderCreationDate,Comment,OrderState,EmployeeId")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "Name", order.CustomerId);
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "NameAndSurname", order.EmployeeId);
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", order.GenreId);
            ViewBag.ProductId = new SelectList(db.Productus, "ProductId", "ProductTitle", order.ProductId);
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
