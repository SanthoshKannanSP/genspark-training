public class Program7
{
    public int[] RotateArray(int[] numbers)
    {
        int size = numbers.Length;
        int[] result = new int[size];
        for(int i=1;i<size;i++)
        {
            result[i-1] = numbers[i];
        }
        result[size - 1] = numbers[0];
        return result;
    }
    public void Run()
    {
        int[] numbers = new int[] { 10, 20, 30, 40, 50 };
        var rotatedNumbers = RotateArray(numbers);
        foreach(int i in rotatedNumbers)
        {
            Console.WriteLine(i);
        }
    }
}