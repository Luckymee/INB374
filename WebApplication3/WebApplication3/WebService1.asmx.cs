using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;
using System.Web;
using System.Web.Services;
using MySql.Data.MySqlClient;

namespace WebApplication3 {
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
        public string getWaitTime(string productCode) {

            string waitTime = "-1";

            MySqlConnection connection = new MySqlConnection("SERVER=fastws.qut.edu.au;PORT=3306;DATABASE=n8510873;UID=n8510873;PASSWORD=12345;");

            try {
                Debug.WriteLine("Connecting to n8510873");
                connection.Open();

                string query = String.Format("SELECT deliveryTime FROM n8510873.warehouse WHERE products_productCode = '{0}'", productCode);
                
                MySqlCommand cmd = new MySqlCommand(query, connection);

                waitTime = cmd.ExecuteScalar().ToString();

            }
            catch (Exception e) {
                Debug.WriteLine(e.Message);
            }

            connection.Close();
            Debug.WriteLine("Finished");

            return waitTime;
        }
    }
}
