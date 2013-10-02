using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml;

namespace INB374
{
    public partial class Form1 : Form
    {
        private int custNum;

        public Form1()
        {
            InitializeComponent();
            this.custNum = Convert.ToInt32(CustomerRestController.makeRequest("http://localhost:8080/n8510873CustomerDB/webresources/entities.customers/count")) + 1;
            customerNumber.Text = custNum.ToString();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Determine next appropriate customer number
            Customer customer = new Customer();
            customer.customerNumber = customerNumber.Text;
            customer.customerName = customerName.Text;
            customer.phoneNumber = phone.Text;
            customer.address = addressLine1.Text;
            customer.city = city.Text;
            customer.state = state.Text;
            customer.country = country.Text;
            customer.postCode = postCode.Text;

            Constants.Test = "12";

            MessageBox.Show(Constants.customerEndpoint);

            CustomerRestController.postXML(Constants.customerEndpoint, CustomerRestController.createCustomerXML(customer));

            this.custNum = Convert.ToInt32(CustomerRestController.makeRequest("http://localhost:8080/n8510873CustomerDB/webresources/entities.customers/count")) + 1;
            customerNumber.Text = custNum.ToString();
        }

        private void customerName_Leave(object sender, EventArgs e)
        {
            string valEx = "1";
            if (!Regex.IsMatch(this.customerName.Text.Trim(), valEx))
            {
                MessageBox.Show("Wrong!");
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            List<string> test = new List<string>();

            string request = ProductRestController.CreateRequest("test");
            XmlDocument response = ProductRestController.makeRequest(request);

            var helper = ProductRestController.ProcessResponse(response);

            for (int i = 0; i < helper.Item4.Count; i++)
            {
                test.Add(String.Format("ProductCode: {0}, ProductName: {1}, QuantitiyInStock: {2}, MRSP: {3}", helper.Item1[i], helper.Item2[i], helper.Item3[i], helper.Item4[i]));
            }

            comboBox1.DataSource = test;


        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            textBox1.Text = comboBox1.Text;
        }
    }
}
