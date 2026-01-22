using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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

namespace WpfApp1 {
    /// <summary>
    /// Interaction logic for CustomerWindow.xaml
    /// </summary>
    public partial class CustomerWindow : Window {
        public CustomerWindow() {
            InitializeComponent();
            using (var db = new ProjectDbContext()) {
                var query1 = (from c in db.Customers select c).ToList<Customer>();
                CmbCustomer.ItemsSource = query1;
                CmbCustomer.DisplayMemberPath = "FullName";
            }
        }
        private void Button_Back(object sender, RoutedEventArgs e) {
            Window mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void Button_Select(object sender, RoutedEventArgs e) {
            if (CmbCustomer.SelectedItem == null)
                return;
            Customer2Window cus2Win = new((Customer)CmbCustomer.SelectedItem);
            cus2Win.Show();
            this.Close();
        }
    }
}
