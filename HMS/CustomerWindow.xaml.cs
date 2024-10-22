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
        private readonly ICustomerService _customerService;

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
                MessageBox.Show(ex.Message, "Lỗi khi tải danh sách khách hàng", MessageBoxButton.OK, MessageBoxImage.Error);
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
                    CustomerStatus = ((ComboBoxItem)cboStatus.SelectedItem).Tag.ToString() == "1" ? 1 : 2,
                    Password = txtPassword.Password
                };
                _customerService.AddCustomer(customer);
                MessageBox.Show("Khách hàng đã được thêm thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                resetInput();
                LoadCustomerList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCustomerID.Text))
                {
                    int customerId = int.Parse(txtCustomerID.Text);
                    var existingCustomer = _customerService.GetCustomerById(customerId);
                    if (existingCustomer != null)
                    {
                        existingCustomer.CustomerFullName = txtFullName.Text;
                        existingCustomer.Telephone = txtPhoneNumber.Text;
                        existingCustomer.EmailAddress = txtEmail.Text;
                        existingCustomer.CustomerBirthday = dpDateOfBirth.SelectedDate;
                        existingCustomer.CustomerStatus = ((ComboBoxItem)cboStatus.SelectedItem).Tag.ToString() == "1" ? 1 : 2;
                        existingCustomer.Password = txtPassword.Password;

                        _customerService.UpdateCustomer(existingCustomer);
                        MessageBox.Show("Khách hàng đã được cập nhật thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        resetInput();
                        LoadCustomerList();
                    }
                    else
                    {
                        MessageBox.Show("Không tìm thấy khách hàng!", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một khách hàng để cập nhật!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtCustomerID.Text))
                {
                    int customerId = int.Parse(txtCustomerID.Text);
                    var result = MessageBox.Show("Bạn có chắc chắn muốn xóa khách hàng này?", "Xác nhận xóa", MessageBoxButton.YesNo, MessageBoxImage.Question);
                    if (result == MessageBoxResult.Yes)
                    {
                        _customerService.DeleteCustomer(customerId);
                        MessageBox.Show("Khách hàng đã được xóa thành công!", "Thông báo", MessageBoxButton.OK, MessageBoxImage.Information);
                        resetInput();
                        LoadCustomerList();
                    }
                }
                else
                {
                    MessageBox.Show("Vui lòng chọn một khách hàng để xóa!", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void resetInput()
        {
            txtCustomerID.Text = "";
            txtFullName.Text = "";
            txtPhoneNumber.Text = "";
            txtEmail.Text = "";
            txtPassword.Password = "";
            dpDateOfBirth.SelectedDate = null;
            cboStatus.SelectedIndex = -1;
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItem != null && dgData.SelectedItem is Customer customer)
            {
                txtCustomerID.Text = customer.CustomerID.ToString();
                txtFullName.Text = customer.CustomerFullName;
                txtPhoneNumber.Text = customer.Telephone;
                txtEmail.Text = customer.EmailAddress;
                txtPassword.Password = customer.Password;
                dpDateOfBirth.SelectedDate = customer.CustomerBirthday;
                cboStatus.SelectedIndex = customer.CustomerStatus == 1 ? 0 : 1;
            }
            else
            {
                resetInput();
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
