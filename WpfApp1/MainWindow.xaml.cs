using ConsoleApp1;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Button_Employee_Click(object sender, RoutedEventArgs e) {
            EmployeeWindow empWin = new();
            empWin.Show();
            this.Close();
        }
        private void Button_Customer_Click(object sender, RoutedEventArgs e) {
            CustomerWindow cusWin = new();
            cusWin.Show();
            this.Close();
        }
        private void Button_Supervisor_Click(object sender, RoutedEventArgs e) {
            SupervisorWindow supWin = new();
            supWin.Show();
            this.Close();
        }
    }
}