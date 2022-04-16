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
    // Faster than Math.Pow() which doesn't have an integer overload for some reason.
    // Calculates x^y
    public static int pow(int x, int y)
    {
        if (y == 0)
            return 1;
        int z = x;
        for (int i = 1; i < y; i++)
            z *= x;
        return z;
    }
    
    public static List<int> splitInt(int n)
    {
        List<int> digits = new List<int>();
        int numberOfDigits = 0;

        while (n / pow(10, numberOfDigits) > 0)
            numberOfDigits++;
        
        for (int i = 0; i < numberOfDigits; i++)
           digits.Add((n % pow(10, i + 1) / pow(10, i)));
        
        return digits;
    }

    /*
     * Complete the 'findDigits' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts INTEGER n as parameter.
     */

    public static int findDigits(int n)
    {
        int count = 0;
        List<int> digits = splitInt(n);
        foreach (int d in digits)
        {
            if (d > 0 && n % d == 0)
                count++;
        }
        return count;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int t = Convert.ToInt32(Console.ReadLine().Trim());

        for (int tItr = 0; tItr < t; tItr++)
        {
            int n = Convert.ToInt32(Console.ReadLine().Trim());

            int result = Result.findDigits(n);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
