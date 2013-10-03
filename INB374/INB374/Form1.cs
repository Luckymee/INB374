using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
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
        /* I dont think using global variables is a very good idea but is just so
         difficult to not when working with a gui*/
        private List<ComboBox> productSelectBoxes = new List<ComboBox>();
        private List<Label> productSelectLabels = new List<Label>();
        private List<Product> productsSelected = new List<Product>();
        private List<Product> productList = new List<Product>();
        private List<ComboBox> quantityBoxes = new List<ComboBox>();
        private List<Label> nameLabels = new List<Label>();
        private List<Label> stockLabels = new List<Label>();
        private List<Label> waitingLabels = new List<Label>();
        private List<TextBox> priceTextBoxes = new List<TextBox>();

        public Form1()
        {
            InitializeComponent();
            this.custNum = Convert.ToInt32(CustomerRestController.makeRequest(Constants.CUSTOMER_COUNT_ENDPOINT)) + 1;
            customerNumber.Text = custNum.ToString();
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            // Build Customer object
            Customer customer = new Customer();
            customer.customerNumber = customerNumber.Text;
            customer.customerName = customerName.Text;
            customer.phoneNumber = phone.Text;
            customer.address = addressLine1.Text;
            customer.city = city.Text;
            customer.state = state.Text;
            customer.country = country.Text;
            customer.postCode = postCode.Text;

            // Create XML from customer object.
            CustomerRestController.postXML(Constants.CUSTOMER_ENDPOINT, CustomerRestController.createCustomerXML(customer));

            // Determine next appropriate customer number
            this.custNum = Convert.ToInt32(CustomerRestController.makeRequest(Constants.CUSTOMER_COUNT_ENDPOINT)) + 1;
            customerNumber.Text = custNum.ToString();
        }

        private void customerName_Leave(object sender, EventArgs e)
        {
            /*
            string valEx = "1";
            if (!Regex.IsMatch(this.customerName.Text.Trim(), valEx))
            {
                MessageBox.Show("Wrong!");
            }
            */
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void tabPage2_Enter(object sender, EventArgs e)
        {
            List<string> productDataSource = new List<string>();
            List<string> numberItemsValues= new List<string>();
            productSelectBoxes.Clear();
            productSelectLabels.Clear();

            /* i could not find a better way to do this :S */
            productSelectBoxes.Add(comboBox1);
            productSelectBoxes.Add(comboBox3);
            productSelectBoxes.Add(comboBox4);
            productSelectBoxes.Add(comboBox5);
            productSelectBoxes.Add(comboBox6);

            productSelectLabels.Add(label10);
            productSelectLabels.Add(label14);
            productSelectLabels.Add(label13);
            productSelectLabels.Add(label12);
            productSelectLabels.Add(label11);

            string request = ProductRestController.CreateRequest("test");
            XmlDocument response = ProductRestController.makeRequest(request);
            productList = ProductRestController.ProcessResponse(response);

            for (int i = 0; i < productList.Count; i++)
            {
                productDataSource.Add(String.Format("ProductCode: {0}, ProductName: {1}, QuantitiyInStock: {2}, MRSP: {3}", productList[i].productCode, productList[i].productName, productList[i].quantityInStock, productList[i].msrp));
            }

            for (int i = 0; i < productSelectBoxes.Count; i++) { //add values to drop down lists
                numberItemsValues.Add((i + 1).ToString());
                productSelectBoxes[i].DataSource = productDataSource;
                productSelectBoxes[i].BindingContext = new BindingContext();
            }

            for (int i = 1; i < productSelectBoxes.Count; i++) { //intially hides additional item selections
                productSelectBoxes[i].Hide();
                productSelectLabels[i].Hide();
            }

            comboBox2.DataSource = numberItemsValues;
            comboBox2.SelectedIndex = 0;

        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e) {
            int numberSelected = int.Parse(comboBox2.SelectedValue.ToString());

            for (int i = 1; i < numberSelected; i++) {
                productSelectBoxes[i].Show();
                productSelectLabels[i].Show();
            }

            for (int i = numberSelected; i < productSelectBoxes.Count; i++) {
                productSelectBoxes[i].Hide();
                productSelectLabels[i].Hide();
            }

            updateSummaryTextBox();
        }

        private void updateSummaryTextBox() {
            int numberSelected = 1;
            textBox1.Clear();

            if (comboBox2.SelectedValue != null) {
                numberSelected = int.Parse(comboBox2.SelectedValue.ToString());
            }

            for (int i = 0; i < numberSelected; i++) {
                textBox1.Text += productSelectBoxes[i].Text + "\r\n";
            }
        }

        private void ProcessOrder_Click(object sender, EventArgs e) {

            List<string> quantityItemsValues = new List<string>();         
            int numberSelected = int.Parse(comboBox2.SelectedValue.ToString());

            //clear all the previously selected product details
            quantityBoxes = new List<ComboBox>();
            nameLabels = new List<Label>();
            stockLabels = new List<Label>();
            waitingLabels = new List<Label>();
            priceTextBoxes = new List<TextBox>();

            nameLabels.Add(label20);
            nameLabels.Add(label26);
            nameLabels.Add(label29);
            nameLabels.Add(label32);
            nameLabels.Add(label35);

            stockLabels.Add(label21);
            stockLabels.Add(label25);
            stockLabels.Add(label28);
            stockLabels.Add(label31);
            stockLabels.Add(label34);

            waitingLabels.Add(label23);
            waitingLabels.Add(label24);
            waitingLabels.Add(label27);
            waitingLabels.Add(label30);
            waitingLabels.Add(label33);

            quantityBoxes.Add(comboBox7);
            quantityBoxes.Add(comboBox8);
            quantityBoxes.Add(comboBox9);
            quantityBoxes.Add(comboBox10);
            quantityBoxes.Add(comboBox11);

            priceTextBoxes.Add(textBox2);
            priceTextBoxes.Add(textBox3);
            priceTextBoxes.Add(textBox4);
            priceTextBoxes.Add(textBox5);
            priceTextBoxes.Add(textBox6);

            productsSelected.Clear(); 

            for (int i = 0; i < numberSelected; i++) { //place all products intially chosen in array
                productsSelected.Add(productList[productSelectBoxes[i].SelectedIndex]);
            }
            
            for (int i = 1; i <= 10; i++) { //magic number, data source for quanitity drop downs
                quantityItemsValues.Add((i).ToString());
            }

            for (int i = 0; i < productsSelected.Count; i++) { //shows products and their details selected previously
                priceTextBoxes[i].Show();
                quantityBoxes[i].Show();
                nameLabels[i].Show();
                waitingLabels[i].Show();
                stockLabels[i].Show();

                nameLabels[i].Text = productsSelected[i].productName;
                priceTextBoxes[i].Text = productsSelected[i].msrp;
                waitingLabels[i].Text = "Not Implemented"; //fix once implemented
                quantityBoxes[i].DataSource = quantityItemsValues;
                quantityBoxes[i].BindingContext = new BindingContext();
                updateInStockTextBox(quantityBoxes[i]);
            }

            for (int i = productsSelected.Count; i < priceTextBoxes.Count; i++) { //hide templates not needed
                priceTextBoxes[i].Hide();
                quantityBoxes[i].Hide();
                nameLabels[i].Hide();
                waitingLabels[i].Hide();
                stockLabels[i].Hide();
            }

            tabControl1.SelectedIndex = 2; //go to tab 3
        }

        private void updateInStockTextBox(ComboBox comboBox) {

            int selectedQuantityIndex = 0;;
            int stockRemaining;

            switch (comboBox.Name) {
                case "comboBox7": {
                        selectedQuantityIndex = 0;
                        break;
                    }
                case "comboBox8": {
                        selectedQuantityIndex = 1;
                        break;
                    }
                case "comboBox9": {
                        selectedQuantityIndex = 2;
                        break;
                    }
                case "comboBox10": {
                        selectedQuantityIndex = 3;
                        break;
                    }
                case "comboBox11": {
                        selectedQuantityIndex = 4;
                        break;
                    }
            }

            stockRemaining = int.Parse(productsSelected[selectedQuantityIndex].quantityInStock) - int.Parse(quantityBoxes[selectedQuantityIndex].SelectedValue.ToString());

            if (stockRemaining < 0) {
                stockLabels[selectedQuantityIndex].Text = "No";
            }
            else {
                stockLabels[selectedQuantityIndex].Text = "Yes";
            }
        }

        /* combo box update on change methods */

        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e) {
            updateInStockTextBox(comboBox7);
        }

        private void comboBox8_SelectedIndexChanged(object sender, EventArgs e) {
            updateInStockTextBox(comboBox8);
        }

        private void comboBox9_SelectedIndexChanged(object sender, EventArgs e) {
            updateInStockTextBox(comboBox9);
        }

        private void comboBox10_SelectedIndexChanged(object sender, EventArgs e) {
            updateInStockTextBox(comboBox10);
        }

        private void comboBox11_SelectedIndexChanged(object sender, EventArgs e) {
            updateInStockTextBox(comboBox11);
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e) {
            updateSummaryTextBox();
        }

        private void comboBox3_SelectedIndexChanged(object sender, EventArgs e) {
            updateSummaryTextBox();
        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e) {
            updateSummaryTextBox();
        }

        private void comboBox5_SelectedIndexChanged(object sender, EventArgs e) {
            updateSummaryTextBox();
        }

        private void comboBox6_SelectedIndexChanged(object sender, EventArgs e) {
            updateSummaryTextBox();
        }
    }
}
