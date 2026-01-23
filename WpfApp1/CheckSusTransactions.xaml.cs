using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for CheckSusTransactions.xaml
    /// </summary>
    public partial class CheckSusTransactions : Window
    {
        //Employee employee;
        public CheckSusTransactions(Employee employee)
        {
            InitializeComponent();
            //this.employee = employee;
            using (var db = new ProjectDbContext()) {
                var currentEmployee = db.Employees.Find(employee.Pesel);
                List<Transaction> susTransactions = currentEmployee.SusTransactions;
                LstSusTrn.ItemsSource = susTransactions;
            }
        }
        void Click_ApproveTransaction(object sender, RoutedEventArgs e) {
            
        }
        void Click_Back(object sender, RoutedEventArgs e) {
            this.Close();
        }
    }
}
