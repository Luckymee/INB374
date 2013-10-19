﻿using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace WebApplication2 {
    /// <summary>
    /// Summary description for WebService1
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class WebService1 : System.Web.Services.WebService {

        [WebMethod]
        public string HelloWorld() {
            return "Hello World";
        }

        [WebMethod]
        public void addOrder(Order order) {

            MySqlConnection connection = new MySqlConnection("SERVER=fastws.qut.edu.au;PORT=3306;DATABASE=n8510873;UID=n8510873;PASSWORD=12345;");

            try {
                Debug.WriteLine("Connecting to n8510873");
                connection.Open();

                string query = String.Format("INSERT INTO `n8510873`.`orders` (`orderNumber`, `customers_customerNumber`, `orderDate`, `shippingDate`, `status`, `comments`) VALUES ({0}, {1}, {2}, {3}, '{4}', '{5}')",
                                            getOrderNumber(), order.customers_customerNumber, order.orderDate, order.shippingDate, order.status, order.comments);
                Debug.WriteLine(query);
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();

            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
            }

            connection.Close();
            Debug.WriteLine("Finished");
        }

        [WebMethod]
        public void addOrderDetails(OrderDetails orderDetails) {

            MySqlConnection connection = new MySqlConnection("SERVER=fastws.qut.edu.au;PORT=3306;DATABASE=n8510873;UID=n8510873;PASSWORD=12345;");

            try {
                Debug.WriteLine("Connecting to n8510873");
                connection.Open();

                string query = String.Format("INSERT INTO `n8510873`.`orderdetails` (`orders_orderNumber`, `products_productCode`, `quantityOrdered`, `priceEach`) VALUES ({0}, {1}, {2}, {3})",
                                (int.Parse(getOrderNumber()) - 1), orderDetails.productCode, orderDetails.quantityOrdered, orderDetails.priceEach);
                Debug.WriteLine(query);
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();

            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
            }

            connection.Close();
            Debug.WriteLine("Finished");
        }

        [WebMethod]
        private string getOrderNumber() {
            int orderNum = 1;

            MySqlConnection connection = new MySqlConnection("SERVER=fastws.qut.edu.au;PORT=3306;DATABASE=n8510873;UID=n8510873;PASSWORD=12345;");
            try {
                Debug.WriteLine("Connecting to n8510873");
                connection.Open();

                string query = "SELECT * FROM `n8510873`.`orders`";
                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read()) {
                    orderNum++;
                }
                reader.Close();

            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
            }

            connection.Close();
            Debug.WriteLine("Finished");

            return orderNum.ToString();
        }
    }
}