using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using LabSystem2.Infrastructure;
using LabSystem2.Models;

namespace LabSystem2.Controllers
{
    public class OrderItemsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: OrderItems
        public ActionResult Index()
        {
            var orderItems = db.OrderItems.Include(o => o.Genre).Include(o => o.Product);
            return View(orderItems.ToList());
        }

        // GET: OrderItems/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderItem = db.OrderItems.Find(id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            return View(orderItem);
        }

        // GET: OrderItems/Create
        [Authorize(Roles = "Employee")]
        public ActionResult Create()
        {

            List<Genre> GenreList = db.Genres.ToList();
            ViewBag.GenreList = new SelectList(GenreList, "GenreId", "Name");
            ViewBag.ProductId = new SelectList(db.Productus, "ProductId", "ProductTitle");
            return View();
            //return RedirectToAction("Index", "Cart");
        }

        // POST: OrderItems/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Employee")]
        public ActionResult Create([Bind(Include = "OrderItemId,GenreId,ProductId,Quantity,MarkingSample,UnitPrice")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                db.OrderItems.Add(orderItem);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", orderItem.GenreId);
            ViewBag.ProductId = new SelectList(db.Productus, "ProductId", "ProductTitle", orderItem.ProductId);
            return View(orderItem);
        }


        [HttpPost]
        public JsonResult GetProductPrice(int ProductId)
        {
            // var Productlist = GetProducts(); //query database and get the Product.
            //based on IDProduct to get the product.
            var query = db.Productus.ToList().Where(c => c.ProductId == ProductId).FirstOrDefault();
            return Json(query.PriceBrutto, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetProductsByCategoryId(int GenreId)
        {
            db.Configuration.ProxyCreationEnabled = false;
            List<Product> ProductList = db.Productus.Where(x => x.GenreId == GenreId).ToList();
            //var result = db.Genres.ToList().Where(c => c.GenreId == GenreId).FirstOrDefault();
     
            return Json(ProductList, JsonRequestBehavior.AllowGet);
        }

        // GET: OrderItems/Edit/5
        [Authorize(Roles = "Employee")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderItem = db.OrderItems.Find(id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", orderItem.GenreId);
            ViewBag.ProductId = new SelectList(db.Productus, "ProductId", "ProductTitle", orderItem.ProductId);
            return View(orderItem);
        }

        // POST: OrderItems/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderItemId,GenreId,ProductId,Quantity,MarkingSample,UnitPrice")] OrderItem orderItem)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderItem).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.GenreId = new SelectList(db.Genres, "GenreId", "Name", orderItem.GenreId);
            ViewBag.ProductId = new SelectList(db.Productus, "ProductId", "ProductTitle", orderItem.ProductId);
            return View(orderItem);
        }

        // GET: OrderItems/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderItem orderItem = db.OrderItems.Find(id);
            if (orderItem == null)
            {
                return HttpNotFound();
            }
            return View(orderItem);
        }

        // POST: OrderItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            OrderItem orderItem = db.OrderItems.Find(id);
            db.OrderItems.Remove(orderItem);
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
