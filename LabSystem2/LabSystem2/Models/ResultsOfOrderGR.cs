using LabSystem2.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabSystem2.Models
{
    public class ResultsOfOrderGR
    {
        [ScaffoldColumn(false)]
        public int ResultsOfOrderGRId { get; set; }

        [Display(Name = "Dotyczy zlecenia")]
        public int OrderId { get; set; }

        [Display(Name = "Przedmiot badań")]
        public List<OrderItem> OrderItems { get; set; }

        [Display(Name = "Oznakowanie prób")]
        public string OrderMarkingSample { get; set; }

        [Display(Name = "Wyniki badań pH")]
        [DataType(DataType.MultilineText)]
        public string WynikiBadanpH { get; set; }

        [Display(Name = "Wyniki badań Fosfor")]
        [DataType(DataType.MultilineText)]
        public string WynikiBadanFosfor { get; set; }

        [Display(Name = "Wyniki badań Potas ")]
        [DataType(DataType.MultilineText)]
        public string WynikiBadanPotas { get; set; }

        [Display(Name = "Wyniki badań Magnez")]
        [DataType(DataType.MultilineText)]
        public string WynikiBadanMagnez { get; set; }

        [Display(Name = "Dodatkowe badania")]
        public bool AddOrder { get; set; }

        [RequiredIfTrue(BooleanPropertyName = "AddOrder", ErrorMessage = "Dodatkowe badania")]
        [Display(Name = "Miedź")]
        [DataType(DataType.MultilineText)]
        public string Cu { get; set; }

        [RequiredIfTrue(BooleanPropertyName = "AddOrder", ErrorMessage = "Dodatkowe badania")]
        [Display(Name = "Żelazo")]
        [DataType(DataType.MultilineText)]
        public string Fe { get; set; }

        [RequiredIfTrue(BooleanPropertyName = "AddOrder", ErrorMessage = "Dodatkowe badania")]
        [Display(Name = "Mangan")]
        [DataType(DataType.MultilineText)]
        public string Mn { get; set; }

        [RequiredIfTrue(BooleanPropertyName = "AddOrder", ErrorMessage = "Dodatkowe badania")]
        [Display(Name = "Cynk")]
        [DataType(DataType.MultilineText)]
        public string Zn { get; set; }

        [RequiredIfTrue(BooleanPropertyName = "AddOrder", ErrorMessage = "Dodatkowe badania")]
        [Display(Name = "Bor")]
        [DataType(DataType.MultilineText)]
        public string Bor { get; set; }

        [Display(Name = "Inne badania")]
        [DataType(DataType.MultilineText)]
        public string InneBadania { get; set; }

        [Display(Name = "Data wykonania zlecenia")]
        [UIHint("DataTimePicker")]
        public DateTime ResultsCreationDate { get; set; }

        [Display(Name = "Wprowadził wyniki")]
        public string EmployeeId { get; set; }
        public virtual ApplicationUser UserEmployee { get; set; }

        public OrderStateRusalt OrderStateRusalt { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Order Order { get; set; }


    }

    public enum OrderStateRusalt
    {
        [Display(Name = "nowe")]
        New,

        [Display(Name = "zrealizowane")]
        Shipped
    }
}