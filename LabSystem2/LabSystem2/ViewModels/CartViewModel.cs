using LabSystem2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabSystem2.ViewModels
{
    public class CartViewModel
    {
        public List<CartItem> CartItems { get; set; }

        public decimal TotalPrice { get; set; }
    }
}