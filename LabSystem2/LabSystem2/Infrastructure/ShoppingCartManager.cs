using LabSystem2.DAL;
using LabSystem2.Infrastructure;
using LabSystem2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabSystem2.Infrastructure
{
    public class ShoppingCartManager
    {
        public const string CartSessionKey = "CartData";

        private ApplicationDbContext db;

        private ISessionManager session;

        public ShoppingCartManager(ISessionManager session, ApplicationDbContext db)
        {
            this.session = session;
            this.db = db;
        }

        public void AddToCart(int orderitemid)
        {
            var cart = this.GetCart();

            //var cartItem = cart.Find(c => c.Product.ProductId == productid);

            var cartItem = cart.Find(c => c.OrderItem.OrderItemId == orderitemid);
            if (cartItem != null)
                cartItem.Quantity++;
            else
            {
                // Find product and add it to cart
                var orderToAdd = db.OrderItems.Where(a => a.OrderItemId == orderitemid).SingleOrDefault();
                //var productToAdd = db.Productus.Where(a => a.ProductId == orderitemid).SingleOrDefault();
                if (orderToAdd != null)
                {
                    var newCartItem = new CartItem()
                        { 
                            OrderItem = orderToAdd,
                            Quantity = 1,
                            TotalPrice = orderToAdd.UnitPrice
                        };

                    cart.Add(newCartItem);
                }
            }

            session.Set(CartSessionKey, cart);
        }

        public int RemoveFromCart(int orderitemid)
        {
            var cart = this.GetCart();

            var cartItem = cart.Find(c => c.OrderItem.OrderItemId == orderitemid);

            if (cartItem != null)
            {
                if (cartItem.Quantity > 1)
                {
                    cartItem.Quantity--;
                    return cartItem.Quantity;
                }
                else
                    cart.Remove(cartItem);
            }

            // Return count of removed item currently inside cart
            return 0;
        }


        public List<CartItem> GetCart()
        {
            List<CartItem> cart;

            if (session.Get<List<CartItem>>(CartSessionKey) == null)
            {
                cart = new List<CartItem>();
            }
            else
            {
                cart = session.Get<List<CartItem>>(CartSessionKey) as List<CartItem>;
            }

            return cart;
        }

        public decimal GetCartTotalPrice()
        {
            var cart = this.GetCart();
            return cart.Sum(c => (c.Quantity * c.OrderItem.UnitPrice));
        }

        public int GetCartItemsCount()
        {
            var cart = this.GetCart();
            int count = cart.Sum(c => c.Quantity);

            return count;
        }

        public Order CreateOrder(Order newOrder, string userId)
        {
            var cart = this.GetCart();

           // newOrder.DateCreated = DateTime.Now;
            newOrder.EmployeeId = userId;

            this.db.Orders.Add(newOrder);

            if (newOrder.OrderItems == null)
                newOrder.OrderItems = new List<OrderItem>();

            decimal cartTotal = 0;

            foreach (var cartItem in cart)
            {
                var newOrderItem = new OrderItem()
                {
                    OrderItemId = cartItem.OrderItem.OrderItemId,
                    Quantity = cartItem.OrderItem.Quantity,
                    UnitPrice = cartItem.OrderItem.UnitPrice
                };

                cartTotal += (cartItem.Quantity * cartItem.OrderItem.UnitPrice);

                newOrder.OrderItems.Add(newOrderItem);
            }

            newOrder.TotalPrice = cartTotal;

            this.db.SaveChanges();

            return newOrder;
        }

        public void EmptyCart()
        {
            session.Set<List<CartItem>>(CartSessionKey, null);
        }

    }
}