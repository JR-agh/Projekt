using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1 {
    /// <summary>
    /// Interaction logic for AddPerson.xaml
    /// </summary>
    public partial class AddPerson : Window {
        bool isCustomer;
        public AddPerson() {
            InitializeComponent();
        }
        public AddPerson(bool isCustomer) : this() {
            this.isCustomer = isCustomer;
            BxDay.ItemsSource = Enumerable.Range(1, 31).ToList();
            BxMonth.ItemsSource = Enumerable.Range(1, 12).ToList();
            BxYear.ItemsSource = Enumerable.Range(1900, 108).ToList();
        }
        public void Click_Back(object sender, RoutedEventArgs e) {
            this.Close();
        }
        public void Click_AddPerson(object sender, RoutedEventArgs e) {
            bool success = false;

            if (checkBoxesInput(this))
                if (isCustomer) {
                    try {
                        using (var db = new ProjectDbContext()) {
                            DateTime d1 = new((int)BxYear.SelectedItem, (int)BxMonth.SelectedItem, (int)BxDay.SelectedItem);
                            Customer customer = new(TxtFName.Text, TxtLName.Text, TxtPesel.Text, TxtTelNmb.Text, d1);
                            db.Customers.Add(customer);
                            db.SaveChanges();
                            success = true;
                        }
                    }
                    catch {
                        MessageBox.Show("Niepoprawna data.");
                    }
                }
                else {
                    try {
                        using (var db = new ProjectDbContext()) {
                            DateTime d1 = new((int)BxYear.SelectedItem, (int)BxMonth.SelectedItem, (int)BxDay.SelectedItem);
                            Employee employee = new(TxtFName.Text, TxtLName.Text, TxtPesel.Text, TxtTelNmb.Text, d1);
                            db.Employees.Add(employee);
                            db.SaveChanges();
                            success = true;
                        }
                    }
                    catch {
                        MessageBox.Show("Niepoprawna data.");
                    }   
                }
            if (success) {
                if (isCustomer)
                    MessageBox.Show("Poprawnie dodano nowego klienta");
                else
                    MessageBox.Show("Poprawnie dodano nowego pracownika");
                this.Close();
            }
        }
        static bool checkBoxesInput(AddPerson addPerson) {
            if (addPerson.TxtFName.Text == "" || addPerson.TxtLName.Text == ""
               || addPerson.TxtPesel.Text == "" || addPerson.TxtTelNmb.Text == "") {
                MessageBox.Show("Źle wypełnione pola.");
                return false;
            }
            if (addPerson.BxYear.SelectedItem == null || addPerson.BxMonth.SelectedItem == null || addPerson.BxYear.SelectedItem == null) {
                MessageBox.Show("Niepoprawna data.");
                return false;
            }
            return true;
        }
    }
}
