using Hangfire;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace LabSystem2.Infrastructure
{
    public class HangFirePostalMailService : IMailService
    {
        public void SendOrderConfirmationEmail(Models.Order order)
        {            
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            string url = urlHelper.Action("SendConfirmationEmail", "Manage", new { orderid = order.OrderId, lastname = order.Email }, HttpContext.Current.Request.Url.Scheme);

            // Hangfire - nice one (if ASP.NET app will be still running)
            BackgroundJob.Enqueue(() => Helpers.CallUrl(url));
        }

        public void SendOrderShippedEmail(Models.Order order)
        {
            var urlHelper = new UrlHelper(HttpContext.Current.Request.RequestContext);
            string url = urlHelper.Action("SendStatusEmail", "Manage", new { orderid = order.OrderId, lastname = order.Email }, HttpContext.Current.Request.Url.Scheme);

            BackgroundJob.Enqueue(() => Helpers.CallUrl(url));
        }
    }
}
