using INB374.warehouseService;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows.Forms;
using System.Xml;

/** \mainpage Slightly Over Price Appliances
 *
 * \section intro_sec Introduction
 *
 * This is the introduction.
 *
 * \section Info Information
 *
 * \subsection step1 Step 1: Sub Information
 *
 * etc...
 */




namespace INB374
{
    /**
     * Primary client for Slightly Overpriced Appliances.
     * This class encapsulates all of the form logic and is the main point of driving,
     * for all user intraction.
     */
    public partial class SOA : Form
    {
        /** Stores customer number. */
        private int custNum;
        /** Stores selected customer for use in ordering */
        private int customerInContext;
        private List<ComboBox> productSelectBoxes = new List<ComboBox>();
        private List<Label> productSelectLabels = new List<Label>();
        private List<Product> productsSelected = new List<Product>();
        private List<Product> productList = new List<Product>();
        private List<ComboBox> quantityBoxes = new List<ComboBox>();
        private List<Label> nameLabels = new List<Label>();
        private List<Label> stockLabels = new List<Label>();
        private List<Label> waitingLabels = new List<Label>();
        private List<TextBox> priceTextBoxes = new List<TextBox>();

        /** Front-end client inital constructor. */
        public SOA()
        {
            InitializeComponent();
            this.custNum = Convert.ToInt32(CustomerRestController.makeRequest(Constants.CUSTOMER_COUNT_ENDPOINT)) + 1;
            customerNumber.Text = custNum.ToString();
            tabControl1.TabPages[2].Text = "Order Summary";
            tabControl1.TabPages[2].Enabled = false;
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        /**
         * Proccess' user inputed details about new customer.
         * Building a customer object and passing it via a restful service/
         * 
         */
        private void addCustomerButton_Click(object sender, EventArgs e)
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

            // Require name.
            if (customerName.Text == string.Empty)
            {
                MessageBox.Show("Please enter a customer name!");
                return;
            }

            // Check for numerics in Name.
            string valEx = "^[A-Za-z]+$";
            if (!Regex.IsMatch(this.customerName.Text.Trim(), valEx))
            {
                MessageBox.Show("Please enter a name, without numerical characters.");
                return;
            }

            // Determine if customer added succesfully.
            // Null if not
            try
            {
                // Create XML from customer object.
                string customerToAdd = CustomerRestController.postXML(Constants.CUSTOMER_ENDPOINT, CustomerRestController.createCustomerXML(customer));
                if (customerToAdd == null)
                {
                    throw new ArgumentNullException();
                }
               
                customerAddStatus.Text = String.Format("Added Customer: {0} to database.", customer.customerNumber);
            }
            catch (Exception exception)
            {
                Debug.WriteLine(exception.Message);
                customerAddStatus.Text = "Failed to add Customer to database.";
            }
            // Determine next appropriate customer number
            this.custNum = Convert.ToInt32(CustomerRestController.makeRequest(Constants.CUSTOMER_COUNT_ENDPOINT)) + 1;
            customerNumber.Text = custNum.ToString();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        /**
         * Entered the order screen.
         * Dynamically creates a selection of combo boxes filled via a service to obtain products
         * from the database.
         */
        private void tabPage2_Enter(object sender, EventArgs e)
        {
            List<string> productDataSource = new List<string>();
            List<string> numberItemsValues= new List<string>();
            List<string> customers = new List<string>();
            int numCustomers = int.Parse(CustomerRestController.makeRequest(Constants.CUSTOMER_COUNT_ENDPOINT));
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

            for (int i = 1; i < numCustomers + 1; ++i)
            {
                customers.Add((i).ToString());
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

            customerContextBox.DataSource = customers;
            customerContextBox.SelectedIndex = 0;
 
            comboBox2.DataSource = numberItemsValues;
            comboBox2.SelectedIndex = 0;

        }

        /**
         * Entered the order confirmation screen.
         */
        private void tabPage3_Enter(object sender, EventArgs e) {
        }

       /**
        * Number of items to select for an order.
        * Dynamically shows or hides a set amount of comboboxes for product input,
        * based on selected number.
        */
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

        /**
         * Updates order summary with selected items.
         * Called via each combo box selected index method.
         */
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

        /**
         * Method for controlling which customer to attach orders to.
         * 
         */
        private void updateCustomerInContext()
        {
            if (customerContextBox.SelectedValue != null)
            {
                customerInContext = int.Parse(customerContextBox.SelectedValue.ToString());
            }
            else
            {
                customerInContext = 0;
            }

            
        }

        /**
         * Process Order
         * Dynamically builds a list of selected items, on the Order confirmation screen.
         * This is to allow the sales person to choose a quantity and determine, whether the item
         * is in stock, and what its delivery time will be.
         */
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
                waitingLabels[i].Text = getWaitingTime(productsSelected[i], Convert.ToInt32(productsSelected[i].quantityInStock) > 0);
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

            warehouseWebServiceSoapClient warehouse = new warehouseWebServiceSoapClient();
            warehouse.orderStock();

            tabControl1.SelectedIndex = 2; //go to tab 3
            tabControl1.TabPages[2].Enabled = true;
            label39.Text = String.Format("Customer: {0}",customerInContext.ToString());
        }

        private string getWaitingTime(Product product, bool inStock) {
            bool inStore = product.inStore;
            string waitTime = "";

            switch (inStore) {
                case true: {
                    waitTime = "In Store";
                    break;
                }
                case false: {
                    switch (inStock) {
                        case true: {
                            waitTime = addDays(WarehouseController.getWaitTime(product.productCode));
                            break;
                        }
                        case false: {
                            waitTime = addDays((Convert.ToInt32(WarehouseController.getWaitTime(product.productCode)) + Convert.ToInt32(SupplierController.getWaitTime(product.productCode))).ToString());
                            break;
                        }
                    }
                    break;      
                }
            }

            return waitTime;
        }

        private string addDays(string waitTime) {
            string concatString = "";

            if (Convert.ToInt32(waitTime) > 1) {
                concatString = waitTime + " days";
            }
            else {
                concatString = waitTime + " day";
            }

            return concatString;
        }

        /**
         * TODO
         */
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

            calculateOrderTotal();
            priceTextBoxes[selectedQuantityIndex].Text = (double.Parse(productsSelected[selectedQuantityIndex].msrp) * double.Parse(quantityBoxes[selectedQuantityIndex].SelectedValue.ToString())).ToString();

            stockRemaining = int.Parse(productsSelected[selectedQuantityIndex].quantityInStock) - int.Parse(quantityBoxes[selectedQuantityIndex].SelectedValue.ToString());

            if (stockRemaining < 0) {
                stockLabels[selectedQuantityIndex].Text = "No";
                waitingLabels[selectedQuantityIndex].Text = getWaitingTime(productsSelected[selectedQuantityIndex], false);
            }
            else {
                stockLabels[selectedQuantityIndex].Text = "Yes";
            }
        }

