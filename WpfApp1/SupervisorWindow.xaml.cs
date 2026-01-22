using ConsoleApp1;
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

namespace WpfApp1 {
    /// <summary>
    /// Interaction logic for SupervisorWindow.xaml
    /// </summary>
    public partial class SupervisorWindow : Window {
        public SupervisorWindow() {
            InitializeComponent();
        }
        //adding employees
        public void Click_BackToMenu(object sender, RoutedEventArgs e) {
            MainWindow mWindow = new();
            mWindow.Show();
            this.Close();
        }
        public void Click_ShowCustomers(object sender, RoutedEventArgs e) {
            using (var db = new ProjectDbContext()) {
                StringBuilder sb = new();
                var query1 = (from c in db.Customers select c).ToList<Customer>();
                foreach (Customer cus in query1) {
                    sb.Append(cus.ToString());
                    sb.Append("\n");
                }
                MessageBox.Show(sb.ToString());
            }
        }
        public void Click_ShowEmployees(object sender, RoutedEventArgs e) {
            using (var db = new ProjectDbContext()) {
                StringBuilder sb = new();
                var query1 = (from em in db.Employees select em).ToList<Employee>();
                foreach (Employee emp in query1) {
                    sb.Append(emp.ToString());
                    sb.Append("\n");
                }
                MessageBox.Show(sb.ToString());
            }
        }
        public void Click_AddEmployee(object sender, RoutedEventArgs e) {
            Window wn = new AddPerson(false);
            wn.Show();
        }
    }
}
