using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CoffeeShopApp
{
    public partial class CoffeeShop : Form
    {
        public CoffeeShop()
        {
            InitializeComponent();
        }
        private void CoffeeShop_Load(object sender, EventArgs e)
        {
            orderComboBox.SelectedIndex = 0;
        }
        private List<string> customerNames = new List<string>();
        private List<string> contactNumbers = new List<string>();
        private List<string> customerAddresses = new List<string>();
        private List<string> coffeeOrders = new List<string>();
        private List<int> prices = new List<int>();
        private List<int> quantities = new List<int>();

        private void saveButton_Click(object sender, EventArgs e)
        {
            bool validData;
            string message;
            validData = Check();
            if (validData)
            {
                 message = SaveCustomerOrder();
            }
            else
            {
                message = "Customer's Data cannot save successfully";
            }
            MessageBox.Show(message);
        }
        private void showButton_Click(object sender, EventArgs e)
        {
            ClearInput();
            DisplayPurchaseInformation(0);
        }
        private void resetButton_Click(object sender, EventArgs e)
        {
            ClearInput();
        }
        private bool Check()
        {
            if (nameTextBox.Text == "" || contactTextBox.Text == "" || addressTextBox.Text == "" || orderComboBox.Text == "--Select--" ||
                quantityTextBox.Text == "")
            {
                MessageBox.Show("Please fill every field");
                return false;
            }
            if (customerNames.Contains(nameTextBox.Text))
            {
                MessageBox.Show("This Customer is already existed");
                return false;
            }
            if (contactNumbers.Contains(contactTextBox.Text))
            {
                MessageBox.Show(contactTextBox.Text + ", This contact number is already used");
                return false;
            }
            return true;
        }
        private string SaveCustomerOrder()
        {
            string name = nameTextBox.Text;
            string contact = contactTextBox.Text;
            string address = addressTextBox.Text;
            string order = orderComboBox.Text;
            int quantity = Convert.ToInt32(quantityTextBox.Text);
            int customerId = customerNames.Count;
            int price;
            if (orderComboBox.SelectedIndex == 1)
                price = 120;
            else if (orderComboBox.SelectedIndex == 2)
                price = 100;
            else if (orderComboBox.SelectedIndex == 3)
                price = 90;
            else
                price = 80;
            customerNames.Add(name);
            customerAddresses.Add(address);
            coffeeOrders.Add(order);
            quantities.Add(quantity);
            prices.Add(price);
            contactNumbers.Add(contact);
            ClearInput();
            DisplayPurchaseInformation(customerId);
            return "Purchase successful.";
        }
        private void DisplayPurchaseInformation(int index)
        {
            for (int i = index; i < customerNames.Count; i++)
            {
                string message;
                message = "\t\tCustomer No : " + Convert.ToInt16(i + 1) + "\n\n";
                message = message + "Customer Name \t: " + customerNames[i] + "\n";
                message = message + "Contact No \t\t: " + contactNumbers[i] + "\n";
                message = message + "Address \t\t: " + customerAddresses[i] + "\n";
                message = message + "Coffee ordered \t: " + coffeeOrders[i] + "\n";
                message = message + "Price per item \t\t: " + prices[i] + "\n";
                message = message + "Quantity \t\t: " + quantities[i] + "\n\n";
                message = message + customerNames[i] + " sir, you have to pay " + prices[i] * quantities[i] +
                          " Taka" + "\n\n";
                purchaseInformationRichTextBox.Text += message;
            }
        }
        private void ClearInput()
        {
            nameTextBox.Clear();
            contactTextBox.Clear();
            addressTextBox.Clear();
            orderComboBox.SelectedIndex = 0;
            quantityTextBox.Clear();
            purchaseInformationRichTextBox.Clear();
        }
        private void quantityTextBox_KeyPress(object sender, KeyPressEventArgs e) // allow only numerical value to input in quantity
        {
            if (!(Char.IsDigit(e.KeyChar) || (e.KeyChar == (char)Keys.Back)))
                e.Handled = true;
        }
    }
}
