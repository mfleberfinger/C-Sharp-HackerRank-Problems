/*
There will be two arrays of integers. Determine all integers that satisfy the following two conditions:

    The elements of the first array are all factors of the integer being considered
    The integer being considered is a factor of all elements of the second array

These numbers are referred to as being between the two arrays. Determine how many such numbers exist.

Example

There are two numbers between the arrays: and .
, , and for the first value.
, and , for the second value. Return

.

Function Description

Complete the getTotalX function in the editor below. It should return the number of integers that are betwen the sets.

getTotalX has the following parameter(s):

    int a[n]: an array of integers
    int b[m]: an array of integers

Returns

    int: the number of integers that are between the sets

Input Format

The first line contains two space-separated integers,
and , the number of elements in arrays and .
The second line contains distinct space-separated integers where .
The third line contains distinct space-separated integers where

.

Constraints

Sample Input

2 3
2 4
16 32 96

Sample Output

3

Explanation

2 and 4 divide evenly into 4, 8, 12 and 16.
4, 8 and 16 divide evenly into 16, 32, 96.

4, 8 and 16 are the only three numbers for which each element of a is a factor and each is a factor of all elements of b.
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
     * Complete the 'getTotalX' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER_ARRAY a
     *  2. INTEGER_ARRAY b
     */

    public static int getTotalX(List<int> a, List<int> b)
    {
        // Number of integers between the arrays.
        int count = 0;
        // Smallest and largest values we need to check.
        int min = int.MinValue;
        int max = int.MaxValue;
        
        // The minimum we need to check is the largest value in a.
        foreach (int i in a) {
            if (i > min)
                min = i;
        }
        // The maximum we need to check is the smallest value in b.
        foreach (int i in b) {
            if (i < max)
                max = i;
        }
        
        bool isBetween;
        for (int i = min; i <= max; i++) {
            isBetween = true;
            // Each value in a must be a factor of i.
            foreach (int j in a)
                isBetween = isBetween && i % j == 0;

            if (isBetween) {
                // i must be a factor of each value in b.
                foreach (int j in b)
                    isBetween = isBetween && j % i == 0;
            }
            
            if (isBetween)
                count++;
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

        int m = Convert.ToInt32(firstMultipleInput[1]);

        List<int> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();

        List<int> brr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(brrTemp => Convert.ToInt32(brrTemp)).ToList();

        int total = Result.getTotalX(arr, brr);

        textWriter.WriteLine(total);

        textWriter.Flush();
        textWriter.Close();
    }
}
