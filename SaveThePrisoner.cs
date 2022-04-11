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

    /*
     * Complete the 'saveThePrisoner' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. INTEGER m
     *  3. INTEGER s
     */

    public static int saveThePrisoner(int n, int m, int s)
    {     
        // The chairs in the problem are numbered 1 to n (inclusive).
        // For the math, assume the chairs are numbered 0 to (n - 1).
        // Then, chair number = ((s - 1) + m) % n
        // Because we have chairs numbered 1 through n we need to return n instead of 0.
		// NOTE FROM AFTER COMPLETING THE PROBLEM: From the editorial, this should
		// have been (M - 1 + S - 1) % N + 1). That would eliminate the need for the
		// ternary operator in the return statement.
        int finalChair = ((s - 1) + m) % n;
        return finalChair == 0 ? n : finalChair;
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
            string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

            int n = Convert.ToInt32(firstMultipleInput[0]);

            int m = Convert.ToInt32(firstMultipleInput[1]);

            int s = Convert.ToInt32(firstMultipleInput[2]);

            int result = Result.saveThePrisoner(n, m, s);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
