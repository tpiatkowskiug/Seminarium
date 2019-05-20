using LabSystem2.Infrastructure;
using LabSystem2.Models;
using LabSystem2.ViewModels;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LabSystem2.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private ICacheProvider cache = new DefaultCacheProvider();

        private static Logger logger = LogManager.GetCurrentClassLogger();

        public ActionResult Index()
        {

            logger.Info("Visited main page");

            var bestsellers = db.Productus.Where(a => a.IsBestseller && !a.IsHidden).OrderBy(g => Guid.NewGuid()).Take(3);

            List<Product> newArrivals;

            //ICacheProvider cache = new DefaultCacheProvider();
            if (cache.IsSet(Consts.NewItemsCacheKey))
            {
                newArrivals = cache.Get(Consts.NewItemsCacheKey) as List<Product>;
            }
            else
            {
                newArrivals = db.Productus.Where(a => !a.IsHidden).OrderByDescending(a => a.DateAdded).Take(3).ToList();
                cache.Set(Consts.NewItemsCacheKey, newArrivals, 30);
            }

            //var newArrivals = db.Albums.Where(a => !a.IsHidden).OrderByDescending(a => a.DateAdded).Take(3).ToList();

            var genres = db.Genres;

            var vm = new HomeViewModel() { Genres = genres, Bestsellers = bestsellers, NewArrivals = newArrivals };

            return View(vm);
            //var genres = db.Genres;



            //var newArrivals = db.Productus.Where(a => !a.IsHidden).OrderByDescending(a => a.DateAdded).Take(3).ToList();

            //var bestsellers = db.Productus.Where(a => a.IsBestseller && !a.IsHidden).OrderBy(g => Guid.NewGuid()).Take(3);

            //var vm = new HomeViewModel() { Genres = genres, Bestsellers = bestsellers, NewArrivals = newArrivals };

            //return View(vm);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult StaticContent(string viewname)
        {
            return View(viewname);
        }
    }
}