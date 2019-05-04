using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabSystem2.Models
{
    public class Product
    {
        [ScaffoldColumn(false)]
        public int ProductId { get; set; }
        
        public int GenreId { get; set; }

        [Display(Name = "Nazwa")]
        [Required]
        public string ProductTitle { get; set; }
        [UIHint("DataTimePicker")]
        public DateTime DateAdded { get; set; }
        public string Description { get; set; }
        public decimal PriceNetto { get; set; }
        public decimal PriceBrutto { get; set; }
        public bool IsHidden { get; set; }
        public bool IsBestseller { get; set; }
        public string CoverFileName { get; set; }

        public virtual Genre Genre { get; set; } //właściwość nawigacyjna
        
       
    }
}