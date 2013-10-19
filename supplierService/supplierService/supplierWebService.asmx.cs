using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web;
using System.Web.Services;

namespace supplierService
{
    /// <summary>
    /// Summary description for supplierWebService
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
    // [System.Web.Script.Services.ScriptService]
    public class supplierWebService : System.Web.Services.WebService
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

                string query = String.Format("SELECT deliveryTime FROM n8510873.supplier WHERE products_productCode = '{0}'", productCode);

                MySqlCommand cmd = new MySqlCommand(query, connection);

                waitTime = cmd.ExecuteScalar().ToString();

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }

            connection.Close();
            Debug.WriteLine("Finished");

            return waitTime;
        }

        [WebMethod]
        public List<Product> restockWarehouse(List<OrderQueue> orderQueue)
        {
            MySqlConnection connection = new MySqlConnection("SERVER=fastws.qut.edu.au;PORT=3306;DATABASE=n8510873;UID=n8510873;PASSWORD=12345;");

            List<Product> shipProducts = new List<Product>();

            try
            {
                Debug.WriteLine("Connecting to n8510873");
                connection.Open();

                string query = String.Format("SELECT * FROM n8510873.supplier;");

                MySqlCommand cmd = new MySqlCommand(query, connection);
                MySqlDataReader reader = cmd.ExecuteReader();

                // Generates comparison list of product codes.
                List<string> tempProductCodeList = new List<string>();

                for (int i = 0; i < orderQueue.Count; ++i)
                {
                    tempProductCodeList.Add(orderQueue[i].productCode);
                }

                while (reader.Read())
                {
                    Product product = new Product();
                    product.productCode = reader[0].ToString();
                    // Found product code in comparison list.
                    if (tempProductCodeList.Contains(product.productCode))
                    {
                        product.productName = reader[1].ToString();
                        product.msrp = reader[4].ToString();
                        product.quantityInStock = reader[5].ToString();
                        shipProducts.Add(product);
                    }
                }
                reader.Close();

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);
            }


            connection.Close();

            Debug.WriteLine("Finished restock warehouse.");

            return (shipProducts);
        }
    }
}
