using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;
using System.Windows.Forms;

namespace INB374
{
    class ProductRestController
    {
        public static string CreateRequest(string queryString)
        {
            string urlRequest = "http://localhost:8080/n8510873CustomerDB/webresources/entities.products";

            return (urlRequest);
        }

        public static XmlDocument makeRequest(string requestURL)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestURL) as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;


                XmlDocument xmlDoc = new XmlDocument();

                xmlDoc.Load(response.GetResponseStream());

                return (xmlDoc);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);

                Console.Read();
                return null;
            }
        }

        public static Tuple<List<string>, List<string>, List<string>, List<string>> ProcessResponse(XmlDocument response)
        {
            XmlNodeList name = response.SelectNodes("//products");

            List<string> productName = new List<string>();
            List<string> productCode = new List<string>();
            List<string> quantityInStock = new List<string>();
            List<string> MSRP = new List<string>();

            foreach (XmlNode xn in name)
            {
               productName.Add(xn["productName"].InnerText);
               productCode.Add(xn["productCode"].InnerText);
               quantityInStock.Add(xn["quantityInStock"].InnerText);
               MSRP.Add(xn["msrp"].InnerText);
            }

            // This is some black magic. Create something like a struct, passing a tuple full of lists, full of product variables.
            // Reqiures .NET 4.0
            var products = Tuple.Create(productCode, productName, quantityInStock, MSRP);

            return (products);
        }
    }
}
