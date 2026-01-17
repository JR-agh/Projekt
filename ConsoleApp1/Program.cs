using ConsoleApp1;

internal class Program {
    private static void Main(string[] args) {
        test1();
    }
    static void test1() {
        DateTime d1 = DateTime.Now;
        Customer c1 = new("Elias", "Olson", "01234567890", "123-123-123", d1);
        Console.WriteLine(c1.FirstName);
        Account acc1 = new(c1);
        Employee e1 = new("Juliette", "Swan", "02345678901", "234-234-234", d1);
        Console.WriteLine(acc1.Advisor); //null
        acc1.AddAdvisor(e1);
        Console.WriteLine(acc1.Advisor.Accounts[0].Balance);
    }
}