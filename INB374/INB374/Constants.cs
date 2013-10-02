#define DEBUG
#define PRD
#undef PRD


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INB374
{
    class Constants
    {
        #if PRD
            public const string CUSTOMER_ENDPOINT = "QUT"; 
            public const string CUSTOMER_COUNT_ENDPOINT = "QUT";
            public const string PRODUCT_ENDPOINT = "QUT";
        #endif
        #if DEBUG
            public const string CUSTOMER_ENDPOINT = "http://localhost:8080/n8510873CustomerDB/webresources/entities.customers/"; 
            public const string CUSTOMER_COUNT_ENDPOINT = "http://localhost:8080/n8510873CustomerDB/webresources/entities.customers/count";
            public const string PRODUCT_ENDPOINT = "http://localhost:8080/n8510873CustomerDB/webresources/entities.products";
        #endif
    }
}
