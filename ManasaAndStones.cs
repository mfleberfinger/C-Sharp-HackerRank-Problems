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
    /* Probably correct but definitely too slow.
    
    private static HashSet<int> recursiveResult;
    
    private static void RecursivePart(int n, int a, int b, int x = 0, int level = 1)
    {
        if (level < n)
        {
            RecursivePart(n, a, b, x + a, level + 1);
            RecursivePart(n, a, b, x + b, level + 1);
        }
        else
        {
            if (!recursiveResult.Contains(x))
                recursiveResult.Add(x);
        }
    }

    // Iterate through all possible sequences that can be formed by starting with
    // the number 0 and adding a and/or b n times. Return a list, sorted in
    // ascending order, containing each distinct integer that appears as
    // the last value in each sequence.
    // For example; If n = 3, a = 1, and b = 2; the sequences are
    //  0, 1, 2; 0, 1, 3; 0, 2, 3; and 0, 2, 4
    // and the returned list is [2, 3, 4] (notice that it is NOT [2, 3, 3, 4]
    //  because the duplicated value (3) only appears once).
    public static List<int> stones(int n, int a, int b)
    {
        //List<int> temp = RecursivePart(n, a, b);
        recursiveResult = new HashSet<int>();
        RecursivePart(n, a, b);
        List<int> result = recursiveResult.ToList();
        
        result.Sort();
        
        return result;        
    }
    */

    // Iterate through all possible sequences that can be formed by starting with
    // the number 0 and adding a and/or b n times. Return a list, sorted in
    // ascending order, containing each distinct integer that appears as
    // the last value in each sequence.
    // For example; If n = 3, a = 1, and b = 2; the sequences are
    //  0, 1, 2; 0, 1, 3; 0, 2, 3; and 0, 2, 4
    // and the returned list is [2, 3, 4] (notice that it is NOT [2, 3, 3, 4]
    //  because the duplicated value (3) only appears once).
    public static List<int> stones(int n, int a, int b)
    {
        // Don't actually iterate through all sequences.
        // Instead, iterate through all possible a and b sums.
        // The order in which we choose a and b when adding up stones doesn't matter.
        // The only thing that matters is how many of each number (a and b)
        // we have.
        List<int> results = new List<int>();
        HashSet<int> hashSet = new HashSet<int>();
        int sum;
        for (int aCount = 0; aCount < n; aCount++)
        {
            sum = aCount * a + (n - 1 - aCount) * b;
            if (!hashSet.Contains(sum))
                hashSet.Add(sum);
        }
        
        results = hashSet.ToList();
        results.Sort();
        return results;
    }
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int T = Convert.ToInt32(Console.ReadLine().Trim());

        for (int TItr = 0; TItr < T; TItr++)
        {
            int n = Convert.ToInt32(Console.ReadLine().Trim());

            int a = Convert.ToInt32(Console.ReadLine().Trim());

            int b = Convert.ToInt32(Console.ReadLine().Trim());

            List<int> result = Result.stones(n, a, b);

            textWriter.WriteLine(String.Join(" ", result));
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
