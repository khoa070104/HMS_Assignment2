using System.Windows;
using BusinessObjects;
using Services;

namespace HMSApp
{
    public partial class RoomHistoryWindow : Window
    {
        private readonly IRoomService _roomService;
        private readonly Customer _currentCustomer;

        public RoomHistoryWindow(Customer customer)
        {
            InitializeComponent();
            _currentCustomer = customer;
            _roomService = ServiceProvider.GetRoomService();
            LoadBookingHistory();
        }

        private void LoadBookingHistory()
        {
            var bookingHistory = _roomService.GetBookingHistory(_currentCustomer.CustomerID);
            dgBookingHistory.ItemsSource = bookingHistory;
        }
    }
}
