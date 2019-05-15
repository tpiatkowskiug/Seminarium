using LabSystem2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabSystem2.ViewModels
{
    public class CartViewModel
    {
        public List<CartItem> CartItems { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}