using ConsoleApp1;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Text.RegularExpressions;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WpfApp1 {
    /// <summary>
    /// Logika interakcji dla okna AddPerson.xaml.
    /// Klasa umożliwia dodawanie nowych obiektów typu <see cref="Customer"/> lub <see cref="Employee"/> do bazy danych.
    /// </summary>
    public partial class AddPerson : Window {
        /// <summary>
        /// Określa, czy aktualnie dodawana osoba jest klientem (true) czy pracownikiem (false).
        /// </summary>
        bool isCustomer;

        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="AddPerson"/>.
        /// </summary>
        public AddPerson() {
            InitializeComponent();
        }

        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="AddPerson"/> z określonym typem osoby
        /// oraz wypełnia listy rozwijane danymi dla daty urodzenia.
        /// </summary>
        /// <param name="isCustomer">Wartość określająca, czy formularz dotyczy klienta.</param>
        public AddPerson(bool isCustomer) : this() {
            this.isCustomer = isCustomer;
            BxDay.ItemsSource = Enumerable.Range(1, 31).ToList();
            BxMonth.ItemsSource = Enumerable.Range(1, 12).ToList();
            BxYear.ItemsSource = Enumerable.Range(1900, 108).ToList();
        }

        /// <summary>
        /// Obsługuje zdarzenie kliknięcia przycisku "Wstecz". Zamyka aktualne okno.
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Argumenty zdarzenia.</param>
        public void Click_Back(object sender, RoutedEventArgs e) {
            this.Close();
        }

        /// <summary>
        /// Obsługuje zdarzenie kliknięcia przycisku "Dodaj". 
        /// Przeprowadza walidację, tworzy odpowiedni obiekt i zapisuje go w bazie danych <see cref="ProjectDbContext"/>.
        /// </summary>
        /// <param name="sender">Obiekt wywołujący zdarzenie.</param>
        /// <param name="e">Argumenty zdarzenia.</param>
        public void Click_AddPerson(object sender, RoutedEventArgs e) {
            bool success = false;

            if (checkBoxesInput(this))
                if (isCustomer) {
                    try {
                        using (var db = new ProjectDbContext()) {
                            DateTime d1 = new((int)BxYear.SelectedItem, (int)BxMonth.SelectedItem, (int)BxDay.SelectedItem);
                            Customer customer = new(TxtFName.Text, TxtLName.Text, TxtPesel.Text, TxtTelNmb.Text, d1);
                            db.Customers.Add(customer);
                            db.SaveChanges();
                            success = true;
                        }
                    }
                    catch {
                        MessageBox.Show("Niepoprawna data.");
                    }
                }
                else {
                    try {
                        using (var db = new ProjectDbContext()) {
                            DateTime d1 = new((int)BxYear.SelectedItem, (int)BxMonth.SelectedItem, (int)BxDay.SelectedItem);
                            Employee employee = new(TxtFName.Text, TxtLName.Text, TxtPesel.Text, TxtTelNmb.Text, d1);
                            db.Employees.Add(employee);
                            db.SaveChanges();
                            success = true;
                        }
                    }
                    catch {
                        MessageBox.Show("Niepoprawna data.");
                    }
                }
            if (success) {
                if (isCustomer)
                    MessageBox.Show("Poprawnie dodano nowego klienta");
                else
                    MessageBox.Show("Poprawnie dodano nowego pracownika");
                this.Close();
            }
        }

        /// <summary>
        /// Sprawdza, czy wszystkie wymagane pola tekstowe zostały wypełnione oraz czy wybrano datę.
        /// </summary>
        /// <param name="addPerson">Instancja okna formularza do sprawdzenia.</param>
        /// <returns>Zwraca true, jeśli wszystkie pola są poprawnie wypełnione; w przeciwnym razie false.</returns>
        static bool checkBoxesInput(AddPerson addPerson) {
            if (addPerson.TxtFName.Text == "" || addPerson.TxtLName.Text == ""
               || addPerson.TxtPesel.Text == "" || addPerson.TxtTelNmb.Text == "") {
                MessageBox.Show("Źle wypełnione pola.");
                return false;
            }
            if (addPerson.BxYear.SelectedItem == null || addPerson.BxMonth.SelectedItem == null || addPerson.BxDay.SelectedItem == null) {
                MessageBox.Show("Niepoprawna data.");
                return false;
            }
            return true;
        }
    }
}