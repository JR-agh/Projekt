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

namespace WpfApp1
{
    /// <summary>
    /// Interaction logic for EmployeeWindow.xaml
    /// </summary>
    public partial class EmployeeWindow : Window
    {
        public EmployeeWindow()
        {
            InitializeComponent();
            using (var db = new ProjectDbContext()) {
                var query1 = (from c in db.Employees select c).ToList<Employee>();
                CmbEmployee.ItemsSource = query1;
                CmbEmployee.DisplayMemberPath = "FullName";
            }
        }
        private void Button_Back(object sender, RoutedEventArgs e) {
            Window mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }
        private void Button_Select(object sender, RoutedEventArgs e) {
            if (CmbEmployee.SelectedItem == null)
                return;
            Employee2Window emp2Win = new((Employee)CmbEmployee.SelectedItem);
            emp2Win.Show();
            this.Close();
        }
    }
}
