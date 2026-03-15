using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Transactions;

namespace ConsoleApp1 {
    /// <summary>
    /// Reprezentuje konto bankowe z obsługą blokad, transakcji i doradcy.
    /// </summary>
    public class Account : IComparable, IEquatable<Account>, IComparer<Account>, IJSONSaveLoad<Account> {
        decimal balance;
        Customer owner;
        string ownersPesel;
        Employee? advisor;
        bool isRestricted;
        string accountNumber;
        List<Transaction> sentTransactions;
        List<Transaction> receivedTransactions;
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
            SentTransactions = new();
            ReceivedTransactions = new();
            owner.AssignAccount(this);
            OwnersPesel = owner.Pesel;
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
            SentTransactions = new();
            ReceivedTransactions = new();
        }
        /// <summary>
        /// Czy konto jest zablokowane (np. z powodu dużej kwoty transakcji).
        /// </summary>
        public bool IsRestricted { get => isRestricted; set => isRestricted = value; }
		public Employee Advisor { get => advisor; set => advisor = value; }
		public Customer Owner { get => owner; set => owner = value; }
        /// <summary>
        /// Saldo konta.
        /// </summary>  
        public decimal Balance { get => balance; set => balance = value; }
        public string AccountNumber { get => accountNumber; set => accountNumber = value; }
		public Transaction? TransactionOnHold { get => transactionOnHold; set => transactionOnHold = value; }
        /// <summary>
        /// Przypisany numer PESEL właściciela (Klucz podstawowy dla DB).
        /// </summary>
        [Key]
        public string OwnersPesel { get => ownersPesel; set => ownersPesel = value; }
        public List<Transaction> SentTransactions { get => sentTransactions; set => sentTransactions = value; }
        public List<Transaction> ReceivedTransactions { get => receivedTransactions; set => receivedTransactions = value; }
        /// <summary>
        /// Przypisuje doradcę do konta i aktualizuje listę kont doradcy.
        /// </summary>
        public void AddAdvisor(Employee advisor) {
            Advisor = advisor;
            advisor.AddAccount(this);
        }
        /// <summary>
        /// Nakłada restrykcję na konto.
        /// </summary>
        void SetRestriction(Transaction transaction) {
            IsRestricted = true;
            TransactionOnHold = transaction;
            Advisor.Notify(transaction);
        }
        /// <summary>
        /// Zdejmuje restrykcję z konta i finalizuje wstrzymaną transakcję.
        /// </summary>
        /// <exception cref="Exception">Rzucany, gdy nie ma transakcji do zatwierdzenia.</exception>
        public void RemoveRestricion() {
            IsRestricted = false;
            if (TransactionOnHold == null)
                throw new Exception("Brak transakcji do zatwierdzenia.");
            this.BookTransaction(TransactionOnHold);
            TransactionOnHold = null;
        }
        /// <summary>
        /// Przeprowadza transakcję na koncie.
        /// </summary>
		public bool ProccessTransaction(Transaction transaction) {
            if (this.CheckTransaction(transaction)) {
                this.BookTransaction(transaction);
                return true;
            }
            else
                return false;
        }
        /// <summary>
        /// Sprawdza, czy transakcja wymaga weryfikacji (kwota > 10000).
        /// </summary>
        /// <returns>True, jeśli transakcja może być procesowana natychmiast.</returns>
        public bool CheckTransaction(Transaction transaction) {
            if (transaction.Amount > 10000) {
                this.SetRestriction(transaction);
                return false;
            }
            else
                return true;
        }
        /// <summary>
        /// Aktualizuje saldo konta na podstawie typu transakcji.
        /// </summary>
		public void BookTransaction(Transaction transaction) {
            
            if ((transaction.Type == TransactionType.Transfer && transaction.Sender == this) || transaction.Type == TransactionType.Withdrawal) {
                Balance -= transaction.Amount;
                //SentTransactions.Add(transaction);
            }
            else {
                Balance += transaction.Amount;
                //ReceivedTransactions.Add(transaction);
            }
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
        /// <summary>
        /// Wczytuje dane konta z pliku JSON.
        /// </summary>
        public static Account LoadFromJSON(string fileName) {
			string jsonString = File.ReadAllText(fileName);
			Account account = JsonSerializer.Deserialize<Account>(jsonString);
			return account;
		}
        /// <summary>
        /// Zapisuje dane konta do pliku JSON.
        /// </summary>
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
        public override string ToString() {
            return $"{AccountNumber} - {Owner.FirstName} {Owner.LastName}";
        }
    }
}
