using System.Windows;
using BusinessObjects;
using Services;

namespace HMSApp
{
    public partial class HomeWindow : Window
    {
        private Customer _currentCustomer;
        private readonly IRoomService _roomService;

        public HomeWindow(Customer customer)
        {
            InitializeComponent();
            _currentCustomer = customer;
            _roomService = new RoomService();
            lblWelcome.Text = $"Hello, {customer.CustomerFullName}";
        }

        private void BookRoom_Click(object sender, RoutedEventArgs e)
        {
            var bookingWindow = new BookingWindow(_currentCustomer);
            bookingWindow.ShowDialog();
        }

        private void BookingHistory_Click(object sender, RoutedEventArgs e)
        {
            var historyWindow = new RoomHistoryWindow(_currentCustomer, _roomService);
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
