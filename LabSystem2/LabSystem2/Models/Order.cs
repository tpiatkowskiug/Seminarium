using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabSystem2.Models
{
    public class Order
    {
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }

        [Display(Name = "Zleceniodawca")]
        public int? CustomerId { get; set; }

        [Display(Name = "Ilość prób")]
        public int QuantitySample { get; set; }

        [Display(Name = "Oznakowanie prób")]
        [DataType(DataType.MultilineText)]
        public string MarkingSample { get; set; }

        [Display(Name = "Rodzaj badań")]
        public int? GenreId { get; set; }

        [Display(Name = "Szczegóły")]
        public int? ProductId { get; set; }

        [Display(Name = "Cena razem")]
        public decimal TotalPrice { get; set; }

        [Display(Name = "Data zlecenia")]
        [UIHint("DataTimePicker")]            //dynamiczny helper
        public DateTime OrderCreationDate { get; set; }

        public string Comment { get; set; }
        
        public OrderState OrderState { get; set; }

        public List<OrderItem> OrderItems { get; set; }

        [Display(Name = "Wprowadził zlecenie")]
        public int? EmployeeId { get; set; }


        public virtual Employee Employee { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual Genre Genre { get; set; }
        public virtual Product Product { get; set; }
    }

    public enum OrderState
    {
        [Display(Name = "nowe")]
        New,

        [Display(Name = "zrobione")]
        Shipped
    }
}