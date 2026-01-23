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
    /// Interaction logic for Employee2Window.xaml
    /// </summary>
    public partial class Employee2Window : Window {
        Employee employee;
        public Employee2Window() {
            InitializeComponent();
        }
        public Employee2Window(Employee employee) : this() {
            this.employee = employee;
            TxtFName.Text = employee.FirstName;
            TxtLName.Text = employee.LastName;
        }
       public void Click_CheckSusTransactions(object sender, RoutedEventArgs e) {
            Window nWn = new CheckSusTransactions(employee);
            nWn.Show();
       }

        public void Click_BackToMenu(object sender, RoutedEventArgs e) {
            MainWindow mWindow = new();
            mWindow.Show();
            this.Close();
        }
        public void Click_AddCustomer(object sender, RoutedEventArgs e) {
            Window wn = new AddPerson(true);
            wn.Show();
        }
    }
}
