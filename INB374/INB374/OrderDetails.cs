using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INB374 {
    class OrderDetails {
        public string productCode { get; set; }
        public string orderNumber { get; set; }
        public string quantityOrdered { get; set; }
        public string priceEach { get; set; }

        public OrderDetails()
        {

        }
    }
}
