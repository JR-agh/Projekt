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
    /// Interaction logic for Customer2Window.xaml
    /// </summary>
    public partial class Customer2Window : Window {
        Customer customer;
        //bool hasAccount = false;
        public Customer2Window() {
            InitializeComponent();
        }
        public Customer2Window(Customer customer) : this() {
            this.customer = customer;
            using (var db = new ProjectDbContext()) {
                var currentCustomer = db.Customers.Find(customer.Pesel);
                var currentAccount = db.Accounts.Find(customer.Pesel);
                customer.AssignAccount(currentAccount);
                if (currentAccount != null) {
                    TxtAccNmb.Text = currentAccount.AccountNumber;
                    //hasAccount = true;
                }
                else {
                    TxtAccNmb.Text = "Brak konta.";
                    //hasAccount = false;
                }
            }
            TxtFName.Text = customer.FirstName;
            TxtLName.Text = customer.LastName;
        }
        public void Click_ShowTransactionHistory(object sender, RoutedEventArgs e) {
            StringBuilder sb = new();
            if(customer.PersonalAccount == null) {
                MessageBox.Show("Brak konta.");
                return;
            }
            if(customer.PersonalAccount.Transactions == null) {
                MessageBox.Show("Brak transakcji w historii tego konta");
                return;
            }
            foreach (Transaction t in customer.PersonalAccount.Transactions) {
                sb.Append(t.ToString());
                sb.Append("\n");
            }
            MessageBox.Show(sb.ToString());
        }
        public void Click_NewTransfer(object sender, RoutedEventArgs e) {
            if(this.customer.PersonalAccount == null) {
                MessageBox.Show("Nie można wykonać operacji, jeśli nie posiada się konta.");
                return;
            }
            Window nWn = new NewTransaction(this.customer.PersonalAccount);
            nWn.Show();
        }
        public void Click_CreateAccount(object sender, RoutedEventArgs e) {
            if (customer.PersonalAccount == null) {
                using (var db = new ProjectDbContext()) {
                    var currentCustomer = db.Customers.Find(customer.Pesel);
                    Account a1 = new(currentCustomer);
                    db.Accounts.Add(a1);
                    db.SaveChanges();
                    MessageBox.Show($"Utworzono konto o numerze {a1.AccountNumber}.");
                }
            }
            else {
                MessageBox.Show("Istnieje już konto przypisane do tego klienta.");
                return;
            }
        }
        public void Click_BackToMenu(object sender, RoutedEventArgs e) {
            MainWindow mWindow = new();
            mWindow.Show();
            this.Close();
        }
        public void Click_ShowBalance(object sender, RoutedEventArgs e) {
            if (customer.PersonalAccount == null)
                MessageBox.Show("Brak konta.");
            else
                MessageBox.Show(customer.PersonalAccount.ToString());
        }
    }
}
