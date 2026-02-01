using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace ConsoleApp1 {
    /// <summary>
    /// Reprezentuje klienta banku, który może posiadać konto osobiste.
    /// </summary>
    public class Customer : Person, IJSONSaveLoad<Customer> {
        Account? personalAccount;

        /// <summary>
        /// Przypisane konto osobiste klienta.
        /// </summary>
        public Account? PersonalAccount { get => personalAccount; set => personalAccount = value; }

        public Customer() : base() { }

        public Customer(string firstName, string lastName, string pesel, string telNumber, DateTime dateOfBirth) :
            base(firstName, lastName, pesel, telNumber, dateOfBirth) {
        }

        /// <summary>
        /// Przypisuje konto do klienta.
        /// </summary>
        /// <param name="account">Instancja konta.</param>
        public void AssignAccount(Account account) {
            PersonalAccount = account;
        }

        public override bool Equals(Person? other) {
            if (other == null) return false;
            if (this.Pesel == other.Pesel) return true;
            return false;
        }

        /// <summary>
        /// Wczytuje dane klienta z pliku JSON.
        /// </summary>
        public static Customer LoadFromJSON(string fileName) {
            string jsonString = File.ReadAllText(fileName);
            Customer customer = JsonSerializer.Deserialize<Customer>(jsonString);
            return customer;
        }

        /// <summary>
        /// Zapisuje dane klienta do pliku JSON o nazwie opartej na numerze PESEL.
        /// </summary>
        public void SaveToJSON() {
            var options = new JsonSerializerOptions {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.Preserve,
                IncludeFields = true
            };
            StringBuilder sb = new("customer");
            sb.Append(this.Pesel);
            sb.Append(".json");
            string fileName = sb.ToString();
            string jsonString = JsonSerializer.Serialize(this, options);
            File.WriteAllText(fileName, jsonString);
        }

        public override string ToString() {
            return base.ToString();
        }
    }
}