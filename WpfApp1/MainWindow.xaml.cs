using ConsoleApp1;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1 {
    /// <summary>
    /// Logika interakcji dla okna MainWindow.xaml.
    /// Jest to główne okno startowe aplikacji, które służy jako panel wyboru roli użytkownika.
    /// </summary>
    public partial class MainWindow : Window {
        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="MainWindow"/>.
        /// </summary>
        public MainWindow() {
            InitializeComponent();
        }

        /// <summary>
        /// Obsługuje zdarzenie kliknięcia przycisku "Pracownik".
        /// Otwiera okno wyboru pracownika (<see cref="EmployeeWindow"/>) i zamyka bieżące menu.
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Argumenty zdarzenia.</param>
        private void Button_Employee_Click(object sender, RoutedEventArgs e) {
            EmployeeWindow empWin = new();
            empWin.Show();
            this.Close();
        }

        /// <summary>
        /// Obsługuje zdarzenie kliknięcia przycisku "Klient".
        /// Otwiera okno wyboru klienta (<see cref="CustomerWindow"/>) i zamyka bieżące menu.
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Argumenty zdarzenia.</param>
        private void Button_Customer_Click(object sender, RoutedEventArgs e) {
            CustomerWindow cusWin = new();
            cusWin.Show();
            this.Close();
        }

        /// <summary>
        /// Obsługuje zdarzenie kliknięcia przycisku "Przełożony".
        /// Otwiera okno panelu przełożonego (<see cref="SupervisorWindow"/>) i zamyka bieżące menu.
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Argumenty zdarzenia.</param>
        private void Button_Supervisor_Click(object sender, RoutedEventArgs e) {
            SupervisorWindow supWin = new();
            supWin.Show();
            this.Close();
        }
    }
}