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
            _roomService = ServiceProvider.GetRoomService();
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
                    MessageBox.Show("Vui lòng chọn loại phòng.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var checkIn = dpCheckIn.SelectedDate;
                var checkOut = dpCheckOut.SelectedDate;

                if (!checkIn.HasValue || !checkOut.HasValue)
                {
                    MessageBox.Show("Vui lòng chọn ngày check-in và check-out.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var availableRooms = _roomService.GetAvailableRooms(roomType.RoomTypeID, checkIn.Value, checkOut.Value);
                dgAvailableRooms.ItemsSource = availableRooms;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tìm kiếm phòng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void BookRoom_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var selectedRoom = (Room)dgAvailableRooms.SelectedItem;
                if (selectedRoom == null)
                {
                    MessageBox.Show("Vui lòng chọn một phòng để đặt.", "Cảnh báo", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }

                var checkIn = dpCheckIn.SelectedDate.Value;
                var checkOut = dpCheckOut.SelectedDate.Value;

                _roomService.BookRoom(_currentCustomer.CustomerID, selectedRoom.RoomID, checkIn, checkOut);
                MessageBox.Show("Đặt phòng thành công!", "Thành công", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi đặt phòng: {ex.Message}", "Lỗi", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
