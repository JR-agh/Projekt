using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ConsoleApp1 {
    /// <summary>
    /// Klasa abstrakcyjna reprezentująca osobę w systemie.
    /// Zawiera podstawowe dane osobowe oraz walidację PESEL i numeru telefonu.
    /// </summary>
    public abstract class Person : IEquatable<Person> {
        string firstName;
        string lastName;
        string pesel;
        string telNumber;
        DateTime dateOfBirth;

        /// <summary>
        /// Konstruktor bezparametrowy wymagany przez serializację.
        /// </summary>
        protected Person() { }

        /// <summary>
        /// Inicjalizuje nową instancję klasy <see cref="Person"/>.
        /// </summary>
        /// <param name="firstName">Imię osoby.</param>
        /// <param name="lastName">Nazwisko osoby.</param>
        /// <param name="pesel">11-cyfrowy numer PESEL.</param>
        /// <param name="telNumber">Numer telefonu w formacie XXX-XXX-XXX.</param>
        /// <param name="dateOfBirth">Data urodzenia.</param>
        protected Person(string firstName, string lastName, string pesel, string telNumber, DateTime dateOfBirth) {
            FirstName = firstName;
            LastName = lastName;
            Pesel = pesel;
            TelNumber = telNumber;
            DateOfBirth = dateOfBirth;
        }

        /// <summary>
        /// Zwraca pełne imię i nazwisko.
        /// </summary>
        public string FullName => $"{FirstName} {LastName}";

        public string FirstName { get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }

        /// <summary>
        /// Numer PESEL. Walidowany pod kątem posiadania dokładnie 11 cyfr.
        /// </summary>
        /// <exception cref="ArgumentException">Rzucany, gdy format PESEL jest nieprawidłowy.</exception>
        [Key]
        public string Pesel {
            get => pesel;
            set {
                Regex regex = new("^[0-9]{11}$");
                Match match = regex.Match(value);
                if (match.Success)
                    pesel = value;
                else
                    throw new ArgumentException("Podany pesel jest błędny.");
            }
        }

        /// <summary>
        /// Numer telefonu. Walidowany pod kątem formatu XXX-XXX-XXX.
        /// </summary>
        /// <exception cref="ArgumentException">Rzucany, gdy format numeru jest nieprawidłowy.</exception>
        public string TelNumber {
            get => telNumber;
            set {
                Regex regex = new("^[0-9]{3}-[0-9]{3}-[0-9]{3}$");
                Match match = regex.Match(value);
                if (match.Success)
                    telNumber = value;
                else
                    throw new ArgumentException("Podany nr telefonu jest błędny.");
            }
        }

        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }

        /// <summary>
        /// Wyświetla podstawowe informacje o osobie w konsoli.
        /// </summary>
        public virtual void DisplayPersonalInfo() {
            Console.WriteLine($"{FirstName}, {LastName}, {Pesel}");
        }

        /// <summary>
        /// Sprawdza równość z innym obiektem klasy Person na podstawie numeru PESEL.
        /// </summary>
        public abstract bool Equals(Person? other);

        public override string ToString() {
            return $"{FirstName} {LastName}, {Pesel}, tel.: {TelNumber}";
        }
    }
}