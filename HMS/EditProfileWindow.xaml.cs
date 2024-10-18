using System;
using System.Windows;
using BusinessObjects;
using Services;

namespace HMSApp
{
    public partial class EditProfileWindow : Window
    {
        private Customer _currentCustomer;
        private readonly ICustomerService _customerService;

        public EditProfileWindow(Customer customer)
        {
            InitializeComponent();
            _currentCustomer = customer;
            _customerService = ServiceProvider.GetCustomerService();
            LoadCustomerData();
        }

        private void LoadCustomerData()
        {
            txtFullName.Text = _currentCustomer.CustomerFullName;
            txtEmail.Text = _currentCustomer.EmailAddress;
            txtPhone.Text = _currentCustomer.Telephone;
            dpBirthday.SelectedDate = _currentCustomer.CustomerBirthday;
        }

        private void SaveChanges_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                _currentCustomer.CustomerFullName = txtFullName.Text;
                _currentCustomer.EmailAddress = txtEmail.Text;
                _currentCustomer.Telephone = txtPhone.Text;
                _currentCustomer.CustomerBirthday = dpBirthday.SelectedDate;
                if (!string.IsNullOrEmpty(txtPassword.Password))
                {
                    _currentCustomer.Password = txtPassword.Password;
                }

                _customerService.UpdateCustomer(_currentCustomer);
                MessageBox.Show("Profile updated successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error updating profile: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
