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
    /// Logika interakcji dla okna SupervisorWindow.xaml.
    /// Okno stanowi panel administracyjny (Przełożonego), umożliwiający całościowy wgląd w bazę klientów i pracowników
    /// oraz dodawanie nowego personelu do systemu.
    /// </summary>
    public partial class SupervisorWindow : Window {

        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="SupervisorWindow"/>.
        /// </summary>
        public SupervisorWindow() {
            InitializeComponent();
        }

        /// <summary>
        /// Obsługuje zdarzenie kliknięcia przycisku powrotu.
        /// Zamyka okno administratora i powraca do menu głównego aplikacji (<see cref="MainWindow"/>).
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Argumenty zdarzenia.</param>
        public void Click_BackToMenu(object sender, RoutedEventArgs e) {
            MainWindow mWindow = new();
            mWindow.Show();
            this.Close();
        }

        /// <summary>
        /// Pobiera z bazy danych listę wszystkich zarejestrowanych klientów (<see cref="Customer"/>)
        /// i wyświetla ich dane w formie tekstowej w oknie komunikatu.
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Argumenty zdarzenia.</param>
        public void Click_ShowCustomers(object sender, RoutedEventArgs e) {
            using (var db = new ProjectDbContext()) {
                StringBuilder sb = new();
                // Pobranie wszystkich rekordów z tabeli Customers
                var query1 = (from c in db.Customers select c).ToList<Customer>();

                foreach (Customer cus in query1) {
                    sb.Append(cus.ToString());
                    sb.Append("\n");
                }

                if (query1.Count == 0)
                    MessageBox.Show("Baza klientów jest pusta.");
                else
                    MessageBox.Show(sb.ToString());
            }
        }

        /// <summary>
        /// Pobiera z bazy danych listę wszystkich pracowników (<see cref="Employee"/>)
        /// i wyświetla ich dane osobowe w oknie komunikatu.
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Argumenty zdarzenia.</param>
        public void Click_ShowEmployees(object sender, RoutedEventArgs e) {
            using (var db = new ProjectDbContext()) {
                StringBuilder sb = new();
                // Pobranie wszystkich rekordów z tabeli Employees za pomocą LINQ
                var query1 = (from em in db.Employees select em).ToList<Employee>();

                foreach (Employee emp in query1) {
                    sb.Append(emp.ToString());
                    sb.Append("\n");
                }

                if (query1.Count == 0)
                    MessageBox.Show("Brak zarejestrowanych pracowników.");
                else
                    MessageBox.Show(sb.ToString());
            }
        }

        /// <summary>
        /// Otwiera formularz dodawania nowej osoby, skonfigurowany specjalnie do rejestracji pracownika.
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Argumenty zdarzenia.</param>
        public void Click_AddEmployee(object sender, RoutedEventArgs e) {
            // Przekazanie parametru false do konstruktora AddPerson wskazuje, że tworzony będzie Employee, a nie Customer
            Window wn = new AddPerson(false);
            wn.Show();
        }
    }
}