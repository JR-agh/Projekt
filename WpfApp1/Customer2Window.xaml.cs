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
using System.Data.Entity;

namespace WpfApp1 {
    /// <summary>
    /// Logika interakcji dla okna Customer2Window.xaml.
    /// Okno to stanowi panel klienta, umożliwiający podgląd danych, zarządzanie kontem oraz dostęp do historii transakcji.
    /// </summary>
    public partial class Customer2Window : Window {
        /// <summary>
        /// Obiekt zalogowanego klienta, którego dane są wyświetlane w oknie.
        /// </summary>
        Customer customer;

        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="Customer2Window"/>.
        /// </summary>
        public Customer2Window() {
            InitializeComponent();
        }

        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="Customer2Window"/> dla konkretnego klienta.
        /// Pobiera dane o koncie z bazy danych i aktualizuje interfejs użytkownika.
        /// </summary>
        /// <param name="customer">Obiekt klienta, dla którego otwierane jest okno.</param>
        public Customer2Window(Customer customer) : this() {
            this.customer = customer;
            using (var db = new ProjectDbContext()) {
                var currentCustomer = db.Customers.Find(customer.Pesel);
                var currentAccount = db.Accounts.Find(customer.Pesel);
                customer.AssignAccount(currentAccount);
                if (currentAccount != null) {
                    TxtAccNmb.Text = currentAccount.AccountNumber;
                }
                else {
                    TxtAccNmb.Text = "Brak konta.";
                }
            }
            TxtFName.Text = customer.FirstName;
            TxtLName.Text = customer.LastName;
        }

        /// <summary>
        /// Pobiera historię wszystkich transakcji (przychodzących i wychodzących) przypisanych do konta klienta 
        /// i wyświetla je w oknie komunikatu.
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Argumenty zdarzenia.</param>
        public void Click_ShowTransactionHistory(object sender, RoutedEventArgs e) {
            StringBuilder sb = new();
            if (customer.PersonalAccount == null) {
                MessageBox.Show("Brak konta.");
                return;
            }

            using (var db = new ProjectDbContext()) {
                List<Transaction> transactions = db.Transactions
                    .Include(t => t.Sender)
                    .Include(t => t.Recipient)
                    .Where(t => (t.RecipientsPesel == this.customer.Pesel || t.SendersPesel == this.customer.Pesel))
                    .ToList();

                if (transactions.Count == 0 || transactions == null) {
                    MessageBox.Show("Brak transakcji w historii tego konta");
                    return;
                }

                foreach (Transaction t in transactions) {
                    sb.Append(t.ToString());
                    sb.Append("\n");
                }
            }
            MessageBox.Show(sb.ToString());
        }

        /// <summary>
        /// Otwiera okno tworzenia nowej transakcji, jeśli klient posiada konto.
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Argumenty zdarzenia.</param>
        public void Click_NewTransfer(object sender, RoutedEventArgs e) {
            if (this.customer.PersonalAccount == null) {
                MessageBox.Show("Nie można wykonać operacji, jeśli nie posiada się konta.");
                return;
            }
            Window nWn = new NewTransaction(this.customer);
            nWn.Show();
            this.Close();
        }

        /// <summary>
        /// Tworzy nowe konto bankowe dla klienta, jeśli ten jeszcze go nie posiada.
        /// Generuje nowy obiekt <see cref="Account"/> i zapisuje go w bazie danych.
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Argumenty zdarzenia.</param>
        public void Click_CreateAccount(object sender, RoutedEventArgs e) {
            if (customer.PersonalAccount == null) {
                using (var db = new ProjectDbContext()) {
                    var currentCustomer = db.Customers.Find(customer.Pesel);
                    Account a1 = new(currentCustomer);
                    db.Accounts.Add(a1);
                    db.SaveChanges();
                    MessageBox.Show($"Utworzono konto o numerze {a1.AccountNumber}.");
                }
                Customer2Window nWn = new(this.customer);
                nWn.Show();
                this.Close();
            }
            else {
                MessageBox.Show("Istnieje już konto przypisane do tego klienta.");
                return;
            }
        }

        /// <summary>
        /// Zamyka obecne okno i powraca do menu głównego aplikacji (<see cref="MainWindow"/>).
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Argumenty zdarzenia.</param>
        public void Click_BackToMenu(object sender, RoutedEventArgs e) {
            MainWindow mWindow = new();
            mWindow.Show();
            this.Close();
        }
    }
}