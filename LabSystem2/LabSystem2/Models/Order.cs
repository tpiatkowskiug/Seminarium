using LabSystem2.Validation;
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

        public int OrderItemId { get; set; }

        public int CustomerId { get; set; }

        [Display(Name = "Wprowadził zlecenie")]
        public string EmployeeId { get; set; }
        public virtual ApplicationUser UserEmployee { get; set; }

        [Display(Name = "Data zlecenia")]
        [UIHint("DataTimePicker")]                       //dynamiczny helper
        public DateTime DateCreated { get; set; }

        public string Comment { get; set; }

        public OrderState OrderState { get; set; }

        public decimal TotalPrice { get; set; }

        public List<OrderItem> OrderItems { get; set; }  //informacja o kolekcji elemtnów, które składają się 
                                                         //na zlecenie badań
        public virtual Employee Employee { get; set; }
        public virtual OrderItem OrderItem { get; set; }
        public virtual Customer Customer { get; set; }

    }

    public enum OrderState
    {
        [Display(Name = "nowe")]
        New,

        [Display(Name = "zrealizowane")]
        Shipped
    }
}