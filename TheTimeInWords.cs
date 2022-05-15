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
    
    private static string[] numbers = new string[] { "zero", "one", "two",
        "three", "four", "five", "six", "seven", "eight", "nine", "ten",
        "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen",
        "seventeen", "eighteen", "nineteen", "twenty", "twenty one",
        "twenty two", "twenty three", "twenty four", "twenty five",
        "twenty six", "twenty seven", "twenty eight", "twenty nine" };

    public static string timeInWords(int h, int m)
    {
        string words = "";
        
        if (m == 0)
            words = numbers[h] + " o' clock";
        else if (m == 1)
            words = numbers[m] + " minute past " + numbers[h];
        else if (m == 15)
            words = "quarter past " + numbers[h];
        else if (m < 30)
            words = numbers[m] + " minutes past " + numbers[h];
        else if (m == 30)
            words = "half past " + numbers[h];
        else if (m == 45)
            words = "quarter to " + numbers[h + 1 == 13 ? 1 : h + 1];
        else if (m == 59)
            words = numbers[60 - m] + " minute to " + numbers[h + 1 == 13 ? 1 : h + 1];
        else
            words = numbers[60 - m] + " minutes to " + numbers[h + 1 == 13 ? 1 : h + 1];
        
        return words;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int h = Convert.ToInt32(Console.ReadLine().Trim());

        int m = Convert.ToInt32(Console.ReadLine().Trim());

        string result = Result.timeInWords(h, m);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
