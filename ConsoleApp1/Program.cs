using ConsoleApp1;

internal class Program {
    private static void Main(string[] args) {
        test1();
    }
    static void test1() {
        DateTime d1 = DateTime.Now;
        Customer c1 = new("Elias", "Olson", "01234567890", "123-123-123", d1);
        Console.WriteLine(c1.FirstName);
    }
}