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
        [System.ComponentModel.Description("Przelew")]
        Transfer,
        [System.ComponentModel.Description("Depozyt")]
        Deposit,
        [System.ComponentModel.Description("Wypłata")]
        Withdrawal
    }
    public class Transaction : ICloneable, IJSONSaveLoad<Transaction> { 
        Guid transactionID;
        decimal amount;
        Account? sender;
        string sendersPesel;
        string recipientsPesel;
        Account? recipient;
        TransactionType type;
        public Transaction () { }
        public Transaction(decimal amount, TransactionType type, Account account1) {
            if(type == TransactionType.Transfer)
                throw new TransactionTypeException(type);
            if (type == TransactionType.Deposit) {
                Recipient = account1;
                RecipientsPesel = account1.OwnersPesel;
            }
            else {
                Sender = account1;
                SendersPesel = account1.OwnersPesel;
            }
            Amount = amount;
            TransactionID = Guid.NewGuid();
        }
        public Transaction(decimal amount, TransactionType type, Account account1, Account account2) {
            if (type != TransactionType.Transfer) {
                throw new TransactionTypeException(type);
            }
            Amount = amount;
            Sender = account1;
            SendersPesel = account1.OwnersPesel;
            Recipient = account2;
            RecipientsPesel = account2.OwnersPesel;
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

        public string SendersPesel { get => sendersPesel; set => sendersPesel = value; }
        public string RecipientsPesel { get => recipientsPesel; set => recipientsPesel = value; }

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
        public override string ToString() {
            StringBuilder sb = new();
            switch (Type) {
                case TransactionType.Transfer:
                    sb.Append($"Transakcja {TransactionID} o kwocie {Amount} z rachunku {sender.AccountNumber} na rachunek {recipient.AccountNumber}");
                    break;
                case TransactionType.Deposit:
                    sb.Append($"Depozyt {TransactionID} o kwocie {Amount} na rachunek {recipient.AccountNumber}");
                    break;
                case TransactionType.Withdrawal:
                    sb.Append($"Wypłata {TransactionID} o kwocie {Amount} z rachunku {sender.AccountNumber}");
                    break;
            }
            return sb.ToString();
        }
    }
}
