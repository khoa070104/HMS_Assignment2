using System.Linq;
using System.Windows;
using BusinessObjects;
using Services;

namespace HMSApp
{
    public partial class RoomHistoryWindow : Window
    {
        private readonly IRoomService _roomService;
        private readonly Customer _currentCustomer;

        public RoomHistoryWindow(Customer customer, IRoomService roomService)
        {
            InitializeComponent();
            _currentCustomer = customer;
            _roomService = roomService;
            LoadBookingHistory();
        }

        private void LoadBookingHistory()
        {
            var bookingHistory = _roomService.GetBookingHistory(_currentCustomer.CustomerID);
            var bookingDetails = bookingHistory.SelectMany(b => b.BookingDetails.Select(bd => new
            {
                BookingReservationID = b.BookingReservationID,
                RoomNumber = bd.Room.RoomNumber,
                StartDate = bd.StartDate,
                EndDate = bd.EndDate,
                ActualPrice = bd.ActualPrice,
                BookingStatus = b.BookingStatus == 1 ? "Active" : "Inactive"
            })).ToList();

            dgBookingHistory.ItemsSource = bookingDetails;
        }
    }
}
