using INB374.warehouseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INB374 {
    class WarehouseController {
        public static string getWaitTime(string productCode) {
            warehouseWebServiceSoapClient warehouse = new warehouseWebServiceSoapClient();
            string waitTime = warehouse.getWaitTime(productCode);

            return waitTime;
        }

        public static string getSupplierWaitTime(string productCode)
        {
            warehouseWebServiceSoapClient warehouse = new warehouseWebServiceSoapClient();
            string waitTime = warehouse.getSupplierWaitTime(productCode);

            return waitTime;
        }
    }
}
