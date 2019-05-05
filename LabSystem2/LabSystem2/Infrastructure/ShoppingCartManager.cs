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

            var cartItem = cart.Find(c => c.Product.ProductId == productid);

            if (cartItem != null)
                cartItem.Quantity++;
            else
            {
                // Find product and add it to cart
                var albumToAdd = db.Productus.Where(a => a.ProductId == productid).SingleOrDefault();
                if (albumToAdd != null)
                {
                    var newCartItem = new CartItem()
                        {
                            Product = albumToAdd,
                            Quantity = 1,
                            TotalPrice = albumToAdd.PriceBrutto
                        };

                    cart.Add(newCartItem);
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
            return cart.Sum(c => (c.Quantity * c.Product.PriceBrutto));
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

            newOrder.DateCreated = DateTime.Now;
            //newOrder.UserId = userId;

            this.db.Orders.Add(newOrder);

            if (newOrder.OrderItems == null)
                newOrder.OrderItems = new List<OrderItem>();

            decimal cartTotal = 0;

            foreach (var cartItem in cart)
            {                
                var newOrderItem = new OrderItem()
                {
                    ProductId = cartItem.Product.ProductId,                    
                    Quantity = cartItem.Quantity,
                    UnitPrice = cartItem.Product.PriceBrutto
                };

                cartTotal += (cartItem.Quantity * cartItem.Product.PriceBrutto);

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