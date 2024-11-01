using System;
using System.Windows;
using System.Windows.Controls;
using BusinessObjects;
using Services;

namespace HMSApp
{
    public partial class RoomTypeWindow : Window
    {
        private readonly IRoomTypeService _roomTypeService;

        public RoomTypeWindow()
        {
            InitializeComponent();
            _roomTypeService = new RoomTypeService();
            LoadRoomTypeList();
        }

        public void LoadRoomTypeList()
        {
            try
            {
                var roomTypeList = _roomTypeService.GetAllRoomTypes();
                dgData.ItemsSource = null;
                dgData.ItemsSource = roomTypeList;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi khi tải danh sách loại phòng", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void btnCreate_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                RoomType roomType = new RoomType
                {
                    RoomTypeName = txtRoomTypeName.Text,
                    TypeDescription = txtDescription.Text,
                    TypeNote = txtTypeNote.Text
                };
                _roomTypeService.AddRoomType(roomType);
                resetInput();
                LoadRoomTypeList();
                MessageBox.Show("Created Room Type", "Created", MessageBoxButton.OK, MessageBoxImage.Information);

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
                if (!string.IsNullOrEmpty(txtRoomTypeID.Text))
                {
                    RoomType roomType = new RoomType
                    {
                        RoomTypeID = int.Parse(txtRoomTypeID.Text),
                        RoomTypeName = txtRoomTypeName.Text,
                        TypeDescription = txtDescription.Text,
                        TypeNote = txtTypeNote.Text
                    };
                    _roomTypeService.UpdateRoomType(roomType);
                    MessageBox.Show("Updated Room Type", "Updated", MessageBoxButton.OK, MessageBoxImage.Information);
                    resetInput();
                    LoadRoomTypeList();
                }
                else
                {
                    MessageBox.Show("Please choose least 1 room type to update", "Warning", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                if (!string.IsNullOrEmpty(txtRoomTypeID.Text))
                {
                    int roomTypeId = int.Parse(txtRoomTypeID.Text);
                    _roomTypeService.DeleteRoomType(roomTypeId);
                    resetInput();
                    LoadRoomTypeList();
                    MessageBox.Show("Deleted Room Type", "Deleted", MessageBoxButton.OK, MessageBoxImage.Information);

                }
                else
                {
                    MessageBox.Show("You must select a Room Type!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void resetInput()
        {
            txtRoomTypeID.Text = "";
            txtRoomTypeName.Text = "";
            txtDescription.Text = "";
            txtTypeNote.Text = "";
        }

        private void dgData_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dgData.SelectedItem != null && dgData.SelectedItem is RoomType roomType)
            {
                txtRoomTypeID.Text = roomType.RoomTypeID.ToString();
                txtRoomTypeName.Text = roomType.RoomTypeName;
                txtDescription.Text = roomType.TypeDescription;
                txtTypeNote.Text = roomType.TypeNote;
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
    }
}
