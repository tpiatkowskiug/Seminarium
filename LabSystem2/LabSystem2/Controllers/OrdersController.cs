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

        private ISessionManager sessionManager { get; set; }

        private ApplicationUserManager _userManager;
        private ShoppingCartManager shoppingCartManager;


        public OrdersController()
        {
            //this.mailService = mailService;

            // Simple way - but hard coupling
            this.sessionManager = new SessionManager();
            this.shoppingCartManager = new ShoppingCartManager(this.sessionManager, this.db);
            // DI way
            //this.sessionManager = sessionManager;
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        // GET: Orders
        public ActionResult Index(string searchQuery = null)
        {
            var orders = db.Orders.Include(o => o.Employee);
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

        public ActionResult Index()
        {
            ShoppingCartManager shoppingCartManager = new ShoppingCartManager(this.sessionManager, this.db);

            var cartItems = shoppingCartManager.GetCart();
            var cartTotalPrice = shoppingCartManager.GetCartTotalPrice();

            CartViewModel cartVM = new CartViewModel() { CartItems = cartItems, TotalPrice = cartTotalPrice };

            return View(cartVM);
        }

        public ActionResult AddToCart(int id)
        {
            ShoppingCartManager shoppingCart = new ShoppingCartManager(this.sessionManager, this.db);
            shoppingCart.AddToCart(id);

            // logger.Info("Added product {0} to cart", id);

            return RedirectToAction("Index");
        }


        public int GetCartItemsCount()
        {
            ShoppingCartManager shoppingCartManager = new ShoppingCartManager(this.sessionManager, this.db);
            return shoppingCartManager.GetCartItemsCount();
        }

        public ActionResult RemoveFromCart(int productID)
        {
            ShoppingCartManager shoppingCartManager = new ShoppingCartManager(this.sessionManager, this.db);

            int itemCount = shoppingCartManager.RemoveFromCart(productID);
            int cartItemsCount = shoppingCartManager.GetCartItemsCount();
            decimal cartTotal = shoppingCartManager.GetCartTotalPrice();

            // Return JSON to process it in JavaScript
            var result = new CartRemoveViewModel
            {
                RemoveItemId = productID,
                RemovedItemCount = itemCount,
                CartTotal = cartTotal,
                CartItemsCount = cartItemsCount
            };

            return Json(result);
        }


        public async Task<ActionResult> Checkout()
        {
            if (Request.IsAuthenticated)
            {
                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                var order = new Order
                {
                    Comment = user.UserData.Comment,
                    DateCreated = user.UserData.DateCreated
                };

                return View(order);
            }
            else
                return RedirectToAction("Checkout", "Cart", new { returnUrl = Url.Action("Checkout", "Cart") });
        }

        [HttpPost]
        public async Task<ActionResult> Checkout(Order orderdetails)
        {
            if (ModelState.IsValid)
            {
                // logger.Info("Checking out");

                // Get user
                var userId = User.Identity.GetUserId();   //identyfiaktor pracownika wprowadzającego zlecenie

                // Save Order
                ShoppingCartManager shoppingCartManager = new ShoppingCartManager(this.sessionManager, this.db);
                var newOrder = shoppingCartManager.CreateOrder(orderdetails, userId);

                // Update profile information
                var user = await UserManager.FindByIdAsync(userId);
                TryUpdateModel(user.UserData);
                await UserManager.UpdateAsync(user);

                // Empty cart
                shoppingCartManager.EmptyCart();

                // Send mail confirmation
                // Refetch - need also albums details
                //var order = db.Orders.Include("OrderItems").SingleOrDefault(o => o.OrderId == newOrder.OrderId);            
                //var order = db.Orders.Include("OrderItems").Include("OrderItems.Album").SingleOrDefault(o => o.OrderId == newOrder.OrderId);


                //IMailService mailService = new HangFirePostalMailService();
                //mailService.SendOrderConfirmationEmail(order);

                // this.mailService.SendOrderConfirmationEmail(order);

                //string url = Url.Action("SendConfirmationEmail", "Cart", new { orderid = newOrder.OrderId, lastname = newOrder.LastName }, Request.Url.Scheme);

                //// Hangfire - nice one (if ASP.NET app will be still running)
                //BackgroundJob.Enqueue(() => Helpers.CallUrl(url));



                //// Strongly typed - without background
                ////OrderConfirmationEmail email = new OrderConfirmationEmail();
                ////email.To = order.Email;
                ////email.Cost = order.TotalPrice;
                ////email.OrderNumber = order.OrderId;
                ////email.FullAddress = string.Format("{0} {1}, {2}, {3}", order.FirstName, order.LastName, order.Address, order.CodeAndCity);
                ////email.OrderItems = order.OrderItems;
                ////email.CoverPath = AppConfig.PhotosFolderRelative;

                //// Loosely typed - without background
                ////dynamic email = new Postal.Email("OrderConfirmation");
                ////email.To = order.Email;
                ////email.Cost = order.TotalPrice;
                ////email.OrderNumber = order.OrderId;
                ////email.FullAddress = string.Format("{0} {1}, {2}, {3}", order.FirstName, order.LastName, order.Address, order.CodeAndCity);
                ////email.OrderItems = order.OrderItems;
                ////email.CoverPath = AppConfig.PhotosFolderRelative;
                ////email.Send();

                //// Easiest background
                ////HostingEnvironment.QueueBackgroundWorkItem(ct => email.Send());

                return RedirectToAction("OrderConfirmation");
            }
            else
                return View(orderdetails);
        }

        public ActionResult OrderConfirmation()
        {
            return View();
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
            ViewBag.EmployeeId = new SelectList(db.OrderItems, "OrderItemId", "OrderItemId");
            ViewBag.EmployeeId = new SelectList(db.Customers, "CustomerId", "NameAndSurnam");
            return View();
        }

        // POST: Orders/Create
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "OrderId,OrderItemId,CustomerId,EmployeeId,DateCreated,Comment,OrderState,TotalPrice")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Orders.Add(order);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "NameAndSurname", order.EmployeeId);
            ViewBag.EmployeeId = new SelectList(db.OrderItems, "OrderItemId", "OrderItemId");
            ViewBag.EmployeeId = new SelectList(db.Customers, "CustomerId", "NameAndSurnam");
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
            ViewBag.EmployeeId = new SelectList(db.OrderItems, "OrderItemId", "OrderItemId");
            ViewBag.EmployeeId = new SelectList(db.Customers, "CustomerId", "NameAndSurnam");
            return View(order);
        }

        // POST: Orders/Edit/5
        // Aby zapewnić ochronę przed atakami polegającymi na przesyłaniu dodatkowych danych, włącz określone właściwości, z którymi chcesz utworzyć powiązania.
        // Aby uzyskać więcej szczegółów, zobacz https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "OrderId,OrderItemId,CustomerId,EmployeeId,DateCreated,Comment,OrderState,TotalPrice")] Order order)
        {
            if (ModelState.IsValid)
            {
                db.Entry(order).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "NameAndSurname", order.EmployeeId);
            ViewBag.EmployeeId = new SelectList(db.OrderItems, "OrderItemId", "OrderItemId");
            ViewBag.EmployeeId = new SelectList(db.Customers, "CustomerId", "NameAndSurnam");
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
