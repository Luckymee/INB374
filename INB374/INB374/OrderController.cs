//using INB374.ServiceReference1;
using INB374.orderService;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace INB374 {
    class OrderController {

        public static void addOrder(Order order) {

            orderWebServiceSoapClient orderService = new orderWebServiceSoapClient();
            try
            {
                orderService.addOrder(order);
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }
        }

        public static void addOrderDetails(OrderDetails orderDetails, int quantityStocked){
            orderWebServiceSoapClient orderService = new orderWebServiceSoapClient();
            orderService.addOrderDetails(orderDetails, quantityStocked);
        }

        public static OrderDetails createOrderDetails(string code, string quantity, string price) {
            OrderDetails details = new OrderDetails();

            details.productCode = code;
            details.quantityOrdered = quantity;
            details.priceEach = price;

            return details;
        }

        public static Order createOrder(string customerNum, string orderDate, string shippingDate, string status, string comments) {
            Order order = new Order();

            order.customers_customerNumber = customerNum;
            order.orderDate = orderDate;
            order.shippingDate = shippingDate;
            order.status = status;
            order.comments = comments;

            return order;
        }

        public static void updateExisitingOrders() {
            orderWebServiceSoapClient orderService = new orderWebServiceSoapClient();
            orderService.updateExistingOrders();
        }
    }
}
