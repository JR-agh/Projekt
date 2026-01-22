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
    /// Interaction logic for NewTransaction.xaml
    /// </summary>
    public partial class NewTransaction : Window {
        Account account;
        public NewTransaction(Account account) {
            InitializeComponent();
            this.account = account;
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
        public void Click_Back(object sender, RoutedEventArgs e) {
            this.Close();
        }
        public void Click_AddTransaction(object sender, RoutedEventArgs e) {
            if (CmbTransferType.SelectedItem == null) {
                MessageBox.Show("Wybierz typ operacji.");
                return;
            }
            using (var db = new ProjectDbContext()) {
                var acc = db.Accounts.Find(account.OwnersPesel);
                var acc2 = db.Accounts.FirstOrDefault(a => a.AccountNumber == this.TxtAcc2Nmb.Text);
                if (acc2 == null) {
                    MessageBox.Show("Zły numer konta odbiorcy.");
                    return;
                }
                switch((TransactionType)this.CmbTransferType.SelectedItem) {
                    case TransactionType.Transfer:
                        Transaction transaction1 = new(decimal.Parse(this.TxtAmount.Text, CultureInfo.InvariantCulture), TransactionType.Transfer, acc, acc2);
                        acc.ProccessTransaction(transaction1);
                        break;
                    case TransactionType.Deposit:
                        Transaction transaction2 = new(decimal.Parse(this.TxtAmount.Text, CultureInfo.InvariantCulture), TransactionType.Deposit, acc);
                        acc.ProccessTransaction(transaction2);
                        break;
                    case TransactionType.Withdrawal:
                        Transaction transaction3 = new(decimal.Parse(this.TxtAmount.Text, CultureInfo.InvariantCulture), TransactionType.Withdrawal, acc);
                        acc.ProccessTransaction(transaction3);
                        break;
                }
            }
            this.Close();
        }
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
