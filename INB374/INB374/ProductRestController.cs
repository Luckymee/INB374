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

        /**
         * Creates an XML doc with the response from a request for a product.
         * 
         *@param requestURL - string: The restful url to be parsed for its response.
         *@return xmlResponse - XmlDocument: A valid xml containing the requested products.
         */
        public static XmlDocument makeProductRequest(string requestURL)
        {
            try
            {
                HttpWebRequest request = WebRequest.Create(requestURL) as HttpWebRequest;
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;


                XmlDocument xmlResponse = new XmlDocument();

                xmlResponse.Load(response.GetResponseStream());

                return (xmlResponse);
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
         *@return products - List<Product>: A list of product variables.
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
                tempProduct.inStore = Convert.ToBoolean(xn["inStore"].InnerText);
                products.Add(tempProduct);
            }

            return (products);
        }
    }
}
