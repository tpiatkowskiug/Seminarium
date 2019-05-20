using LabSystem2.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabSystem2.Infrastructure
{
    public class PostalMailService : IMailService
    {
        public void SendOrderConfirmationEmail(Models.Order order)
        {
            // Strongly typed - without background
            OrderConfirmationEmail email = new OrderConfirmationEmail();
            email.To = order.Email;
            email.Cost = order.TotalPrice;
            email.OrderNumber = order.OrderId;
           //email.OrderItems = order.OrderItems;
            email.ResultsOfOrderGRList = order.ResultsOfOrderGRList;
            email.CoverPath = AppConfig.PhotosFolderRelative;
            email.Send();
        }

        public void SendOrderShippedEmail(Models.Order order)
        {
            OrderShippedEmail email = new OrderShippedEmail();
            email.To = order.Email;
            email.OrderId = order.OrderId;
            email.ResultsOfOrderGRList = order.ResultsOfOrderGRList;
            email.Send();
        }
    }
}
