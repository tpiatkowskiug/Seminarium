using System;
using System.Collections.Generic;
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
        public int ProductId { get; set; }
        public int Quantity { get; set; }
        public string UnitPrice { get; set; }

        public virtual Product Product { get; set; }
        public virtual Order Order { get; set; }
    }
}