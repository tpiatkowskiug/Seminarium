using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LabSystem2.ViewModels
{
    public class CartRemoveViewModel
    {
        public string Message
        {
            get;
            set;
        }

        public decimal CartTotal
        {
            get;
            set;
        }

        public int CartItemsCount
        {
            get;
            set;
        }

        public int RemovedItemCount
        {
            get;
            set;
        }

        public int RemoveItemId
        {
            get;
            set;
        }
    }
}