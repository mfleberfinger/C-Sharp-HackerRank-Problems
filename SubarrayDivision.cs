/*
Two children, Lily and Ron, want to share a chocolate bar. Each of the squares has an integer on it.

Lily decides to share a contiguous segment of the bar selected such that:

    The length of the segment matches Ron's birth month, and,
    The sum of the integers on the squares is equal to his birth day.

Determine how many ways she can divide the chocolate.

Example


Lily wants to find segments summing to Ron's birth day, with a length equalling his birth month, . In this case, there are two segments meeting her criteria: and

.

Function Description

Complete the birthday function in the editor below.

birthday has the following parameter(s):

    int s[n]: the numbers on each of the squares of chocolate
    int d: Ron's birth day
    int m: Ron's birth month

Returns

    int: the number of ways the bar can be divided

Input Format

The first line contains an integer
, the number of squares in the chocolate bar.
The second line contains space-separated integers , the numbers on the chocolate squares where .
The third line contains two space-separated integers, and

, Ron's birth day and his birth month.

Constraints

, where (
)

Sample Input 0

5
1 2 1 3 2
3 2

Sample Output 0

2

Explanation 0

Lily wants to give Ron
squares summing to

. The following two segments meet the criteria:

image

Sample Input 1

6
1 1 1 1 1 1
3 2

Sample Output 1

0

Explanation 1

Lily only wants to give Ron
consecutive squares of chocolate whose integers sum to

. There are no possible pieces satisfying these constraints:

image

Thus, we print

as our answer.

Sample Input 2

1
4
4 1

Sample Output 2

1

Explanation 2

Lily only wants to give Ron
square of chocolate with an integer value of . Because the only square of chocolate in the bar satisfies this constraint, we print as our answer.
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
     * Complete the 'birthday' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER_ARRAY s
     *  2. INTEGER d
     *  3. INTEGER m
     */

    // Finds all contiguous runs of m numbers in List s that sum to d.
    // Returns a count of those runs.
    public static int birthday(List<int> s, int d, int m)
    {
        int count = 0;
        
        // The problem statement doesn't say what to do if m > n. However, if m > n, then
        // there can be no segment of length m, meaning our answer must be 0.
        // Otherwise, test whether each m element window in the list sums to d.
        if (m <= s.Count) {
            int sum = 0;
            int start = 0;

            // Get the sum of the first window.
            for (int i = 0; i <= m - 1; i++)
                sum += s[i];
            
            if (sum == d)
                count++;
            
            // Check each window's sum and count the windows that sum to d.
            for (int end = m; end < s.Count; end++) {
                // Move the window right.
                sum -= s[start];
                sum += s[end];
                start++;
                
                if (sum == d)
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

        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> s = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(sTemp => Convert.ToInt32(sTemp)).ToList();

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int d = Convert.ToInt32(firstMultipleInput[0]);

        int m = Convert.ToInt32(firstMultipleInput[1]);

        int result = Result.birthday(s, d, m);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
