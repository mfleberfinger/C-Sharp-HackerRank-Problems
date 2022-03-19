/*
You are choreographing a circus show with various animals. For one act, you are given two kangaroos on a number line ready to jump in the positive direction (i.e, toward positive infinity).

    The first kangaroo starts at location 

and moves at a rate of
meters per jump.
The second kangaroo starts at location
and moves at a rate of

    meters per jump.

You have to figure out a way to get both kangaroos at the same location at the same time as part of the show. If it is possible, return YES, otherwise return NO.

Example



After one jump, they are both at , (,

), so the answer is YES.

Function Description

Complete the function kangaroo in the editor below.

kangaroo has the following parameter(s):

    int x1, int v1: starting position and jump distance for kangaroo 1
    int x2, int v2: starting position and jump distance for kangaroo 2

Returns

    string: either YES or NO

Input Format

A single line of four space-separated integers denoting the respective values of
, , , and

.

Constraints

Sample Input 0

0 3 4 2

Sample Output 0

YES

Explanation 0

The two kangaroos jump through the following sequence of locations:

image

From the image, it is clear that the kangaroos meet at the same location (number
on the number line) after same number of jumps (

jumps), and we print YES.

Sample Input 1

0 2 5 3

Sample Output 1

NO

Explanation 1

The second kangaroo has a starting location that is ahead (further to the right) of the first kangaroo's starting location (i.e.,
). Because the second kangaroo moves at a faster rate (meaning ) and is already ahead of the first kangaroo, the first kangaroo will never be able to catch up. Thus, we print NO. 
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
     * Complete the 'kangaroo' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts following parameters:
     *  1. INTEGER x1
     *  2. INTEGER v1
     *  3. INTEGER x2
     *  4. INTEGER v2
     */

    public static string kangaroo(int x1, int v1, int x2, int v2)
    {
        // We want to solve for the number of jumps, j, that would put the
        // kangaroos at the same location.
        // If j is a finite, non-negative integer, it is possible to make the
        // kangaroos reach the same location at the same time.
        // We want jv1 + x1 = jv2 + x2
        // There are two solutions to this but because x1 < x2:
        // Solving for j... j = (x2 - x1) / (v1 - v2)
        bool possible = true;
        double j = -1;

        // If the kangaroos start in the same place, we're done.
        // This never happens. The constraints say x1 is less than x2.
        //if (x1 == x2)
        //    possible = true;
        
        // Check for division by 0: the infinite kanagaroo.
        if (v1 - v2 == 0)
            possible = false;
        else {
            j = ((double)(x2 - x1)) / ((double)(v1 - v2));
            // Find out if j is an integer.
            // If j is negative, kangaroo 1 never catches up to kangaroo 2.
            possible = j >= 0 && Math.Truncate(j) == j;
        }
        
        return possible ? "YES" : "NO";
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int x1 = Convert.ToInt32(firstMultipleInput[0]);

        int v1 = Convert.ToInt32(firstMultipleInput[1]);

        int x2 = Convert.ToInt32(firstMultipleInput[2]);

        int v2 = Convert.ToInt32(firstMultipleInput[3]);

        string result = Result.kangaroo(x1, v1, x2, v2);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
