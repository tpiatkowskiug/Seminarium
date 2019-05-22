using LabSystem2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace LabSystem2.ViewModels
{
    public class CartViewModel
    {

        [DisplayName("Przedmiot badań")]
        public int? GenreId { get; set; }
        [DisplayName("Zakres badań")]
        public int? ProductId { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual Product Product { get; set; }
        public List<CartItem> CartItems { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
    }
}