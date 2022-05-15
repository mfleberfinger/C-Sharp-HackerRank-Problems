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
     * Complete the 'beautifulTriplets' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER d
     *  2. INTEGER_ARRAY arr
     */

    public static int beautifulTriplets(int d, List<int> a)
    {
        int count = 0;
        
        for (int i = 0; i < a.Count - 2; i++)
        {
            // In order for this to run quickly enough to solve all test
            // cases, we need to take advantage of the fact that the array is
            // sorted by stopping once the difference between candidate a[j]
            // and a[i] or a[k] and a[j] is greater than d.
            for (int j = i + 1; j < a.Count - 1 && a[j] - a[i] <= d; j++)
            {
                for (int k = j + 1; k < a.Count && a[k] - a[j] <= d; k++)
                {
                    if (a[j] - a[i] == a[k] - a[j] && a[k] - a[j] == d)
                        count++;
                }
            }
        }
        
        return count;
    }
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int n = Convert.ToInt32(firstMultipleInput[0]);

        int d = Convert.ToInt32(firstMultipleInput[1]);

        List<int> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();

        int result = Result.beautifulTriplets(d, arr);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
