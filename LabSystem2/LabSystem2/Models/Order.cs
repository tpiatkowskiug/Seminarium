using LabSystem2.Validation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LabSystem2.Models
{
    public class Order : IValidatableObject
    {
        [ScaffoldColumn(false)]
        public int OrderId { get; set; }

        [Display(Name = "Wprowadził zlecenie")]
        public int? EmployeeId { get; set; }
        public virtual ApplicationUser User { get; set; }

        [Display(Name = "NIP")]
        [RegularExpression(@"([0-9]{3})\-?[-\s]?([0-9]{3})\-?[-\s]?([0-9]{3})$",
            ErrorMessage = "Nip zapisany w formacie 000-000-000")]
        public string NIP { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzić imię")]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Musisz wprowadzć nazwisko")]
        [StringLength(150)]
        [Display(Name = "Imię i Nazwisko zleceniodawcy")]
        public string NameAndSurname { get; set; }

        [Required(ErrorMessage = "Wprowadź kod pocztowy i miasto")]
        [StringLength(50)]
        [Display(Name = "Miasto/Wieś")]
        public string City { get; set; }

        [Display(Name = "Kod pocztowy")]
        [RegularExpression(@"([0-9]{2})\-?[-\s]?([0-9]{3})$",
            ErrorMessage = "Kod pocztowy zapisany w formacie 00-000")]
        public string PostalCode { get; set; }

        [Required(ErrorMessage = "Wprowadź swój adres e-mail.")]
        [EmailAddress(ErrorMessage = "Błędny format adresu e-mail.")]
        public string Email { get; set; }

        [Display(Name = "Preferowany kontakt telefoniczy")]
        public bool PhonePreferred { get; set; }

        [Display(Name = "Numer telefonu:")]
        [RequiredIfTrue(BooleanPropertyName = "PhonePreferred", ErrorMessage = "Skoro preferujesz kontakt telefoniczny, musisz podać numer.")]
        [Phone]
        [RegularExpression(@"([\+]){0,1}([0-9]{2})?[\-\s]?[-]?([0-9]{3})\-?[-\s]?([0-9]{3})[-\s]\-?([0-9]{3})$",
            ErrorMessage = "Numer musi być zapisany w formacie 000-000-000")]
        public string Phone { get; set; }

        public string Comment { get; set; }

        [Display(Name = "Data zlecenia")]
        [UIHint("DataTimePicker")]                       //dynamiczny helper
        public DateTime DateCreated { get; set; }
        public OrderState OrderState { get; set; }

        public decimal TotalPrice { get; set; }

        public List<OrderItem> OrderItems { get; set; }  //informacja o kolekcji elemtnów, które składają się 
                                                         //na zlecenie badań
        public virtual Employee Employee { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (this.Email.Contains("spam"))
                yield return new ValidationResult("Podany e-mail nie wygląda na prawidłowy.", new string[] { "Email" });
        }

    }

    public enum OrderState
    {
        [Display(Name = "nowe")]
        New,

        [Display(Name = "zrobione")]
        Shipped
    }
}