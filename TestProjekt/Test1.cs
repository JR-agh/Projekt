using ConsoleApp1;

namespace TestProjekt {
    [TestClass]
    public sealed class Test1 {
        [TestMethod]
        public void CustomerTest() {
            DateTime d1 = DateTime.Now;
            Customer c1 = new("Elias", "Olson", "01234567890", "123-123-123", d1);
            Assert.AreEqual("Elias", c1.FirstName);
            Assert.AreEqual("Olson", c1.LastName);
            Assert.AreEqual("01234567890", c1.Pesel);
        }
        [TestMethod]
        public void AccountTest() {
            DateTime d1 = DateTime.Now;
            Customer c1 = new("Elias", "Olson", "01234567890", "123-123-123", d1);
            Account a1 = new(c1);
            decimal testValue = 0;
            Assert.AreEqual(testValue, a1.Balance);
            Assert.AreEqual(false, a1.IsRestricted);
        }
        [TestMethod]
        public void CustomerTest2() {
            
            DateTime d1 = DateTime.Now;
            Customer c1;
            var exception = Assert.ThrowsException<ArgumentException>(() => c1 = new("Elias", "Olson", "0123456789", "123-123-123", d1));
            Assert.AreEqual("Podany pesel jest błędny.", exception.Message);
        }
        [TestMethod]
        public void EmployeeTest() {
            DateTime d1 = DateTime.Now;
            Employee e1 = new("Bella", "Swan", "02345678901", "234-234-234", d1);
            Assert.AreEqual("Bella", e1.FirstName);
            Assert.AreEqual("Swan", e1.LastName);
            Assert.AreEqual("02345678901", e1.Pesel);
        }
        [TestMethod]
        public void EmployeeAdvisorTest() {
            DateTime d1 = DateTime.Now;
            Employee e1 = new("Bella", "Swan", "02345678901", "234-234-234", d1);
            Customer c1 = new("Elias", "Olson", "01234567890", "123-123-123", d1);
            Account a1 = new(c1);
            Assert.AreEqual(null, a1.Advisor);
            a1.AddAdvisor(e1);
            Assert.AreEqual(e1, a1.Advisor);
            Assert.AreEqual("02345678901", a1.Advisor.Pesel);
        }
        [TestMethod]
        public void TransactionTest() {
            DateTime d1 = DateTime.Now;
            Customer c1 = new("Elias", "Olson", "01234567890", "123-123-123", d1);
            Account a1 = new(c1);
            decimal testValue = 10;
            Transaction t1 = new(testValue, TransactionType.Deposit, a1);
            a1.ProccessTransaction(t1);
            Assert.AreEqual(a1.Balance, testValue);
        }
        [TestMethod]
        public void TransactionTest2() {
            DateTime d1 = DateTime.Now;
            Customer c1 = new("Elias", "Olson", "01234567890", "123-123-123", d1);
            Account a1 = new(c1);
            decimal testValue = 10;
            Transaction t1;
            var exception = Assert.ThrowsException<TransactionTypeException>(() => t1 = new(testValue, TransactionType.Transfer, a1));
            Assert.AreEqual("Transfer jest błędnym typem operacji.", exception.Message);
        }
        [TestMethod]
        public void TransactionTest3() {
            DateTime d1 = DateTime.Now;
            Customer c1 = new("Elias", "Olson", "01234567890", "123-123-123", d1);
            Customer c2 = new("Bella", "Swan", "02345678901", "234-234-234", d1);
            Account a1 = new(c1);
            Account a2 = new(c2);
            a1.Balance = 20;
            decimal testValue = 10;
            Transaction t1 = new(testValue, TransactionType.Transfer, a1, a2);
            a1.ProccessTransaction (t1);
            a2.ProccessTransaction(t1);
            Assert.AreEqual(a2.Balance, 10);
            Assert.AreEqual(a1.Balance, 10);
        }
    }
}
