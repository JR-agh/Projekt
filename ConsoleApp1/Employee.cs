using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1 {
    internal class Employee : Person {
        int employeeID;
        List<Account> accounts;
        List<Transaction> susTransactions;

        public int EmployeeID { get => employeeID; set => employeeID = value; }
        internal List<Account> Accounts { get => accounts; set => accounts = value; }
        internal List<Transaction> SusTransactions { get => susTransactions; set => susTransactions = value; }

        public Employee(string firstName, string lastName, string pesel, string telNumber, DateTime dateOfBirth) :
            base(firstName, lastName, pesel, telNumber, dateOfBirth) {
            Accounts = [];
            SusTransactions = [];
        }

        public void AddAccount(Account account) {
            Accounts.Add(account);
        }

        public void Approve(Transaction transaction) {
            transaction.Sender.RemoveRestricion();
        }

        public void Notify(Transaction transaction) {
            SusTransactions.Add(transaction);
        }

        public override bool Equals(Person? other) {
            if (other == null)
                return false;
            if (this.Pesel == other.Pesel)
                return true;
            return false;
        }
        public override void SaveToJSON() {
            StringBuilder sb = new("employee");
            sb.Append(this.Pesel);
            sb.Append(".json");
            string fileName = sb.ToString();
            string jsonString = JsonSerializer.Serialize(this);
            File.WriteAllText(fileName, jsonString);
        }
    }
}
