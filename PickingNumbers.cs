/*
Given an array of integers, find the longest subarray where the absolute difference between any two elements is less than or equal to

.

Example

There are two subarrays meeting the criterion: and . The maximum length subarray has

elements.

Function Description

Complete the pickingNumbers function in the editor below.

pickingNumbers has the following parameter(s):

    int a[n]: an array of integers

Returns

    int: the length of the longest subarray that meets the criterion

Input Format

The first line contains a single integer
, the size of the array .
The second line contains space-separated integers, each an

.

Constraints

The answer will be

    .

Sample Input 0

6
4 6 5 3 3 1

Sample Output 0

3

Explanation 0

We choose the following multiset of integers from the array:
. Each pair in the multiset has an absolute difference (i.e., and ), so we print the number of chosen integers,

, as our answer.

Sample Input 1

6
1 2 2 3 1 2

Sample Output 1

5

Explanation 1

We choose the following multiset of integers from the array:
. Each pair in the multiset has an absolute difference (i.e., , , and ), so we print the number of chosen integers, , as our answer.
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
     * Complete the 'pickingNumbers' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts INTEGER_ARRAY a as parameter.
     */

    public static int pickingNumbers(List<int> a)
    {
        int longest = 0;
        int current = 0;
        int vL = a[0];
        int vR = a[0];
        int run = 0;
        
        a.Sort();
        
        for (int i = 0; i < a.Count(); i++) {
            // Are we in the same subarray?
            if (Math.Abs(a[i] - vL) <= 1 && Math.Abs(a[i] - vR) <= 1) {
                current++;
                // Are we in a run?
                if (a[i] == vR) {
                    run++;
                }
                else {
                    run = 1;
                    vL = vR;
                    vR = a[i];
                }
            }
            else {
                if (current > longest)
                    longest = current;
                // Is the previous value in the same subarray as the current value?
                if (Math.Abs(a[i] - vR) <= 1) {
                    vL = vR;
                    current = run + 1;
                }
                else {
                    vL = a[i];
                    current = 1;
                }
                vR = a[i];
                run = 1;
            }
        }
        
        if (current > longest)
            longest = current;
        
        return longest;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> a = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(aTemp => Convert.ToInt32(aTemp)).ToList();

        int result = Result.pickingNumbers(a);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
