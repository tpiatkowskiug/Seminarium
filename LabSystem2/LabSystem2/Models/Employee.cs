using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabSystem2.Models
{
    public class Employee
    {
        [ScaffoldColumn(false)]
        public int EmployeeId { get; set; }

        [Required(ErrorMessage = "Wprowadź imię")]
        [Display(Name = "Imię i nazwisko")]
        public string NameAndSurname { get; set; }

        [Display(Name = "Email:")]
        [EmailAddress]
        public string Email { get; set; }

        [Phone]
        [RegularExpression(@"([\+]){0,1}([0-9]{2})?[\-\s]?[-]?([0-9]{3})\-?[-\s]?([0-9]{3})[-\s]\-?([0-9]{3})$",
        ErrorMessage = "Numer musi być zapisany w formacie 123-123-123")]
        public string PhoneNumber { get; set; }

        public string Region { get; set; }


        public virtual ICollection<Order> Orders { get; set; } //  ile zleceń wprwadził pracwonik
        public virtual ICollection<Customer> Customers { get; set; } //ilu klientów wprowadził praconwików
    }
}