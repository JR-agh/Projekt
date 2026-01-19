using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleApp1 {
    public class Customer : Person, IJSONSaveLoad<Customer> {
        Account? personalAccount;

        Account? PersonalAccount { get => personalAccount; set => personalAccount = value; }

        public Customer() : base() { }

        public Customer(string firstName, string lastName, string pesel, string telNumber, DateTime dateOfBirth) :
            base(firstName, lastName, pesel, telNumber, dateOfBirth) {

        }
		public void AssignAccount(Account account) {
            PersonalAccount = account;
        }
        public override bool Equals(Person? other) {
            if (other == null)
                return false;
            if(this.Pesel == other.Pesel)
                return true;
            return false;
        }
        public static Customer LoadFromJSON(string fileName) {
			string jsonString = File.ReadAllText(fileName);
			Customer customer = JsonSerializer.Deserialize<Customer>(jsonString);
			return customer;
		}
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
    }
}
