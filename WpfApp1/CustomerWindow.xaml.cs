using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Logika interakcji dla okna CustomerWindow.xaml.
    /// Okno służy do wyboru konkretnego klienta z listy wszystkich zarejestrowanych klientów w systemie.
    /// </summary>
    public partial class CustomerWindow : Window {

        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="CustomerWindow"/>.
        /// Pobiera listę klientów z bazy danych i ustawia ją jako źródło danych dla kontrolki ComboBox.
        /// </summary>
        public CustomerWindow() {
            InitializeComponent();
            using (var db = new ProjectDbContext()) {
                var query1 = (from c in db.Customers select c).ToList<Customer>();
                CmbCustomer.ItemsSource = query1;
                CmbCustomer.DisplayMemberPath = "FullName";
            }
        }

        /// <summary>
        /// Obsługuje zdarzenie kliknięcia przycisku powrotu.
        /// Zamyka bieżące okno i otwiera główne menu aplikacji (<see cref="MainWindow"/>).
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Argumenty zdarzenia.</param>
        private void Button_Back(object sender, RoutedEventArgs e) {
            Window mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        /// <summary>
        /// Obsługuje zdarzenie kliknięcia przycisku wyboru klienta.
        /// Przekazuje wybrany obiekt <see cref="Customer"/> do kolejnego okna szczegółów (<see cref="Customer2Window"/>).
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Argumenty zdarzenia.</param>
        private void Button_Select(object sender, RoutedEventArgs e) {
            if (CmbCustomer.SelectedItem == null)
                return;
            Customer2Window cus2Win = new((Customer)CmbCustomer.SelectedItem);
            cus2Win.Show();
            this.Close();
        }
    }
}