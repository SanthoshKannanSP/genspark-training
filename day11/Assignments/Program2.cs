public class Program2
{
    public void Run()
    {
        int num1, num2;
        Console.Write("Enter a number: ");
        if (!int.TryParse(Console.ReadLine(), out num1))
        {
            Console.WriteLine("Enter a valid number");
            return;
        }
        Console.Write("Enter another number: ");
        if (!int.TryParse(Console.ReadLine(), out num2))
        {
            Console.WriteLine("Enter a valid number");
            return;
        }
        var maxNum = Math.Max(num1, num2);
        Console.WriteLine($"{maxNum} is the larger number");
    }
}