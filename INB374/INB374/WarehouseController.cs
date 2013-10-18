using INB374.warehouseService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INB374 {
    class WarehouseController {
        public static string getWaitTime(string productCode) {
            WebService1SoapClient warehouse = new WebService1SoapClient();
            string waitTime = warehouse.getWaitTime(productCode);

            return waitTime;
        }
    }
}
