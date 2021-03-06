﻿using LabSystem2.DAL;
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

        public void AddToCart(int productid)
        {
            var cart = this.GetCart();

            var cartItem = cart.Find(c => c.Product.ProductId == productid); //szukamy czy dany produktów jest już dodany
            if (cartItem != null)                                                  //jeśli jest zwiększamy liczbę
                cartItem.Quantity++;
            else
            {
                // Find product and add it to cart
                var productToAdd = db.Productus.Where(a => a.ProductId == productid).SingleOrDefault();

                if (productToAdd != null)
                {
                    var newCartItem = new CartItem()
                        {
                          //  Genre = genretToAdd,
                            Product = productToAdd,
                            Quantity = 1,
                            TotalPrice = productToAdd.PriceBrutto,
                            UnitPrice = productToAdd.PriceBrutto
                    };

                    cart.Add(newCartItem);  //dodajemy do listy sesji
                }
            }

            session.Set(CartSessionKey, cart);
        }

        public int RemoveFromCart(int productid)
        {
            var cart = this.GetCart();

            var cartItem = cart.Find(c => c.Product.ProductId == productid);

            if (cartItem != null)
            {
                //if (cartItem.Quantity > 1)
                //{
                //    cartItem.Quantity--;
                //    return cartItem.Quantity;
                //}
                //else
                    cart.Remove(cartItem);
            }

            // Return count of removed item currently inside cart
            return 0;
        }

        public int UpdateCartCount(int id, int cartCount)
        {
            var cart = this.GetCart();

            var cartItem = cart.Find(c => c.Product.ProductId == id);

            int itemCount = 0;

            if (cartItem != null)
            {
                if (cartCount > 0)
                {
                    cartItem.Quantity = cartCount;
                    itemCount = cartItem.Quantity;
                }
                else
                {
                    cart.Remove(cartItem);
                }
       
            }
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
            return cart.Sum(c => (c.Quantity * c.Product.PriceBrutto));         //CartItem model
        }


        public decimal GetCartItemPrice()
        {
            var cart = this.GetCart();
            return cart.Sum(c => (c.Quantity * c.Product.PriceBrutto));         //CartItem model
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
          //  newOrder.OrderId = orderId;

            this.db.Orders.Add(newOrder);

            if (newOrder.OrderItems == null)
                newOrder.OrderItems = new List<OrderItem>();

            decimal cartTotal = 0;

            foreach (var cartItem in cart)
            {
                var newOrderItem = new OrderItem()
                {
                    ProductId = cartItem.Product.ProductId,
                    OrderId = newOrder.OrderId,
                    UnitPrice = (cartItem.Quantity * cartItem.Product.PriceBrutto),
                    Quantity = cartItem.Quantity
                };

                cartTotal += (cartItem.Quantity * cartItem.Product.PriceBrutto);
                //cartTotal += cartItem.UnitPrice;
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