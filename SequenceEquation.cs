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
     * Complete the 'permutationEquation' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts INTEGER_ARRAY p as parameter.
     */

    // Given array (list) p, where 1 <= p[x] <= n and 1 <= x <= n:
    // For each x, find any integer y such that p[p[y]] = x.
    // Return the y-values in a list in the order they are found.
    public static List<int> permutationEquation(List<int> p)
    {
        List<int> yValues = new List<int>();
        
        // The constraints say n <= 50.
        // A brute force solution is probably expected.
        // We will assume that y such that p[p[y]] = x exists for all permitted values of x.
        int y;
        for (int x = 1; x <= p.Count; x++)
        {
            // Need to start at y = 1 and subtract 1 from y and p[y - 1] because
            // the values in the array assume 1-indexing... bad instructions.
            y = 1;
            while (p[p[y - 1] - 1] != x)
                y++;
            yValues.Add(y);
        }
        
        return yValues;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> p = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(pTemp => Convert.ToInt32(pTemp)).ToList();

        List<int> result = Result.permutationEquation(p);

        textWriter.WriteLine(String.Join("\n", result));

        textWriter.Flush();
        textWriter.Close();
    }
}
