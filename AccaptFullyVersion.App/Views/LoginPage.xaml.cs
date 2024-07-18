using AccaptFullyVersion.Core.Servies;
using AccaptFullyVersion.Core.Servies.Interface;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AccaptFullyVersion.App.Views
{
    /// <summary>
    /// Interaction logic for LoginPage.xaml
    /// </summary>
    public partial class LoginPage : Window
    {
        private MainWindow _mainWindow;
        private readonly ApiCallServies _apiCallServies;
        public LoginPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _apiCallServies = new ApiCallServies();
            _mainWindow = mainWindow ?? throw new ArgumentNullException(nameof(mainWindow));
        }

        #region Login

        private async void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            if(string.IsNullOrEmpty(txtUserName.Text) || string.IsNullOrEmpty(txtPassword.Password))
            {
                MessageBox.Show("لطفا تمامی فیلدهای مورد نیاز را پر کنید");
                return;
            }

            var data = new
            {
                UserName = txtUserName.Text,
                Password = txtPassword.Password,
            };

            var responseMessage = await _apiCallServies.SendPostReauest("https://localhost:7205/api/UserAccount(V1)/LUA(V1)", data);

            if(!responseMessage.IsSuccessStatusCode)
            {
                var response = await responseMessage.Content.ReadAsStringAsync();

                if (!string.IsNullOrEmpty(response))
                {
                    MessageBox.Show($"{response}", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show("خطا در ارسال اطلاعات!", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            this.Close();
            _mainWindow.Visibility = Visibility.Visible;
            return;
        }

        #endregion

        private void btnSingUp_Click(object sender, RoutedEventArgs e)
        {
            this.Visibility = Visibility.Hidden;
            RegisterPage register = new RegisterPage(_mainWindow);
            register.Show();
        }
    }
}
