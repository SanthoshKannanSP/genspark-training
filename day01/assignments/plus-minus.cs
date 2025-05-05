using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using System.Text.RegularExpressions;
using System.Text;
using System;

class Result
{

    public static void plusMinus(List<int> arr)
    {
        int N = arr.Count;
        float positive = 0;
        float negative = 0;
        float zeroes = 0;
        foreach(var number in arr)
        {
            if(number > 0)
                positive++;
            else if(number < 0)
                negative++;
            else 
                zeroes++;
        }
        
        Console.WriteLine($"{positive/N:N6}");
        Console.WriteLine($"{negative/N:N6}");
        Console.WriteLine($"{zeroes/N:N6}");
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();

        Result.plusMinus(arr);
    }
}
