using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace orderService
{
    public class OrderDetails
    {
        public string orders_orderNumber { get; set; }
        public string productCode { get; set; }
        public string quantityOrdered { get; set; }
        public string priceEach { get; set; }

        public OrderDetails()
        {

        }
    }
}