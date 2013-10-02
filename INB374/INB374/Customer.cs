using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INB374
{
    class Customer
    {
        public string customerNumber { get; set; }
        public string customerName { get; set; }
        public string phoneNumber { get; set; }
        public string address { get; set; }
        public string city { get; set; }
        public string state { get; set; }
        public string country { get; set; }
        public string postCode { get; set; }

        public Customer()
        {

        }

        public string printCustomer()
        {
            return (this.customerName);
        }

    }
}