        /**
         * Calculates the order total based on the selected items and their quanitites.
         * Fired on 
         */
        private void calculateOrderTotal() {
            double total = 0;
            double ignore;

            for (int i = 0; i < productsSelected.Count; i++) {
                Debug.WriteLine(priceTextBoxes[i].Text);
                if (double.TryParse(priceTextBoxes[i].Text, out ignore)) {
                    total += double.Parse(priceTextBoxes[i].Text);
                }
            }

            label37.Text = "$" + total;
        }

        private void confirmOrder_Click(object sender, EventArgs e) {

            int shippingDelay = 0; 

            for (int i = 0; i < productsSelected.Count; i++) { // get the longest delay for delivery
                int delay = Int32.Parse(waitingLabels[i].Text.Substring(0, 2));

                if (delay > shippingDelay) {
                    shippingDelay = delay;
                }
            }

            OrderController.addOrder(OrderController.createOrder(customerInContext.ToString(), DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.AddDays(shippingDelay).ToString("yyyy-MM-dd"), "processing", "None"));

            Debug.WriteLine(shippingDelay);
            Debug.WriteLine(DateTime.Now.ToString("yyyy-MM-dd"));
            Debug.WriteLine(DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss"));
            Debug.WriteLine(DateTime.Now.AddDays(shippingDelay).ToString("yyyy-MM-dd"));

            for (int i = 0; i < productsSelected.Count; i++) {
                OrderController.addOrderDetails(OrderController.createOrderDetails(productsSelected[i].productCode, quantityBoxes[i].SelectedValue.ToString(), priceTextBoxes[i].Text));
            }
        }

        /* combo box update on change methods */
        private void comboBox7_SelectedIndexChanged(object sender, EventArgs e) {
            calculateOrderTotal();
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

        private void customerContextBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            updateCustomerInContext();
        }

        /**
         * Calculates the order total based on the selected items and their quanitites.
         * Used to ensure the total is correct before confirming order. 
         */
        private void recalculateTotal_Click(object sender, EventArgs e)
        {
            //TODO Inhibit confirm order before this has been clicked.
            calculateOrderTotal();
        }
    }
}
