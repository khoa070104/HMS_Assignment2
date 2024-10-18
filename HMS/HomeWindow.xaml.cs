using System.Windows;
using BusinessObjects;

namespace HMSApp
{
    public partial class HomeWindow : Window
    {
        private Customer _currentCustomer;

        public HomeWindow(Customer customer)
        {
            InitializeComponent();
            _currentCustomer = customer;
            lblWelcome.Text = $"Hello, {customer.CustomerFullName}";
        }

        private void BookRoom_Click(object sender, RoutedEventArgs e)
        {
            var bookingWindow = new BookingWindow(_currentCustomer);
            bookingWindow.ShowDialog();
        }

        private void BookingHistory_Click(object sender, RoutedEventArgs e)
        {
            var historyWindow = new RoomHistoryWindow(_currentCustomer);
            historyWindow.ShowDialog();
        }

        private void EditProfile_Click(object sender, RoutedEventArgs e)
        {
            var editProfileWindow = new EditProfileWindow(_currentCustomer);
            editProfileWindow.ShowDialog();
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            var loginWindow = new LoginWindow();
            loginWindow.Show();
            this.Close();
        }
    }
}
