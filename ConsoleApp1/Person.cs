using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ConsoleApp1 {
    internal abstract class Person : IEquatable<Person> {
        required internal  string firstName;
        required internal string lastName;
        required internal string pesel;
        required internal string telNumber;
        required internal DateTime dateOfBirth;

        protected Person(String firstName, String lastName, String pesel, String telNumber, DateTime dateOfBirth) {
            FirstName = firstName;
            LastName = lastName;
            Pesel = pesel;
            TelNumber = telNumber;
            DateOfBirth = dateOfBirth;
        }

        public string FirstName {get => firstName; set => firstName = value; }
        public string LastName { get => lastName; set => lastName = value; }
        public string Pesel { get => pesel;
            set {
                Regex regex = new("^[0-9]{11}$");
                Match match = regex.Match(value);
                if (match.Success)
                    pesel = value;
                else
                    throw new ArgumentException("Podany pesel jest bledny.");
            }
        }
        public string TelNumber { get => telNumber; set => telNumber = value; }
        public DateTime DateOfBirth { get => dateOfBirth; set => dateOfBirth = value; }

        public virtual void DisplayPersonalInfo() {

        }

        public abstract bool Equals(Person? other);
    }
}
