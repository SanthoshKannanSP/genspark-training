using System.Collections;

public class Program6
{
    private Dictionary<int,int> CountFrequency(int[] numbers)
    {
        var freq = new Dictionary<int,int>();
        foreach (var number in numbers)
        {
            if (freq.ContainsKey(number))
                freq[number] += 1;
            else
                freq[number] = 1;
        }
        return freq;
    }
    public void Run()
    {
        var numbers = new int[] { 1, 2, 2, 3, 4, 4, 4 };
        var freq = CountFrequency(numbers);
        foreach (int key in freq.Keys)
        {
            Console.WriteLine($"{key} occurs {freq[key]} times");
        }
    }
}
