using HMS;
using Services;
using System.Windows;

namespace HMSApp
{
    public partial class AdminManagementWindow : Window
    {
        private readonly ICustomerService _customerService;

        public AdminManagementWindow()
        {
            InitializeComponent();
            _customerService = ServiceProvider.GetCustomerService();
        }

        private void CustomerManagement_Click(object sender, RoutedEventArgs e)
        {
            var customerWindow = new CustomerWindow();
            customerWindow.Show();
        }

        private void RoomManagement_Click(object sender, RoutedEventArgs e)
        {
            new RoomWindow().Show();
        }

        private void RoomTypeManagement_Click(object sender, RoutedEventArgs e)
        {
            new RoomTypeWindow().Show();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            LoginWindow loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
