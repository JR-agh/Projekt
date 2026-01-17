using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace ConsoleApp1 {
    enum TransactionType {
        Transfer,
        Deposit,
        Withdrawal
    }
    internal class Transaction : ICloneable, IJSONSaveLoad {
        int transactionID;
        decimal amount;
        Account? sender;
        Account? recipient;
        TransactionType type;
        Transaction(decimal amount, TransactionType type, Account account1) {
            if(type == TransactionType.Transfer) {
                throw new TransactionTypeException();
            }
            this.Amount = amount;
            if(type == TransactionType.Deposit) {
                this.Recipient = account1;
            }
            else {
                this.Sender = account1;
            }
            transactionID = 0;
        }
        Transaction(decimal amount, TransactionType type, Account account1, Account account2) {
            if (type != TransactionType.Transfer) {
                throw new TransactionTypeException();
            }
            Amount = amount;
            Sender = account1;
            Recipient = account2;
        }
        public int TransactionID { get => transactionID; set => transactionID = value; }
        public decimal Amount { get => amount; set => amount = value; }
        public TransactionType Type { get => type; set => type = value; }
        internal Account Sender {
            get {
                if(sender == null)
                    throw new InvalidOperationException();
                return sender;
            }
            set => sender = value;
        }
        internal Account Recipient {
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
        public void LoadToJSON() {
            throw new NotImplementedException();
        }
        public void SaveToJSON() {
            StringBuilder sb = new("transaction");
            sb.Append(this.TransactionID);
            sb.Append(".json");
            string fileName = sb.ToString();
            string jsonString = JsonSerializer.Serialize(this);
            File.WriteAllText(fileName, jsonString);
        }
    }
}
