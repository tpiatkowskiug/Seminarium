using LabSystem2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LabSystem2.Infrastructure
{
    public interface IMailService
    {
        void SendOrderConfirmationEmail(Order order);
        void SendOrderShippedEmail(Order order);
    }
}
