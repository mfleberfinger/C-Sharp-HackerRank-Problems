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
     * Complete the 'circularArrayRotation' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts following parameters:
     *  1. INTEGER_ARRAY a
     *  2. INTEGER k
     *  3. INTEGER_ARRAY queries
     */

    public static List<int> circularArrayRotation(List<int> a, int k, List<int> queries)
    {
        // The best way to do this problem is probably to come up with a formula
        // (modular arithmetic) to translate between a given index in `a` and the
        // corresponding index in the rotated array in a single operation, then
        // use that to pick the desired elements out of `a` without actually rotating `a`.
        // ...
        // Can't figure this out... it seems like we need a way to do an inverse modulo
        // operation or a formula for a left rotation of the indices to answer the
        // question "What number will be at index j in the rotated array?" rather than
        // the much easier "At which index j does the element from index i in the original
        // end up in the rotated array (answer: (i + k) % n = j)?"
        int[] b = new int[a.Count];
        for (int i = 0; i < a.Count; i++)
            b[(i + k) % a.Count] = a[i];
        List<int> results = new List<int>(queries.Count);
        foreach (int i in queries)
            results.Add(b[i]);
        return results;        
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int n = Convert.ToInt32(firstMultipleInput[0]);

        int k = Convert.ToInt32(firstMultipleInput[1]);

        int q = Convert.ToInt32(firstMultipleInput[2]);

        List<int> a = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(aTemp => Convert.ToInt32(aTemp)).ToList();

        List<int> queries = new List<int>();

        for (int i = 0; i < q; i++)
        {
            int queriesItem = Convert.ToInt32(Console.ReadLine().Trim());
            queries.Add(queriesItem);
        }

        List<int> result = Result.circularArrayRotation(a, k, queries);

        textWriter.WriteLine(String.Join("\n", result));

        textWriter.Flush();
        textWriter.Close();
    }
}
