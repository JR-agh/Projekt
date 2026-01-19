using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace ConsoleApp1 {
    public class Account : IComparable, IEquatable<Account>, IComparer<Account>, IJSONSaveLoad<Account> {
        decimal balance;
        Customer owner;
        Employee? advisor;
        bool isRestricted;
        string accountNumber;
        List<Transaction> transactions;
        Transaction? transactionOnHold;

        public Account() {

        }

        public Account(Customer owner) {
            IsRestricted = false;
            Balance = 0;
            Owner = owner;
            Advisor = null;
            StringBuilder sb = new();
            Random rnd = new();
            for (int i = 0; i < 26; i++)
                sb.Append(rnd.Next(1, 10));
            AccountNumber = sb.ToString();
            TransactionOnHold = null;
            Transactions = [];
        }

        public Account(Customer owner, Employee advisor) {
            IsRestricted = false;
            Balance = 0;
            Owner = owner;
            Advisor = advisor;
            StringBuilder sb = new();
            Random rnd = new();
            for (int i = 0; i < 26;  i++)
                sb.Append(rnd.Next(1, 10));
            AccountNumber = sb.ToString();
            TransactionOnHold = null;
            Transactions = [];
        }

        public bool IsRestricted { get => isRestricted; set => isRestricted = value; }
		public Employee Advisor { get => advisor; set => advisor = value; }
		public Customer Owner { get => owner; set => owner = value; }
        public decimal Balance { get => balance; set => balance = value; }

		public List<Transaction> Transactions { get => transactions; set => transactions = value; }
        public string AccountNumber { get => accountNumber; set => accountNumber = value; }
		public Transaction? TransactionOnHold { get => transactionOnHold; set => transactionOnHold = value; }

        public void AddAdvisor(Employee advisor) {
            Advisor = advisor;
            advisor.AddAccount(this);
        }

        void SetRestriction(Transaction transaction) {
            IsRestricted = true;
            TransactionOnHold = transaction;
            Advisor.Notify(transaction);
        }

        public void RemoveRestricion() {
            IsRestricted = false;
            if (TransactionOnHold == null)
                throw new Exception("Brak transakcji do zatwierdzenia.");
            this.BookTransaction(TransactionOnHold);
            TransactionOnHold = null;
        }

		public bool ProccessTransaction(Transaction transaction) {
            if (this.CheckTransaction(transaction)) {
                this.BookTransaction(transaction);
                return true;
            }
            else
                return false;
        }
        public bool CheckTransaction(Transaction transaction) {
            if (transaction.Amount > 10000) {
                this.SetRestriction(transaction);
                return false;
            }
            else
                return true;
        }
		public void BookTransaction(Transaction transaction) {
            Transactions.Add(transaction);
            if ((transaction.Type == TransactionType.Transfer && transaction.Sender == this) || transaction.Type == TransactionType.Withdrawal)
                Balance -= transaction.Amount;
            else
                Balance += transaction.Amount;
        }
        public int CompareTo(object? obj) {
            if (obj == null) return 1;
            if (obj is Account other) {
                if (other.Balance > this.Balance)
                    return -1;
                if (other.Balance == this.Balance)
                    return 0;
                else
                    return 1;
            }
            else
                return 1;
        }
        public bool Equals(Account? other) {
            if(other == null) return false;
            if(other.AccountNumber == this.AccountNumber)
                return true;
            else
                return false;
        }
        public int Compare(Account? x, Account? y) {
            if(x == null || y == null) return 1;
            if(x.balance >  y.balance) return -1;
            if(x.balance == y.balance) return 0;
            return 1;
        }
        public static Account LoadFromJSON(string fileName) {
			string jsonString = File.ReadAllText(fileName);
			Account account = JsonSerializer.Deserialize<Account>(jsonString);
			return account;
		}
        public void SaveToJSON() {
			var options = new JsonSerializerOptions {
				WriteIndented = true,
				ReferenceHandler = ReferenceHandler.Preserve,
				IncludeFields = true
			};
			StringBuilder sb = new("account");
            sb.Append(this.AccountNumber);
            sb.Append(".json");
            string fileName = sb.ToString();
            string jsonString = JsonSerializer.Serialize(this, options);
            File.WriteAllText(fileName, jsonString);
        }
    }
}
