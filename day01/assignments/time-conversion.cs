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

    public static string timeConversion(string s)
    {
        int hours = int.Parse(s.Substring(0, 2));
        string period = s.Substring(8, 2);
        if (period == "PM" && hours != 12)
            hours += 12;
        else if (period == "AM" && hours == 12)
            hours = 0;

        return hours.ToString().PadLeft(2, '0') +
            s.Substring(2, 6);
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string s = Console.ReadLine();

        string result = Result.timeConversion(s);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
