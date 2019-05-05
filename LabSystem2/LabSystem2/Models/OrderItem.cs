using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabSystem2.Models
{
    public class OrderItem
    {
        /// <summary>
        /// zamówienie jednostkowe klienta, kolekjca elementów
        /// składająca się na zamówienie
        /// </summary>
        public int OrderItemId { get; set; } 
        public int OrderId { get; set; }

        [Display(Name = "Rodzaj badań")]
        public int? GenreId { get; set; }

        [Display(Name = "Szczegóły")]
        public int? ProductId { get; set; }

        [Display(Name = "Ilość prób")]
        public int Quantity { get; set; }

        [Display(Name = "Oznakowanie prób")]
        [DataType(DataType.MultilineText)]
        public string MarkingSample { get; set; }

        public decimal UnitPrice { get; set; }

        public virtual Genre Genre { get; set; }
        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}