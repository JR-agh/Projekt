using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1 {
    public class TransactionTypeException : Exception {
        public TransactionTypeException() : base() { }
        public TransactionTypeException(TransactionType type)
            : base($"{type} jest błędnym typem operacji.") {

        }
    }
}
