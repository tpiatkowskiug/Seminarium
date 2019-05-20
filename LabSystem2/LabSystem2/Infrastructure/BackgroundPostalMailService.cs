using LabSystem2.ViewModels;
using LabSystem2.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Hosting;

namespace LabSystem2.Infrastructure
{
    public class BackgroundPostalMailService : IMailService
    {
        public void SendOrderConfirmationEmail(Models.Order order)
        {
            HostingEnvironment.QueueBackgroundWorkItem(ct => 
                {
                    OrderConfirmationEmail email = new OrderConfirmationEmail();
                    email.To = order.Email;
                    email.Cost = order.TotalPrice;
                    email.OrderNumber = order.OrderId;
                    email.ResultsOfOrderGRList = order.ResultsOfOrderGRList;
                    email.OrderItems = order.OrderItems;
                    email.CoverPath = AppConfig.PhotosFolderRelative;

                    email.Send();
                });
        }

        public void SendOrderShippedEmail(Models.Order order)
        {
            OrderShippedEmail email = new OrderShippedEmail();
            email.To = order.Email;
            email.OrderId = order.OrderId;
            email.ResultsOfOrderGRList = order.ResultsOfOrderGRList;

            HostingEnvironment.QueueBackgroundWorkItem(ct => email.Send());
        }
    }
}
