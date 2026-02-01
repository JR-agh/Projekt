using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;

namespace ConsoleApp1 {
    /// <summary>
    /// Typy operacji bankowych.
    /// </summary>
    public enum TransactionType {
        [System.ComponentModel.Description("Przelew")]
        Transfer,
        [System.ComponentModel.Description("Depozyt")]
        Deposit,
        [System.ComponentModel.Description("Wypłata")]
        Withdrawal
    }
    /// <summary>
    /// Klasa reprezentująca pojedynczą operację finansową.
    /// </summary>
    public class Transaction : ICloneable, IJSONSaveLoad<Transaction> { 
        Guid transactionID;
        decimal amount;
        Account? sender;
        string? sendersPesel;
        string? recipientsPesel;
        Account? recipient;
        TransactionType type;
        public Transaction () { }
        /// <summary>
        /// Konstruktor dla depozytów i wypłat (transakcje jednoosobowe).
        /// </summary>
        /// <exception cref="TransactionTypeException">Gdy typ to Transfer (wymaga dwóch kont).</exception>
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
        /// <summary>
        /// Konstruktor dla przelewów między dwoma kontami.
        /// </summary>
        public Transaction(decimal amount, TransactionType type, Account account1, Account account2) {
            if (type != TransactionType.Transfer) {
                throw new TransactionTypeException(type);
            }
            Amount = amount;
            Sender = account1;
            SendersPesel = account1.OwnersPesel;
            Recipient = account2;
            RecipientsPesel = account2.OwnersPesel;
            TransactionID = Guid.NewGuid();
        }
        /// <summary>
        /// Unikalny identyfikator transakcji.
        /// </summary>
        [Key]
        public Guid TransactionID { get => transactionID; private set => transactionID = value; }
        public decimal Amount { get => amount; set => amount = value; }
        public TransactionType Type { get => type; set => type = value; }
        public Account Sender {
            get {
                //if(sender == null)
                //    throw new InvalidOperationException();
                return sender;
            }
            set => sender = value;
        }
        public Account Recipient {
            get {
                //if (recipient == null)
                //    throw new InvalidOperationException();
                return recipient;
            }
            set => recipient = value;
        }

        public string SendersPesel { get => sendersPesel; set => sendersPesel = value; }
        public string RecipientsPesel { get => recipientsPesel; set => recipientsPesel = value; }
        /// <summary>
        /// Zwraca sklonowany obiekt.
        /// </summary>
        public object Clone() {
            return (Transaction)MemberwiseClone();
        }
        /// <summary>
        /// Wczytuje dane transakcji z pliku JSON.
        /// </summary>
        public static Transaction LoadFromJSON(string fileName) {
            string jsonString = File.ReadAllText(fileName);
            Transaction transaction = JsonSerializer.Deserialize<Transaction>(jsonString);
            return transaction;
        }
        /// <summary>
        /// Zapisuje dane transakcji do pliku JSON.
        /// </summary>
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
            if (recipientsPesel == null)
                return $"Wypłata {TransactionID} o kwocie {Amount} z rachunku {sender.AccountNumber}.";
            if (sendersPesel == null)
                return $"Depozyt {TransactionID} o kwocie {Amount} na rachunek {recipient.AccountNumber}.";
            return $"Transakcja {TransactionID} o kwocie {Amount} z rachunku {sender.AccountNumber} na rachunek {recipient.AccountNumber}.";
        }
    }
}
