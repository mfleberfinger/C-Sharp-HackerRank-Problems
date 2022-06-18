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
     * Complete the 'larrysArray' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts INTEGER_ARRAY A as parameter.
     */

    // There are at most 1,000 elements. Is backtracking feasible?
    // The recursion could bottom out when either of the following occurs:
    //  1. A is in the same order as at the start. (Sorting is impossible on this branch.)
    //  2. A is sorted. (Done. Sorting is possible.)
    // Start -> n recursive calls -> Second level of recursion tree -> n - 1 recursive calls...
    // n * (n - 1) * (n - 2) * ... This is n! No Good.
    
    // From the discussion: The array is sortable if, and only if, the number of
    //  inversions is a multiple of R - 1, where R is the number of elements we
    //  must rotate with each operation (3, in this case).
    // The number of inversions can be calculated either by iterating through the
    //  entire array, A, and, for each i and j such that i < j < n, counting all pairs
    //  (i, j) such that A[i] > A[j] OR by performing merge sort and counting the
    //  total number of swaps performed during all of the merge steps.
    // The first solution is O(n^2) and the second solution is O(nlgn). The n^2 solution
    //  is probably fine because we only have up to 1000 elements.
    public static string larrysArray(List<int> A)
    {
        int inversions = 0;
        
        for (int i = 0; i < A.Count - 1; i++)
        {
            for (int j = i + 1; j < A.Count; j++)
            {
                if (A[i] > A[j])
                    inversions++;
            }
        }
        
        return inversions % 2 == 0 ? "YES" : "NO";
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

            List<int> A = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(ATemp => Convert.ToInt32(ATemp)).ToList();

            string result = Result.larrysArray(A);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}