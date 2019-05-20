using LabSystem2.DAL;
using LabSystem2.Models;
using LabSystem2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.Owin;
using LabSystem2.Infrastructure;
using System.Web.Hosting;
using System.Net;
//using Hangfire;
using NLog;
using LabSystem2;

namespace LabSystem2.Controllers
{
    public class CartController : Controller
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        private ApplicationDbContext db = new ApplicationDbContext();

        private ISessionManager sessionManager { get; set; }

       // private IMailService mailService { get; set; }

        private ApplicationUserManager _userManager;
        private ShoppingCartManager shoppingCartManager;

        public CartController()
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

        public ActionResult Index()
        {
            ShoppingCartManager shoppingCartManager = new ShoppingCartManager(this.sessionManager, this.db);

            var cartItems = shoppingCartManager.GetCart();
            var cartTotalPrice = shoppingCartManager.GetCartTotalPrice();

            CartViewModel cartVM = new CartViewModel() { CartItems = cartItems, TotalPrice = cartTotalPrice };

            return View(cartVM);
        }
        [Authorize(Roles = "Employee")]
        public ActionResult AddToCart(int id)
        {
            ShoppingCartManager shoppingCart = new ShoppingCartManager(this.sessionManager, this.db);
            shoppingCart.AddToCart(id);

            logger.Info("Added product {0} to cart", id);

             return RedirectToAction("Index");
        }

        [HttpPost]
        public JsonResult GetCustomer(int CustomerId)
        {
            var query = db.Customers.ToList().Where(c => c.CustomerId == CustomerId).FirstOrDefault();
            return Json(query.Email, JsonRequestBehavior.AllowGet);
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
            decimal cartItemTotal = shoppingCartManager.GetCartItemPrice();
            decimal cartTotal = shoppingCartManager.GetCartTotalPrice();
            string productName = db.Productus
                    .Single(item => item.ProductId == productID).ProductTitle;
            // Return JSON to process it in JavaScript
            var result = new CartRemoveViewModel
            {
                Message = Server.HtmlEncode(productName) +
                    " has been removed from your shopping cart.",
          
                RemoveItemId = productID,
                RemovedItemCount = itemCount,
                CartTotal = cartTotal,
                CartItemsCount = cartItemsCount,
                CartItemUnitPrice = cartItemTotal
            };

            return Json(result);
        }

        [HttpPost]
        public ActionResult UpdateCartCount(int id, int cartCount)
        {
            CartRemoveViewModel results = null;
            try
            {
                ShoppingCartManager shoppingCartManager = new ShoppingCartManager(this.sessionManager, this.db);

                int itemCount = shoppingCartManager.UpdateCartCount(id, cartCount);
                int cartItemsCount = shoppingCartManager.GetCartItemsCount();
                decimal cartItemTotal = shoppingCartManager.GetCartItemPrice();
                decimal cartTotal = shoppingCartManager.GetCartTotalPrice();
                // Get the name of the album to display confirmation 
                string productName = db.Productus
                    .Single(item => item.ProductId == id).ProductTitle;

                //Prepare messages
                string msg = "The quantity of " + Server.HtmlEncode(productName) +
                        " has been refreshed in your shopping cart.";
                if (itemCount == 0) msg = Server.HtmlEncode(productName) +
                        " has been removed from your shopping cart.";
                // Display the confirmation message 
                results = new CartRemoveViewModel
                {
                    Message = msg,
                    RemoveItemId = id,
                    RemovedItemCount = itemCount,
                    CartTotal = cartTotal,
                    CartItemUnitPrice = cartItemTotal
                };
            }
            catch
            {
                results = new CartRemoveViewModel
                {
                    Message = "Error occurred or invalid input...",
                    RemovedItemCount = -1,
                    RemoveItemId = id,
                    CartTotal = -1,
                    CartItemUnitPrice = -1
                };
            }
            return Json(results);
        }

        public async Task<ActionResult> Checkout()
        {
            if (Request.IsAuthenticated)
            {

                var user = await UserManager.FindByIdAsync(User.Identity.GetUserId());

                ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "NameAndSurname");
                ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "NameAndSurname");

                var order = new Order
                {

                };

                return View(order);
            }
            else
                return RedirectToAction("Login", "Account", new { returnUrl = Url.Action("Checkout", "Cart") });
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

                ViewBag.CustomerId = new SelectList(db.Customers, "CustomerId", "NameAndSurname", orderdetails.CustomerId);
                ViewBag.EmployeeId = new SelectList(db.Employees, "EmployeeId", "NameAndSurname", orderdetails.EmployeeId);

                var user = await UserManager.FindByIdAsync(userId);
                //TryUpdateModel(user.UserData);
                await UserManager.UpdateAsync(user);

                // Empty cart
                shoppingCartManager.EmptyCart();

                // Send mail confirmation
                // Refetch - need also albums details
                //var order = db.Orders.Include("OrderItems").SingleOrDefault(o => o.OrderId == newOrder.OrderId);            
                var order = db.Orders.Include("OrderItems").Include("OrderItems.Product").SingleOrDefault(o => o.OrderId == newOrder.OrderId);


                //IMailService mailService = new HangFirePostalMailService();
                //mailService.SendOrderConfirmationEmail(order);

                // this.mailService.SendOrderConfirmationEmail(order);

                //string url = Url.Action("SendConfirmationEmail", "Cart", new { orderid = newOrder.OrderId, lastname = newOrder.LastName }, Request.Url.Scheme);

                //// Hangfire - nice one (if ASP.NET app will be still running)
                //BackgroundJob.Enqueue(() => Helpers.CallUrl(url));



                // Strongly typed - without background
                OrderConfirmationEmail email = new OrderConfirmationEmail();
                email.To = order.Email;
                email.Cost = order.TotalPrice;
                email.OrderNumber = order.OrderId;
              //  email.FullAddress = string.Format("{0} {1}, {2}, {3}", order.Customer.NIP, order.Customer.NameAndSurname, order.Customer.Address, order.Customer.City);
                email.OrderItems = order.OrderItems;
                email.CoverPath = AppConfig.PhotosFolderRelative;

                //Loosely typed -without background
                //dynamic email = new Postal.Email("OrderConfirmation");
                //email.To = order.Customer.Email;
                //email.Cost = order.TotalPrice;
                //email.OrderNumber = order.OrderId;
                //email.FullAddress = string.Format("{0} {1}, {2}, {3}", order.Customer.NIP, order.Customer.NameAndSurname, order.Customer.Address, order.Customer.City);
                //email.OrderItems = order.OrderItems;
                //email.CoverPath = AppConfig.PhotosFolderRelative;
                //email.Send();

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


    }
};