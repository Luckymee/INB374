#define DEBUG
#undef DEBUG
#define PRD
//#undef PRD


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INB374
{
    class Constants
    {
        #if PRD
        public const string CUSTOMER_ENDPOINT = "http://fastws.qut.edu.au:8080/n8510873CustomerDB5823737525019890638/webresources/entities.customers";
        public const string CUSTOMER_COUNT_ENDPOINT = "http://fastws.qut.edu.au:8080/n8510873CustomerDB5823737525019890638/webresources/entities.customers/count";
        public const string PRODUCT_ENDPOINT = "http://fastws.qut.edu.au:8080/n8510873CustomerDB5823737525019890638/webresources/entities.products";
        public const string ORDER_ENDPOINT = "http://fastws.qut.edu.au:8080/n8510873CustomerDB5823737525019890638/webresources/entities.orders";
        public const string ORDERDETAILS_ENDPOINT = "https://fastws.qut.edu.au:8080/n8510873CustomerDB5823737525019890638/webresources/entities.orderdetails";
        #endif
        #if DEBUG
            public const string CUSTOMER_ENDPOINT = "http://localhost:8080/n8510873CustomerDB/webresources/entities.customers/"; 
            public const string CUSTOMER_COUNT_ENDPOINT = "http://localhost:8080/n8510873CustomerDB/webresources/entities.customers/count";
            public const string PRODUCT_ENDPOINT = "http://localhost:8080/n8510873CustomerDB/webresources/entities.products";
            public const string ORDER_ENDPOINT = "http://localhost:8080/n8510873CustomerDB/webresources/entities.orders";
            public const string ORDERDETAILS_ENDPOINT = "http://localhost:8080/n8510873CustomerDB/webresources/entities.orderdetails";
        #endif
    }
}
