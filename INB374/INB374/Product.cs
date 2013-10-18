using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INB374 {
    /** 
     * Encapsulates a product, holding all relevate fields.
     */
    class Product {

        /** Products name. */
        public string productName { get; set; }
        /** Unique identifier for product. */
        public string productCode { get; set; }
        /** Quantity of product in stock, either instore or warehouse. */
        public string quantityInStock { get; set; }
        /** Recommeded retail price. */
        public string msrp { get; set; }
        /** Item kept in store. */
        public bool inStore { get; set; }

        /** Paramterless constructor*/
        public Product() {

        }
    }
}
