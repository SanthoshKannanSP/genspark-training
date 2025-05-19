public class Program3
{
    public void Run()
    {
        int num1, num2;
        char op;
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

        Console.Write("Enter operation you want to perform (+,-,*,/): ");
        op = Console.ReadLine().ElementAtOrDefault(0);
        double ans;
        switch (op)
        {
            case '+':
                ans = num1 + num2;
                break;
            case '-':
                ans = num1 - num2;
                break;
            case '*':
                ans = num1 * num2;
                break;
            case '/':
                if (num2 == 0)
                {
                    Console.WriteLine("Second number should not be zero");
                    return;
                }
                ans = num1 / num2;
                break;
            default:
                Console.WriteLine("Enter a valid operation");
                return;
        }
        Console.WriteLine($"{num1}{op}{num2}={ans}");
    }
}