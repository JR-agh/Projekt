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
    /// Interaction logic for TransactionsHistory.xaml
    /// </summary>
    public partial class TransactionsHistory : Window
    {
        Account account;
        public TransactionsHistory(Account account)
        {
            InitializeComponent();
        }
    }
}
