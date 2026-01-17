using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp1 {
    internal class Customer : Person {
        Account? personalAccount;

        internal Account? PersonalAccount { get => personalAccount; set => personalAccount = value; }

        Customer(String firstName, String lastName, String pesel, String telNumber, DateTime dateOfBirth) :
            base(firstName, lastName, pesel, telNumber, dateOfBirth) {

        }

        void AssignAccount(Account account) {
            PersonalAccount = account;
        }

        public override bool Equals(Person? other) {
            if (other == null)
                return false;
            if(this.Pesel == other.Pesel)
                return true;
            return false;
        }
    }
}
