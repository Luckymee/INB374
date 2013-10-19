using INB374.supplierService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INB374 {
    class SupplierController {
        public static string getWaitTime(string productCode) {
            supplierWebServiceSoapClient supplier = new supplierWebServiceSoapClient();
            string waitTime = supplier.getWaitTime(productCode);

            return waitTime;
        }
    }
}
