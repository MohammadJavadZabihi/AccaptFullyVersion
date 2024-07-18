using AccaptFullyVersion.App.Views;
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

namespace AccaptFullyVersion.App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public int formId = -1;
        public MainWindow()
        {
            InitializeComponent();
            if (formId == -1)
            {
                LoginPage loginPage = new LoginPage(this);
                loginPage.ShowDialog();
            }
        }
    }
}