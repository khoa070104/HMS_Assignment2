using System;
using System.Linq;
using System.Windows;
using Services;

namespace HMSApp
{
    public partial class ReportWindow : Window
    {
        private readonly IRoomService _roomService;

        public ReportWindow(IRoomService roomService)
        {
            InitializeComponent();
            _roomService = roomService;
        }

        private void GenerateReport_Click(object sender, RoutedEventArgs e)
        {
            var startDate = StartDatePicker.SelectedDate ?? DateTime.MinValue;
            var endDate = EndDatePicker.SelectedDate ?? DateTime.MaxValue;

            var report = _roomService.GenerateReport(startDate, endDate);
            ReportDataGrid.ItemsSource = report.OrderByDescending(r => r.TotalRevenue);
        }
    }
}
