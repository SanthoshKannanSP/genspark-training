class Program8
{ 
    private int[] MergeArrays(int[] array1, int[] array2)
    {
        var result = array1.Concat(array2).ToArray();
        return result;
    }
    public void Run()
    {
        var array1 = new int[] { 1, 3, 5 };
        var array2 = new int[] { 2, 4, 6 };
        var result = MergeArrays(array1, array2);
        foreach ( var i in result )
            Console.WriteLine(i);
    }
}