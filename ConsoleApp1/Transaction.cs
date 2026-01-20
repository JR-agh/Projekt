using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ConsoleApp1 {
    public enum TransactionType {
        Transfer,
        Deposit,
        Withdrawal
    }
    public class Transaction : ICloneable, IJSONSaveLoad<Transaction> { 
        Guid transactionID;
        decimal amount;
        Account? sender;
        Account? recipient;
        TransactionType type;
        public Transaction () { }
        public Transaction(decimal amount, TransactionType type, Account account1) {
            if(type == TransactionType.Transfer)
                throw new TransactionTypeException(type);
            if(type == TransactionType.Deposit)
                Recipient = account1;
            else
                Sender = account1;
            Amount = amount;
            TransactionID = Guid.NewGuid();
        }
        public Transaction(decimal amount, TransactionType type, Account account1, Account account2) {
            if (type != TransactionType.Transfer) {
                throw new TransactionTypeException(type);
            }
            Amount = amount;
            Sender = account1;
            Recipient = account2;
        }
        [Key]
        public Guid TransactionID { get => transactionID; private set => transactionID = value; }
        public decimal Amount { get => amount; set => amount = value; }
        public TransactionType Type { get => type; set => type = value; }
        public Account Sender {
            get {
                if(sender == null)
                    throw new InvalidOperationException();
                return sender;
            }
            set => sender = value;
        }
        public Account Recipient {
            get {
                if (recipient == null)
                    throw new InvalidOperationException();
                return recipient;
            }
            set => recipient = value;
        }
        public object Clone() {
            return (Transaction)MemberwiseClone();
        }
        public static Transaction LoadFromJSON(string fileName) {
            string jsonString = File.ReadAllText(fileName);
            Transaction transaction = JsonSerializer.Deserialize<Transaction>(jsonString);
            return transaction;
        }
        public void SaveToJSON() {
            var options = new JsonSerializerOptions {
                WriteIndented = true,
                ReferenceHandler = ReferenceHandler.Preserve,
                IncludeFields = true
            };
            StringBuilder sb = new("transaction");
            sb.Append(this.TransactionID);
            sb.Append(".json");
            string fileName = sb.ToString();
            string jsonString = JsonSerializer.Serialize(this, options);
            File.WriteAllText(fileName, jsonString);
        }
    }
}
