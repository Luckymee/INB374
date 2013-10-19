using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace orderService
{
    public class Order
    {
        public string orderNumber { get; set; }
        public string customers_customerNumber { get; set; }
        public string orderDate { get; set; }
        public string shippingDate { get; set; }
        public string status { get; set; }
        public string comments { get; set; }

        public Order()
        {

        }
    }
}