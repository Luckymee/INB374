using INB374.ServiceReference1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace INB374 {
    class OrderController {

        public static void addOrder(Order order) {
            WebService1SoapClient orderService = new WebService1SoapClient();
            orderService.addOrder(order);
        }

        public static void addOrderDetails(OrderDetails orderDetails){
            WebService1SoapClient orderService = new WebService1SoapClient();
            orderService.addOrderDetails(orderDetails);
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
    }
}
