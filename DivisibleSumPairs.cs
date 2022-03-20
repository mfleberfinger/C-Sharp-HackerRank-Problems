/*
Given an array of integers and a positive integer , determine the number of pairs where and + is divisible by

.

Example


Three pairs meet the criteria: and

.

Function Description

Complete the divisibleSumPairs function in the editor below.

divisibleSumPairs has the following parameter(s):

    int n: the length of array 

    int ar[n]: an array of integers
    int k: the integer divisor

Returns
- int: the number of pairs

Input Format

The first line contains
space-separated integers, and .
The second line contains space-separated integers, each a value of

.

Constraints

Sample Input

STDIN           Function
-----           --------
6 3             n = 6, k = 3
1 3 2 6 1 2     ar = [1, 3, 2, 6, 1, 2]

Sample Output

 5

Explanation

Here are the
valid pairs when

:


*/

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
     * Complete the 'divisibleSumPairs' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. INTEGER k
     *  3. INTEGER_ARRAY l
     */

    // Given a List, l, of integers and a positive integer k, determine the number
    // of (i, j) pairs where i < j and l[i] + l[j] is divisible by k.
    public static int divisibleSumPairs(int n, int k, List<int> l)
    {
        // This probably needs to be done exhaustively.
        int count = 0;
        
        for (int i = 0; i < l.Count; i++) {
            for (int j = i + 1; j < l.Count; j++) {
                if ((l[i] + l[j]) % k == 0)
                    count++;
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

        int k = Convert.ToInt32(firstMultipleInput[1]);

        List<int> ar = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arTemp => Convert.ToInt32(arTemp)).ToList();

        int result = Result.divisibleSumPairs(n, k, ar);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
