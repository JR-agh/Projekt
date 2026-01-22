using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ConsoleApp1 {
    public abstract class Person : IEquatable<Person> {
        string firstName;
        string lastName;
        string pesel;
        string telNumber;
        DateTime dateOfBirth;
        protected Person() { }
        protected Person(string firstName, string lastName, string pesel, string telNumber, DateTime dateOfBirth) {
            FirstName = firstName;
            LastName = lastName;
            Pesel = pesel;
            TelNumber = telNumber;
            DateOfBirth = dateOfBirth;
        }
        public string FullName => $"{FirstName} {LastName}";
        public string FirstName {get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        [Key]
        public string Pesel { get => pesel;
            set {
                Regex regex = new("^[0-9]{11}$");
                Match match = regex.Match(value);
                if (match.Success)
                    pesel = value;
                else
                    throw new ArgumentException("Podany pesel jest błędny.");
            }
        }
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
        public virtual void DisplayPersonalInfo() {
            Console.WriteLine($"{FirstName}, {LastName}, {Pesel}");
        }
        public abstract bool Equals(Person? other);
        public override string ToString() {
            return $"{FirstName} {LastName}, {Pesel}, tel.: {TelNumber}";
        }
    }
}
