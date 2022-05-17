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
     * Complete the 'workbook' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. INTEGER k
     *  3. INTEGER_ARRAY arr
     */

    // Iterate through all of the pages, keeping track of which chapter
    // we are in. For each page, p, determine whether problem p is on that page.
    // If so, increment the special problems counter.
    public static int workbook(int n, int k, List<int> arr)
    {
        int specialProblems = 0;
        int page = 1;
        // Sum of problems in this chapter on previous pages.
        int previousProblems = 0;
        
        foreach (int problems in arr)
        {
            // While we're still in this chapter
            while (previousProblems < problems)
            {
                // If the problem with the same number as this page is on this page
                // i.e. previousProblems < page <= previousProblems + (problems on this page)
                // Note that previousProblems + (problems on this page) = (highest problem
                //  number on this page) = (problems in this chapter) (if on the last page).
                if ((previousProblems < page && ((page <= previousProblems + k &&
                    previousProblems + k <= problems) || (page <= problems &&
                    problems - previousProblems < k))))
                {
                    specialProblems++;
                }
                previousProblems += k;
                page++;
            }
            previousProblems = 0;
        }
        
        return specialProblems;
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

        List<int> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();

        int result = Result.workbook(n, k, arr);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
