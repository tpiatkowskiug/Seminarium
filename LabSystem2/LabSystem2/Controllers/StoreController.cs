using LabSystem2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabSystem2.Controllers
{
    public class StoreController : Controller
    {
        ApplicationDbContext db = new ApplicationDbContext();
        // GET: Store
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Details(int id)
        {
            var product = db.Productus.Find(id);
                        
            return View(product);
        }

        public ActionResult DetailsOrderItem(int id)
        {
            var orderItem = db.OrderItems.Find(id);

            return View(orderItem);
        }

        public ActionResult List(string genrename, string searchQuery = null)
        {
            var genre = db.Genres.Include("Products").Where(g => g.Name.ToUpper() == genrename.ToUpper()).Single();
            var products = genre.Products.Where(a => (searchQuery == null ||
                                                a.ProductTitle.ToLower().Contains(searchQuery.ToLower()) ||
                                                a.ProductTitle.ToLower().Contains(searchQuery.ToLower())) &&
                                                !a.IsHidden);

            if (Request.IsAjaxRequest())
            {
                return PartialView("_ProductList", products);
            }

            return View(products);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 86400)]
        public ActionResult GenresMenu()
        {
            var genres = db.Genres;

            return PartialView(genres);
        }

        public ActionResult ProductSuggestions(string term)
        {
            var products = this.db.Productus.Where(a => a.ProductTitle.ToLower().Contains(term.ToLower()) && !a.IsHidden).Take(3).Select(a => new { label = a.ProductTitle });

            return Json(products, JsonRequestBehavior.AllowGet);
        }
    }
}