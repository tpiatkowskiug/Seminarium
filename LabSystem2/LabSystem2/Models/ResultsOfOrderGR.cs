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

        [Display(Name = "Zleceniodawca")]
        public int? CustomerId { get; set; }

        [Display(Name = "Oznakowanie prób")]
        public string OrderMarkingSample { get; set; }

        [Display(Name = "Rodzaj badania")]
        public string OrderPriceList { get; set; }

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

        [Display(Name = "Miedź")]
        [DataType(DataType.MultilineText)]
        public string Cu { get; set; }

        [Display(Name = "Żelazo")]
        [DataType(DataType.MultilineText)]
        public string Fe { get; set; }

        [Display(Name = "Mangan")]
        [DataType(DataType.MultilineText)]
        public string Mn { get; set; }

        [Display(Name = "Cynk")]
        [DataType(DataType.MultilineText)]
        public string Zn { get; set; }

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
        public int? EmployeeId { get; set; }

        public virtual Employee Employee { get; set; }
        public virtual Order Order { get; set; }
        public virtual Customer Customer { get; set; }
    }
}