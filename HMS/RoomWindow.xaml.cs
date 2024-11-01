using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using BusinessObjects;
using Services;

namespace HMSApp
{
    public partial class RoomWindow : Window
    {
        private readonly IRoomService _roomService;
        private readonly IRoomTypeService _roomTypeService;

        public RoomWindow()
        {
            InitializeComponent();
            _roomService = new RoomService();
            _roomTypeService = new RoomTypeService();
            LoadRoomList();
            LoadRoomTypes();
        }

        public void LoadRoomList()
        {
            try
            {
                var roomList = _roomService.GetAllRooms();
                dgData.ItemsSource = null;
                dgData.ItemsSource = roomList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on load list of rooms", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void LoadRoomTypes()
        {
            try
            {
                var roomTypes = _roomTypeService.GetAllRoomTypes();
                cboRoomType.ItemsSource = roomTypes;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error on load room types", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RoomInformation room = new RoomInformation
                {
                    RoomNumber = txtRoomNumber.Text,
                    RoomDescription = txtDescription.Text,
                    RoomMaxCapacity = int.Parse(txtMaxCapacity.Text),
                    RoomPricePerDate = decimal.Parse(txtPricePerDate.Text),
                    RoomTypeID = (int)cboRoomType.SelectedValue,
                    RoomStatus = 1 
                };
                MessageBox.Show("Created Room", "Created", MessageBoxButton.OK, MessageBoxImage.Information);

                _roomService.AddRoom(room);
                resetInput();
                LoadRoomList();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnUpdate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!string.IsNullOrEmpty(txtRoomID.Text))
                {
                    RoomInformation room = new RoomInformation
                    {
                        RoomID = int.Parse(txtRoomID.Text),
                        RoomNumber = txtRoomNumber.Text,
                        RoomDescription = txtDescription.Text,
                        RoomMaxCapacity = int.Parse(txtMaxCapacity.Text),
                        RoomPricePerDate = decimal.Parse(txtPricePerDate.Text),
                        RoomTypeID = (int)cboRoomType.SelectedValue,
                        RoomStatus = 1 
                    };
                    _roomService.UpdateRoom(room);
                    resetInput();
                    LoadRoomList();
                    MessageBox.Show("Updated Room", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("You must select a Room!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var tag = MessageBox.Show("Are you okey ?", "Delete?", MessageBoxButton.YesNo, MessageBoxImage.Warning).ToString();
                if (tag.Equals("No"))
                    return;
                if (!string.IsNullOrEmpty(txtRoomID.Text))
                {
                    int roomId = int.Parse(txtRoomID.Text);
                    _roomService.DeleteRoom(roomId);
                    resetInput();
                    LoadRoomList();
                    MessageBox.Show("Deleted Success!", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("You must select a Room!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void resetInput()
        {
            txtRoomID.Text = "";
            txtRoomNumber.Text = "";
            txtDescription.Text = "";
            txtMaxCapacity.Text = "";
            txtPricePerDate.Text = "";
            cboRoomType.SelectedIndex = -1;
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItem != null && dgData.SelectedItem is RoomInformation room)
            {
                txtRoomID.Text = room.RoomID.ToString();
                txtRoomNumber.Text = room.RoomNumber;
                txtDescription.Text = room.RoomDescription;
                txtMaxCapacity.Text = room.RoomMaxCapacity.ToString();
                txtPricePerDate.Text = room.RoomPricePerDate.ToString();
                cboRoomType.SelectedValue = room.RoomTypeID;
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

        private void btnCreateReport_Click(object sender, RoutedEventArgs e)
        {
            var reportWindow = new ReportWindow(_roomService);
            reportWindow.ShowDialog();
        }
    }
}
