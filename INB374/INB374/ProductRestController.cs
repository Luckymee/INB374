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
            string urlRequest = Constants.PRODUCT_ENDPOINT;
            
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

        /**
         * Process response
         * 
         *@param response - XMLDocument: built from product database respone.
         *@return products - Tuple: Contains lists of product variables.
         */
        public static List<Product> ProcessResponse(XmlDocument response)
        {
            XmlNodeList name = response.SelectNodes("//products");
            List<Product> products= new List<Product>();

            foreach (XmlNode xn in name) {
                Product tempProduct = new Product();
                tempProduct.productName = xn["productName"].InnerText;
                tempProduct.productCode = xn["productCode"].InnerText;
                tempProduct.quantityInStock = xn["quantityInStock"].InnerText;
                tempProduct.msrp = xn["msrp"].InnerText;
                products.Add(tempProduct);
            }

            return (products);
        }
    }
}
