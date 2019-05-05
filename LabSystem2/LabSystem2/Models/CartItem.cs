using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabSystem2.Models
{
    public class CartItem
    {
        [Display(Name = "Rodzaj badań")]
        public int? GenreId { get; set; }

        [Display(Name = "Szczegóły")]
        public int? ProductId { get; set; }

        public int Quantity { get; set; }

        [Display(Name = "Oznakowanie prób")]
        [DataType(DataType.MultilineText)]
        public string MarkingSample { get; set; }

        public decimal UnitPrice { get; set; }

        public decimal TotalPrice { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Product Product { get; set; }
    }
}