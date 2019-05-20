﻿using Postal;
using LabSystem2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabSystem2.ViewModels
{
    public class OrderConfirmationEmail : Email
    {
        public string To { get; set; }
        public decimal Cost { get; set; }
        public int OrderNumber { get; set; }
        public string FullAddress { get; set; }
        public List<OrderItem> OrderItems { get; set; }
        public string CoverPath { get; set; }
    }

    public class OrderShippedEmail : Email
    {
        public string To { get; set; }
        public int OrderId { get; set; }
        public string FullAddress { get; set; }
        public List<ResultsOfOrderGR> ResultsOfOrderGRList { get; set; }
    }
}
