using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INB374
{
    /**
     * Encapsulates a customer object.
     * Allowing for the easy storage and retrieval of customer information.
     */
    class Customer
    {
        /** Unique identifier for customer. */
        public string customerNumber { get; set; }
        /** Customers chosen name. Can include first and last name. */
        public string customerName { get; set; }
        /** Contact phone number. */
        public string phoneNumber { get; set; }
        /** Customer Address. */
        public string address { get; set; }
        /** Customers City. */
        public string city { get; set; }
        /** Customers State. */
        public string state { get; set; }
        /** Customers Country. */
        public string country { get; set; }
        /** Customers Post Code. */
        public string postCode { get; set; }


        /**
         * Parameterless constructor for Customer.
         */
        public Customer()
        {

        }

    }
}
