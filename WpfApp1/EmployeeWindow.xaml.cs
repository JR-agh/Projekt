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
    /// Logika interakcji dla okna EmployeeWindow.xaml.
    /// Okno to służy do wyboru pracownika (doradcy) z listy wszystkich pracowników zarejestrowanych w systemie.
    /// </summary>
    public partial class EmployeeWindow : Window {
        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="EmployeeWindow"/>.
        /// Podczas inicjalizacji łączy się z bazą danych przez <see cref="ProjectDbContext"/>, 
        /// pobiera listę pracowników i przypisuje ją do kontrolki wyboru.
        /// </summary>
        public EmployeeWindow() {
            InitializeComponent();
            using (var db = new ProjectDbContext()) {
                // Pobranie wszystkich pracowników z bazy danych przy użyciu zapytania LINQ
                var query1 = (from c in db.Employees select c).ToList<Employee>();

                // Ustawienie źródła danych dla ComboBoxa
                CmbEmployee.ItemsSource = query1;

                // Określenie, która właściwość obiektu Employee ma być wyświetlana w liście
                CmbEmployee.DisplayMemberPath = "FullName";
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
        /// Obsługuje zdarzenie kliknięcia przycisku wyboru.
        /// Pobiera wybrany obiekt <see cref="Employee"/> i przekazuje go do okna panelu pracownika (<see cref="Employee2Window"/>).
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Argumenty zdarzenia.</param>
        private void Button_Select(object sender, RoutedEventArgs e) {
            // Walidacja: upewnienie się, że element został wybrany przed przejściem dalej
            if (CmbEmployee.SelectedItem == null)
                return;

            // Tworzenie instancji okna szczegółowego i przekazanie wybranego pracownika przez konstruktor
            Employee2Window emp2Win = new((Employee)CmbEmployee.SelectedItem);
            emp2Win.Show();

            // Zamknięcie okna wyboru po udanym przejściu
            this.Close();
        }
    }
}