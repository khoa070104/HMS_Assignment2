﻿using HMSApp;
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
        private readonly CustomerService _customerService;
        public LoginWindow()
        {
            _customerService = new CustomerService();
            InitializeComponent();
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
            var adminEmail = _configuration["AdminCredentials:Email"];
            var adminPassword = _configuration["AdminCredentials:Password"];

            var email = txtUser.Text;
            var password = txtPass.Password;

            if (email == adminEmail && password == adminPassword)
            {
                var customerManagementWindow = new CustomerWindow();
                customerManagementWindow.Show();
                this.Close();
            }
            else
            {
                MessageBox.Show("Invalid credentials!");
            }
        }


        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}