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
            return View();
        }

        public ActionResult List(string genrename)
        {
            var genre = db.Genres.Include("Products").Where(g => g.Name.ToUpper() == genrename.ToUpper()).Single();
            var products = genre.Products.ToList();
            return View(products);
        }

        [ChildActionOnly]
        [OutputCache(Duration = 86400)]
        public ActionResult GenresMenu()
        {
            var genres = db.Genres;

            return PartialView(genres);
        }
    }
}