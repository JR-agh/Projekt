using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
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
    /// Logika interakcji dla okna NewTransaction.xaml.
    /// Okno umożliwia użytkownikowi realizację przelewów, depozytów oraz wypłat.
    /// </summary>
    public partial class NewTransaction : Window {
        /// <summary>
        /// Klient inicjujący transakcję.
        /// </summary>
        Customer customer;

        /// <summary>
        /// Konto powiązane z klientem, z którego lub na które dokonywana jest operacja.
        /// </summary>
        Account account;

        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="NewTransaction"/>.
        /// Konfiguruje interfejs użytkownika, wyświetla saldo i wypełnia listę typów transakcji opisami z enum.
        /// </summary>
        /// <param name="customer">Obiekt klienta wykonującego transakcję.</param>
        public NewTransaction(Customer customer) {
            InitializeComponent();
            this.customer = customer;
            this.account = customer.PersonalAccount;
            TxtAccNmb.Text = account.AccountNumber;
            TxtBalance.Text = account.Balance.ToString("F2");
            var transferOptions = Enum.GetValues(typeof(TransactionType)).Cast<TransactionType>().Select(e => new {
                Value = e,
                Description = GetEnumDescription(e)
            }).ToList();
            CmbTransferType.ItemsSource = transferOptions;
            CmbTransferType.DisplayMemberPath = "Description";
            CmbTransferType.SelectedValuePath = "Value";
        }

        /// <summary>
        /// Obsługuje zdarzenie kliknięcia przycisku powrotu.
        /// Powraca do panelu klienta (<see cref="Customer2Window"/>).
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Argumenty zdarzenia.</param>
        public void Click_Back(object sender, RoutedEventArgs e) {
            Customer2Window nWn = new(this.customer);
            nWn.Show();
            this.Close();
        }

        /// <summary>
        /// Obsługuje logikę zatwierdzania nowej transakcji.
        /// Waliduje typ operacji, numer konta odbiorcy oraz kwotę, a następnie zapisuje zmiany w bazie danych.
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Argumenty zdarzenia.</param>
        public void Click_AddTransaction(object sender, RoutedEventArgs e) {
            if (CmbTransferType.SelectedItem == null) {
                MessageBox.Show("Wybierz typ operacji.");
                return;
            }

            using (var db = new ProjectDbContext()) {
                var acc = db.Accounts.Find(account.OwnersPesel);
                var acc2 = db.Accounts.FirstOrDefault(a => a.AccountNumber == this.TxtAcc2Nmb.Text);
                if (acc2 == null && (TransactionType)this.CmbTransferType.SelectedValue == TransactionType.Transfer) {
                    MessageBox.Show("Zły numer konta odbiorcy.");
                    return;
                }

                decimal amount;
                try {
                    amount = decimal.Parse(this.TxtAmount.Text, CultureInfo.InvariantCulture);
                }
                catch {
                    MessageBox.Show("Błędna kwota transakcji");
                    return;
                }
                switch ((TransactionType)this.CmbTransferType.SelectedValue) {
                    case TransactionType.Transfer:
                        Transaction transaction1 = new(amount, TransactionType.Transfer, acc, acc2);
                        acc.ProccessTransaction(transaction1);
                        acc2.ProccessTransaction(transaction1);
                        db.Transactions.Add(transaction1);
                        MessageBox.Show("Udana transakcja.");
                        break;
                    case TransactionType.Deposit:
                        Transaction transaction2 = new(amount, TransactionType.Deposit, acc);
                        acc.ProccessTransaction(transaction2);
                        db.Transactions.Add(transaction2);
                        MessageBox.Show("Udany depozyt.");
                        break;
                    case TransactionType.Withdrawal:
                        Transaction transaction3 = new(amount, TransactionType.Withdrawal, acc);
                        acc.ProccessTransaction(transaction3);
                        db.Transactions.Add(transaction3);
                        MessageBox.Show("Udana wypłata.");
                        break;
                }
                db.SaveChanges();
            }
            Customer2Window nWn = new(this.customer);
            nWn.Show();
            this.Close();
        }

        /// <summary>
        /// Pobiera wartość atrybutu <see cref="DescriptionAttribute"/> dla podanej wartości wyliczeniowej (enum).
        /// Jeśli atrybut nie istnieje, zwraca nazwę tekstową elementu enum.
        /// </summary>
        /// <param name="value">Wartość enum, dla której szukany jest opis.</param>
        /// <returns>Tekstowy opis elementu enum lub jego nazwa.</returns>
        public static string GetEnumDescription(Enum value) {
            FieldInfo fi = value.GetType().GetField(value.ToString());
            DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);

            if (attributes != null && attributes.Length > 0)
                return attributes[0].Description;
            else
                return value.ToString();
        }
    }
}