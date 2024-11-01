using System;
using System.Linq;
using System.Windows;
using BusinessObjects;
using Services;

namespace HMSApp
{
    public partial class BookingWindow : Window
    {
        private Customer _currentCustomer;
        private readonly IRoomService _roomService;

        public BookingWindow(Customer customer)
        {
            InitializeComponent();
            _currentCustomer = customer;
            _roomService = new RoomService();
            LoadRoomTypes();
        }

        private void LoadRoomTypes()
        {
            var roomTypes = _roomService.GetAllRoomTypes();
            cboRoomType.ItemsSource = roomTypes;
        }

        private void SearchRooms_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var roomType = (RoomType)cboRoomType.SelectedItem;
                if (roomType == null)
                {
                    MessageBox.Show("Please choose room type to search", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var checkIn = dpCheckIn.SelectedDate;
                var checkOut = dpCheckOut.SelectedDate;

                if (!checkIn.HasValue || !checkOut.HasValue)
                {
                    MessageBox.Show("Please choose check-in day and check-out day.", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var availableRooms = _roomService.GetAvailableRooms(roomType.RoomTypeID, checkIn.Value, checkOut.Value);
                dgAvailableRooms.ItemsSource = availableRooms;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error to search room: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BookRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedRoom = (RoomInformation)dgAvailableRooms.SelectedItem;
                if (selectedRoom == null)
                {
                    MessageBox.Show("Please choose a room to book", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var checkIn = dpCheckIn.SelectedDate.Value;
                var checkOut = dpCheckOut.SelectedDate.Value;

                _roomService.BookRoom(_currentCustomer.CustomerID, selectedRoom.RoomID, checkIn, checkOut);
                MessageBox.Show("Booking Successfully!", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error to Booking: {ex.Message}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
