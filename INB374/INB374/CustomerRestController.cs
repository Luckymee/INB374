using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Xml;

namespace INB374
{
    class CustomerRestController
    {
        public static XmlDocument createCustomerXML(Customer customer)
        {
            nullChecker(customer);

            XmlDocument customerRecord = new XmlDocument();
            XmlElement el = (XmlElement)customerRecord.AppendChild(customerRecord.CreateElement("customers"));
            el.AppendChild(customerRecord.CreateElement("addressLine1")).InnerText = customer.address;
            el.AppendChild(customerRecord.CreateElement("city")).InnerText = customer.city;
            el.AppendChild(customerRecord.CreateElement("country")).InnerText = customer.country;
            el.AppendChild(customerRecord.CreateElement("customerName")).InnerText = customer.customerName;
            el.AppendChild(customerRecord.CreateElement("customerNumber")).InnerText = customer.customerNumber;
            el.AppendChild(customerRecord.CreateElement("phone")).InnerText = customer.phoneNumber;
            el.AppendChild(customerRecord.CreateElement("postCode")).InnerText = customer.postCode;
            el.AppendChild(customerRecord.CreateElement("state")).InnerText = customer.state;

            return (customerRecord);
        }

        public static string postXML(string postURL, XmlDocument xmlDoc)
        {
            // Create the request
            WebRequest request = WebRequest.Create(postURL);

            // Set header parameters
            request.ContentType = "application/xml";
            request.Method = "POST";

            // Encode the string
            byte[] bytes = Encoding.UTF8.GetBytes(xmlDoc.OuterXml);

            using (Stream outputStream = request.GetRequestStream())
            {
                outputStream.Write(bytes, 0, bytes.Length);
            }

            try
            {
                using (WebResponse response = request.GetResponse())
                {
                    if (response == null) return (null);

                    using (StreamReader streamReader = new StreamReader(response.GetResponseStream()))
                    {
                        return streamReader.ReadToEnd().Trim();
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return (null);
            }

        }

        private static void nullChecker(Customer customer)
        {
            customer.address =          (string.IsNullOrEmpty(customer.address))        ? "N/A" : customer.address;
            customer.city =             (string.IsNullOrEmpty(customer.city))           ? "N/A" : customer.city;
            customer.country =          (string.IsNullOrEmpty(customer.country))        ? "N/A" : customer.country;
            customer.customerName =     (string.IsNullOrEmpty(customer.customerName))   ? "N/A" : customer.customerName;
            customer.customerNumber =   (string.IsNullOrEmpty(customer.customerNumber)) ? "N/A" : customer.customerNumber;
            customer.phoneNumber =      (string.IsNullOrEmpty(customer.phoneNumber))    ? "N/A" : customer.phoneNumber;
            customer.state =            (string.IsNullOrEmpty(customer.state))          ? "N/A" : customer.state;
            customer.postCode =         (string.IsNullOrEmpty(customer.postCode))       ? "N/A" : customer.postCode;
        }

        public static string makeRequest(string requestURL)
        {
            try
            {
                WebClient webClient = new WebClient();
                string readHtml = webClient.DownloadString(requestURL);

                return readHtml;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return null;
            }
        }

    }



}
