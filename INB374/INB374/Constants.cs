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
        public static string customerEndpoint { set; get; }
        public static string customerCountEndpoint { set; get; }
        private static string test = "test";

        public static string Test
        {
            get { return Constants.test; }
            set { Constants.test = value; }
        }


        static Constants()
        {
            #if PRD
                customerEndpoint = "QUT";
                customerCountEndpoint = "QUT";
            #endif
            #if DEBUG
                customerEndpoint = "http://localhost:8080/n8510873CustomerDB/webresources/entities.customers/";
                customerCountEndpoint = "http://localhost:8080/n8510873CustomerDB/webresources/entities.customers/count";
            #endif
        }



    }
}
