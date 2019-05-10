using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabSystem2.Models
{
    public class UserData
    {
        public string NIP { get; set; }

        public string NameAndSurname { get; set; }

        public string City { get; set; }

        public string PostalCode { get; set; }

        [EmailAddress(ErrorMessage = "Błędny format adresu e-mail.")]
        public string Email { get; set; }

        public bool PhonePreferred { get; set; }

        public string Phone { get; set; }

        public string Comment { get; set; }

        public DateTime DateCreated { get; set; }
    }
}
