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

    public static List<int> breakingRecords(List<int> scores)
    {
        int min = scores[0];
        int max = scores[0];
        int minCount = 0;
        int maxCount = 0;

        for (int i = 1; i < scores.Count; i++)
        {
            if (scores[i] < min)
            {
                minCount++;
                min = scores[i];
            }

            if (scores[i] > max)
            {
                maxCount++;
                max = scores[i];
            }
        }
        return new List<int>() { maxCount, minCount };
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> scores = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(scoresTemp => Convert.ToInt32(scoresTemp)).ToList();

        List<int> result = Result.breakingRecords(scores);

        textWriter.WriteLine(String.Join(" ", result));

        textWriter.Flush();
        textWriter.Close();
    }
}
