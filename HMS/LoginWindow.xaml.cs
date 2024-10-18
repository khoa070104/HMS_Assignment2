using HMSApp;
using Microsoft.Extensions.Configuration;
using Services;
using System.Configuration;
using System.IO;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace HMSApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class LoginWindow : Window
    {
        private IConfiguration _configuration;
        private readonly ICustomerService _customerService;

        public LoginWindow()
        {
            InitializeComponent();
            _customerService = ServiceProvider.GetCustomerService();
            LoadConfiguration();
        }

        private void LoadConfiguration()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
            _configuration = builder.Build();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            string username = txtUser.Text;
            string password = txtPass.Password;

            var adminEmail = _configuration["AdminCredentials:Email"];
            var adminPassword = _configuration["AdminCredentials:Password"];

            if (username == adminEmail && password == adminPassword)
            {
                AdminManagementWindow adminWindow = new AdminManagementWindow();
                adminWindow.Show();
                this.Close();
            }
            else
            {
                var customer = _customerService.GetCustomerByUsernameAndPassword(username, password);
                if (customer != null)
                {
                    HomeWindow homeWindow = new HomeWindow(customer);
                    homeWindow.Show();
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Invalid username or password");
                }
            }
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
