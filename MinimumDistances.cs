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
     * Complete the 'minimumDistances' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts INTEGER_ARRAY a as parameter.
     */

    // Use a Dictionary<int, List<int>> to store each unique value and all
    // of its indexes. After populating the Dictionary, iterate through it
    // and subtract the first value of each list from the second value in
    // that list. This is the minimum distance for that key value. The
    // smallest such difference in the Dictionary is the minimum for the
    // entire array, a.
    public static int minimumDistances(List<int> a)
    {
        int minimum = int.MaxValue;
        Dictionary<int, List<int>> d = new Dictionary<int, List<int>>();
        
        for (int i = 0; i < a.Count; i++)
        {
            if (d.ContainsKey(a[i]))
                d[a[i]].Add(i);
            else
                d.Add(a[i], new List<int>() { i });
        }
        
        foreach (KeyValuePair<int, List<int>> kvp in d)
        {
            if (kvp.Value.Count > 1 && kvp.Value[1] - kvp.Value[0] < minimum)
                minimum = kvp.Value[1] - kvp.Value[0];
        }
        
        return minimum < int.MaxValue ? minimum : -1;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> a = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(aTemp => Convert.ToInt32(aTemp)).ToList();

        int result = Result.minimumDistances(a);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
