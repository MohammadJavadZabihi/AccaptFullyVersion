using AccaptFullyVersion.Core.Servies;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

namespace AccaptFullyVersion.App.Views
{
    /// <summary>
    /// Interaction logic for RegisterPage.xaml
    /// </summary>
    public partial class RegisterPage : Window
    {
        private readonly ApiCallServies _apiCallServies;
        private MainWindow _mainWindow;
        public RegisterPage(MainWindow mainWindow)
        {
            InitializeComponent();
            _apiCallServies = new ApiCallServies();
            _mainWindow = mainWindow;
        }

        private async void btnRegister_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtEmail.Text) || string.IsNullOrWhiteSpace(txtUserName.Text) || string.IsNullOrWhiteSpace(txtPassword.Password) || string.IsNullOrWhiteSpace(txtRePassword.Password))
            {
                MessageBox.Show("لطفاً تمام فیلدهای مورد نیاز را پر کنید");
                return;
            }

            var data = new
            {
                UserName = txtUserName.Text,
                Email = txtEmail.Text,
                Password = txtPassword.Password,
                RePassword = txtRePassword.Password
            };

            var responseMessage = await _apiCallServies.
                SendPostReauest("https://localhost:7205/api/UserAccount(V1)/RUA(V1)", data);

            if(!responseMessage.IsSuccessStatusCode)
            {
                var response = await responseMessage.Content.ReadAsStringAsync();

                if(!string.IsNullOrWhiteSpace(response))
                {
                    MessageBox.Show($"{response}", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show("خطا در ارسال اطلاعات!", "خطا", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            MessageBox.Show("ثبت نام شما با موفقیت انجام شد, لطفا حساب کاربری خود را با استفاده از ایمیلی که برای شما ارسال کردیم فعال بکنید", "موفقیت",
                MessageBoxButton.OK, MessageBoxImage.Information);

            LoginPage loginPage = new LoginPage(_mainWindow);
            loginPage.Visibility = Visibility.Visible;

        }

        private void btnSingin_Click(object sender, RoutedEventArgs e)
        {
            LoginPage loginPage = new LoginPage(_mainWindow);
            loginPage.Visibility = Visibility.Visible;
            this.Close();
        }
    }
}
