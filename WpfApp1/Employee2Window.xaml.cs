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
    /// Logika interakcji dla okna Employee2Window.xaml.
    /// Okno stanowi panel operacyjny dla pracownika banku, umożliwiający zarządzanie klientami 
    /// oraz monitorowanie bezpieczeństwa transakcji.
    /// </summary>
    public partial class Employee2Window : Window {
        /// <summary>
        /// Obiekt zalogowanego pracownika (doradcy), którego dane są powiązane z sesją okna.
        /// </summary>
        Employee employee;

        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="Employee2Window"/>.
        /// </summary>
        public Employee2Window() {
            InitializeComponent();
        }

        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="Employee2Window"/> dla konkretnego pracownika.
        /// Wyświetla podstawowe dane osobowe pracownika w interfejsie.
        /// </summary>
        /// <param name="employee">Obiekt pracownika, dla którego otwierane jest okno.</param>
        public Employee2Window(Employee employee) : this() {
            this.employee = employee;
            TxtFName.Text = employee.FirstName;
            TxtLName.Text = employee.LastName;
        }

        /// <summary>
        /// Obsługuje zdarzenie kliknięcia przycisku sprawdzania podejrzanych transakcji.
        /// Otwiera okno <see cref="CheckSusTransactions"/> przekazując listę transakcji przypisanych do tego pracownika.
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Argumenty zdarzenia.</param>
        public void Click_CheckSusTransactions(object sender, RoutedEventArgs e) {
            Window nWn = new CheckSusTransactions(employee);
            nWn.Show();
        }

        /// <summary>
        /// Zamyka bieżące okno i powraca do menu głównego aplikacji (<see cref="MainWindow"/>).
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Argumenty zdarzenia.</param>
        public void Click_BackToMenu(object sender, RoutedEventArgs e) {
            MainWindow mWindow = new();
            mWindow.Show();
            this.Close();
        }

        /// <summary>
        /// Otwiera formularz dodawania nowej osoby, skonfigurowany pod dodawanie klienta.
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Argumenty zdarzenia.</param>
        public void Click_AddCustomer(object sender, RoutedEventArgs e) {
            Window wn = new AddPerson(true);
            wn.Show();
        }
    }
}