using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Services;
using warehouseService.supplierService;

namespace warehouseService
{
    /// <summary>
    /// Summary description for warehouseWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class warehouseWebService : System.Web.Services.WebService
    {
        [WebMethod]
        public string getWaitTime(string productCode)
        {

            string waitTime = "-1";

            MySqlConnection connection = new MySqlConnection("SERVER=fastws.qut.edu.au;PORT=3306;DATABASE=n8510873;UID=n8510873;PASSWORD=12345;");

            try
            {
                Debug.WriteLine("Connecting to n8510873");
                connection.Open();

                string query = String.Format("SELECT deliveryTime FROM n8510873.warehouse WHERE products_productCode = '{0}'", productCode);

                MySqlCommand cmd = new MySqlCommand(query, connection);

                waitTime = cmd.ExecuteScalar().ToString();

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            connection.Close();
            Debug.WriteLine("Finished getWaitTime");

            return waitTime;
        }

        [WebMethod]
        public string getSupplierWaitTime(string productCode)
        {
            supplierWebServiceSoapClient supplier = new supplierWebServiceSoapClient();
            return (supplier.getWaitTime(productCode));
        }

        [WebMethod]
        public string orderStock()
        {
            MySqlConnection connection = new MySqlConnection("SERVER=fastws.qut.edu.au;PORT=3306;DATABASE=n8510873;UID=n8510873;PASSWORD=12345;");
            // This list is needed as the web service will not take a normal list for some reason
            // as such we must build an array from this once we know the size.
            List<OrderQueue> temporarylist = new List<OrderQueue>();
            int index = 0;
            try
            {
                Debug.WriteLine("Connecting to n8510873");
                connection.Open();

                // This block gets the number of products needed to be ordered and stores that value.
                string query = String.Format("SELECT COUNT(*) FROM n8510873.products WHERE quantityInStock <= '3';");
                MySqlCommand cmd = new MySqlCommand(query, connection);
                cmd.ExecuteNonQuery();
                MySqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    index = Int32.Parse(reader[0].ToString());

                }
                reader.Close();

                // This block gets the product codes for the products that need ordering.
                // Done like this as I don't want to do complex sql.
                query = String.Format("SELECT productCode FROM n8510873.products WHERE quantityInStock <= '3';");

                cmd = new MySqlCommand(query, connection);
                reader = cmd.ExecuteReader();

                int i = 0;
                while (reader.Read())
                {

                    OrderQueue orderQueue = new OrderQueue();
                    orderQueue.productCode = reader[0].ToString();
                    temporarylist.Add(orderQueue);
                    i++;

                }
                reader.Close();

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }


            connection.Close();

            // Create the array to send, now that we know the size.
            OrderQueue[] productCode = temporarylist.ToArray<OrderQueue>();
            supplierWebServiceSoapClient supplierRequest = new supplierWebServiceSoapClient();

            if (index > 0)
            {
                List<Product> products = new List<Product>(supplierRequest.restockWarehouse(productCode));
                sendStock(products);
            }

            Debug.WriteLine("Finished orderStock");

            return "Successful";

        }

        private void sendStock(List<Product> productList)
        {
            MySqlConnection connection = new MySqlConnection("SERVER=fastws.qut.edu.au;PORT=3306;DATABASE=n8510873;UID=n8510873;PASSWORD=12345;");

            try
            {
                Debug.WriteLine("Connecting to n8510873");
                connection.Open();

                //string query = String.Format("SELECT deliveryTime FROM n8510873.warehouse WHERE products_productCode = '{0}'");

                for (int i = 0; i < productList.Count; ++i)
                {
                    String query = String.Format("UPDATE products SET productName = '{1}', quantityInStock= quantityInStock + '{2}', MSRP='{3}' WHERE productCode = '{0}';",
                        productList[i].productCode,
                        productList[i].productName,
                        productList[i].quantityInStock,
                        productList[i].msrp);

                    MySqlCommand cmd = new MySqlCommand(query, connection);
                    cmd.ExecuteNonQuery();
                }

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            connection.Close();
            Debug.WriteLine("Finished sendStock");

        }
    }
}
