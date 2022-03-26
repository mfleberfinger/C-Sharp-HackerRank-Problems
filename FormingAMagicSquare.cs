/*
We define a magic square to be an matrix of distinct positive integers from to where the sum of any row, column, or diagonal of length

is always equal to the same number: the magic constant.

You will be given a
matrix of integers in the inclusive range . We can convert any digit to any other digit in the range at cost of . Given

, convert it into a magic square at minimal cost. Print this cost on a new line.

Note: The resulting magic square must contain distinct integers in the inclusive range

.

Example

$s = [[5, 3, 4], [1, 5, 8], [6, 4, 2]]

The matrix looks like this:

5 3 4
1 5 8
6 4 2

We can convert it to the following magic square:

8 3 4
1 5 9
6 7 2

This took three replacements at a cost of

.

Function Description

Complete the formingMagicSquare function in the editor below.

formingMagicSquare has the following parameter(s):

    int s[3][3]: a 

    array of integers

Returns

    int: the minimal total cost of converting the input square to a magic square

Input Format

Each of the
lines contains three space-separated integers of row

.

Constraints

Sample Input 0

4 9 2
3 5 7
8 1 5

Sample Output 0

1

Explanation 0

If we change the bottom right value,
, from to at a cost of ,

becomes a magic square at the minimum possible cost.

Sample Input 1

4 8 2
4 5 7
6 1 6

Sample Output 1

4

Explanation 1

Using 0-based indexing, if we make

-> at a cost of -> at a cost of -> at a cost of

    ,

then the total cost will be
. 
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

    // Determine whether a given 2d array is "magic."
    // Don't verify that all of the values are distinct.
    // Assume that the caller will not insert duplicate numbers.
    private static bool isMagic(List<List<int>> a) {
        // We'll say that the outer list contains the rows.
        int sum;
        int previousSum;
        
        bool rowsMatch = true;
        previousSum = 0;
        for (int j = 0; j < a[0].Count; j++) {
            previousSum += a[0][j];
        }
        for (int i = 1; i < a.Count; i++) {
            sum = 0;
            for (int j = 0; j < a[i].Count; j++) {
                sum += a[i][j];
            }
            rowsMatch = rowsMatch && sum == previousSum;
            previousSum = sum;
        }

        bool colsMatch = true;
        //previousSum = 0; // Cols need to match rows
        sum = 0;
        for (int j = 0; j < a.Count; j++) {
            sum += a[j][0];
        }
        colsMatch = sum == previousSum;
        for (int i = 1; i < a.Count; i++) {
            sum = 0;
            for (int j = 0; j < a[i].Count; j++) {
                sum += a[j][i];
            }
            colsMatch = colsMatch && sum == previousSum;
            previousSum = sum;
        }
        
        bool diagsMatch = true;
        //previousSum = 0; // diags need to match rows and cols
        int sum1 = 0;
        int sum2 = 0;
        for (int i = 0; i < a.Count; i++)
            sum1 += a[i][i];
        int jay = a.Count - 1;
        for (int i = 0; i < a.Count; i++) {
                sum2 += a[i][jay];
                jay--;
        }
        diagsMatch = sum1 == sum2 && sum2 == previousSum;
        
        return rowsMatch && colsMatch && diagsMatch;
    }
    
    // Get the cost of converting matrix s to matrix a.
    // We can convert any digit d1 to any other digit d2 at cost of abs(d1 - d2). 
    // It is assumed that s and a have the same dimensions.
    private static int getCost(List<List<int>> s, List<List<int>> a) {
        int cost = 0;
        for (int i = 0; i < s.Count; i++)
            for (int j = 0; j < s[i].Count; j++) 
                cost += Math.Abs(s[i][j] - a[i][j]);
        return cost;
    }

    private static void swap(List<int> l, int i, int j) {
        int temp = l[i];
        l[i] = l[j];
        l[j] = temp;
    }

    // s: 3 x 3 matrix used to evaluate cost.
    // a: List of ints 1 through 9 to permute. Should be initialized in the first call.
    // l: Starting index. Should be 0 in the first call.
    // r: Ending index. Should be a.Count - 1 in the first call.
    private static int backtrackMagic(List<List<int>> s, List<int> a, int l, int r) {
        int cheapest = int.MaxValue;
        int cheapestInSubset = int.MaxValue;
        
        if (l == r) {
            // Base case. Calculate the cost.
            List<List<int>> matrix = new List<List<int>>() {
                new List<int>() {a[0], a[1], a[2]},
                new List<int>() {a[3], a[4], a[5]},
                new List<int>() {a[6], a[7], a[8]}
            };
            // If this is not a magic square, the cost is "infinite" (int.MaxValue);
            if (isMagic(matrix))
                cheapest = getCost(s, matrix);
        }
        else {
            for (int i = l; i <= r; i++) {
                // Choose the first element for the following permutations.
                swap(a, l, i);
                // Recursively determine the lowest cost of possible permutations,
                // given the first element we just chose.
                cheapestInSubset = backtrackMagic(s, a, l + 1, r);
                if (cheapestInSubset < cheapest)
                    cheapest = cheapestInSubset;
                // Put the elements back in place so we can get the next set
                // of permutations in the next iteration.
                // This is the "backtracking" for which this algorithm is named.
                swap(a, l, i);
            }
        }
        
        return cheapest;
    }

    /*
     * Complete the 'formingMagicSquare' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts 2D_INTEGER_ARRAY s as parameter.
     */

    public static int formingMagicSquare(List<List<int>> s)
    {
        // There are "only" 9! = 362,880 matrices that fit the description given
        // in this problem.
        // A brute force solution is probably viable.
        // If there was an efficient solution, it seems likely that the problem would
        // include larger inputs.
        // Solve this by iterating through every permitted matrix, and for those that are
        // "magic," calculate their cost. Keep track of the smallest cost.
        // A possible optmization is to precompute all 3 x 3 magic squares and load them
        // from a file (probably not doable on HackerRank) or hardcode them if there are
        // few enough.
               
        // Generate all possible matrices. Remember, all integers in a magic
        // square must be distinct (e.g. a 3x3 matrix with only 1s is not magic)
        // and isMagic() doesn't check for duplicate values.
        
        
        
        // Need to find all permutations of the integers 1 through 9... This is
        // solved by backtracking.
        return backtrackMagic(s, new List<int> {1,2,3,4,5,6,7,8,9}, 0, 8);
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        List<List<int>> s = new List<List<int>>();

        for (int i = 0; i < 3; i++)
        {
            s.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(sTemp => Convert.ToInt32(sTemp)).ToList());
        }

        int result = Result.formingMagicSquare(s);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
