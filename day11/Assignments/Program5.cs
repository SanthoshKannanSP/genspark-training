public class Program5
{
    private int _count = 10;
    private int _divisibility = 7;

    private void FilterNumbers(int[] numbers)
    {
        var result = numbers.Where(number => number % _divisibility == 0);
        Console.WriteLine($"Numbers divisible by {_divisibility}");
        foreach (var number in result)
            Console.WriteLine(number);
        Console.WriteLine($"Count: {result.Count()}");
    }
    public void Run()
    {   
        int[] numbers = new int[_count];
        Console.WriteLine($"Enter {_count} numbers:");
        for (int i = 0; i < _count; i++)
        {
            if (!int.TryParse(Console.ReadLine(), out numbers[i]))
            {
                Console.WriteLine("Enter a valid number!");
                return;
            }
        }
        FilterNumbers(numbers);
    }
}
