using System;
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
    public class ResultsOfOrderGRsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: ResultsOfOrderGRs
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Cuastomer")]
        [Authorize(Roles = "Employee")]
        public ActionResult Index()
        {
            var getResultsOfOrderGRs = db.GetResultsOfOrderGRs.Include(r => r.Employee).Include(r => r.Order);
            return View(getResultsOfOrderGRs.ToList());
        }

        // GET: ResultsOfOrderGRs/Details/5
        [Authorize(Roles = "Admin")]
        [Authorize(Roles = "Cuastomer")]
        [Authorize(Roles = "Employee")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultsOfOrderGR resultsOfOrderGR = db.GetResultsOfOrderGRs.Find(id);
            if (resultsOfOrderGR == null)
            {
                return HttpNotFound();
            }
            return View(resultsOfOrderGR);
        }

        // GET: ResultsOfOrderGRs/Create
        [Authorize(Roles = "Lab")]
        public ActionResult Create()
        {
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "NameAndSurname");
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "OrderId");
            return View();
        }

        // POST: ResultsOfOrderGRs/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Lab")]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ResultsOfOrderGRId,OrderId,OrderMarkingSample,WynikiBadanpH,WynikiBadanFosfor,WynikiBadanPotas,WynikiBadanMagnez,AddOrder,Cu,Fe,Mn,Zn,Bor,InneBadania,ResultsCreationDate,EmployeeId")] ResultsOfOrderGR resultsOfOrderGR)
        {
            if (ModelState.IsValid)
            {
                db.GetResultsOfOrderGRs.Add(resultsOfOrderGR);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "NameAndSurname", resultsOfOrderGR.EmployeeId);
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "OrderId", resultsOfOrderGR.OrderId);
            return View(resultsOfOrderGR);
        }

        // GET: ResultsOfOrderGRs/Edit/5
        [Authorize(Roles = "Lab")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultsOfOrderGR resultsOfOrderGR = db.GetResultsOfOrderGRs.Find(id);
            if (resultsOfOrderGR == null)
            {
                return HttpNotFound();
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "NameAndSurname", resultsOfOrderGR.EmployeeId);
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "EmployeeId", resultsOfOrderGR.OrderId);
            return View(resultsOfOrderGR);
        }

        // POST: ResultsOfOrderGRs/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Lab")]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ResultsOfOrderGRId,OrderId,OrderMarkingSample,WynikiBadanpH,WynikiBadanFosfor,WynikiBadanPotas,WynikiBadanMagnez,AddOrder,Cu,Fe,Mn,Zn,Bor,InneBadania,ResultsCreationDate,EmployeeId")] ResultsOfOrderGR resultsOfOrderGR)
        {
            if (ModelState.IsValid)
            {
                db.Entry(resultsOfOrderGR).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "NameAndSurname", resultsOfOrderGR.EmployeeId);
            ViewBag.OrderId = new SelectList(db.Orders, "OrderId", "EmployeeId", resultsOfOrderGR.OrderId);
            return View(resultsOfOrderGR);
        }

        // GET: ResultsOfOrderGRs/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ResultsOfOrderGR resultsOfOrderGR = db.GetResultsOfOrderGRs.Find(id);
            if (resultsOfOrderGR == null)
            {
                return HttpNotFound();
            }
            return View(resultsOfOrderGR);
        }

        // POST: ResultsOfOrderGRs/Delete/5
        [HttpPost, ActionName("Delete")]
        [Authorize(Roles = "Admin")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            ResultsOfOrderGR resultsOfOrderGR = db.GetResultsOfOrderGRs.Find(id);
            db.GetResultsOfOrderGRs.Remove(resultsOfOrderGR);
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
