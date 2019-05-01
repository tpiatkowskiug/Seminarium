using LabSystem2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabSystem2.ViewModels
{
    public class HomeViewModel
    {
        public IEnumerable<Product> Bestsellers { get; set; }
        public IEnumerable<Product> NewArrivals { get; set; }
        public IEnumerable<Genre> Genres { get; set; }
    }
}