using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BusinessObjects;
using Services;

namespace HMSApp
{
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window
    {
        private readonly CustomerService _customerService;

        public CustomerWindow()
        {
            InitializeComponent();
            _customerService = new CustomerService();
            LoadCustomerList();
        }

        public void LoadCustomerList()
        {
            try
            {
                var customerList = _customerService.GetAllCustomers();
                dgData.ItemsSource = null;
                dgData.ItemsSource = customerList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on load list of customers");
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Customer customer = new Customer
                {
                    CustomerFullName = txtFullName.Text,
                    Telephone = txtPhoneNumber.Text,
                    EmailAddress = txtEmail.Text,
                    CustomerBirthday = dpDateOfBirth.SelectedDate,
                    CustomerStatus = cboStatus.SelectedValue.ToString() == "Active" ? 1 : 2,
                    Password = txtPassword.ToString(),
                };
                _customerService.AddCustomer(customer);
                resetInput();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadCustomerList();
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCustomerID.Text))
                {
                    Customer customer = new Customer
                    {
                        CustomerID = Int32.Parse(txtCustomerID.Text),
                        CustomerFullName = txtFullName.Text,
                        CustomerBirthday = dpDateOfBirth.SelectedDate,
                        CustomerStatus = cboStatus.SelectedValue.ToString() == "Active" ? 1 : 2,
                        Telephone = txtPhoneNumber.Text,
                        EmailAddress = txtEmail.Text,
                        Password = txtPassword.ToString(),
                    };
                    _customerService.UpdateCustomer(customer);
                    resetInput();
                }
                else
                {
                    MessageBox.Show("You must select a Customer!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadCustomerList();
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCustomerID.Text))
                {
                    int customerId = Int32.Parse(txtCustomerID.Text);
                    _customerService.DeleteCustomer(customerId);
                    resetInput();
                }
                else
                {
                    MessageBox.Show("You must select a Customer!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            finally
            {
                LoadCustomerList();
            }
        }

        private void resetInput()
        {
            txtCustomerID.Text = "";
            txtFullName.Text = "";
            txtPhoneNumber.Text = "";
            txtEmail.Text = "";
            dpDateOfBirth.SelectedDate = null;
            cboStatus.SelectedValue = null;
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItem != null)
            {
                Customer customer = (Customer)dgData.SelectedItem;
                txtCustomerID.Text = customer.CustomerID.ToString();
                txtFullName.Text = customer.CustomerFullName;
                txtPhoneNumber.Text = customer.Telephone;
                txtEmail.Text = customer.EmailAddress;
                dpDateOfBirth.SelectedDate = customer.CustomerBirthday;
                cboStatus.SelectedValue = customer.CustomerStatus;
            }
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            LoadCustomerList();
        }
    }
}
