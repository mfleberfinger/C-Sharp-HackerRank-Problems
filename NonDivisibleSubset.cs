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
     * Complete the 'nonDivisibleSubset' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER k
     *  2. INTEGER_ARRAY s
     */

    public static int nonDivisibleSubset(int k, List<int> s)
    {
        // From the discussion section, the trick to doing this efficiently is
        // knowing that
        // A + B is divisible by k iff (A % k) + (B % k) = k or (A % k) + (B % k) = 0.
        // 
        // Iterate through s, calculating the remainders. As each remainder is calculated,
        // store it in a dictionary of (remainder, count). If it already is present, increment
        // its count. Iterate from 0 to k div 2 and, for each value, r, decide whether to include
        // numbers with that remainder or with k - r as their remainder (whichever has the higher
        // count). Add that count to the total. If k = 2 * (k div 2), and at least one value with
        // remainder k div 2 exists, only add one to the count for that value.
        // Do the same for remainder 0 (only add one if count greater than 0).
        
        Dictionary<int, int> dict = new Dictionary<int, int>();
        int count = 0;
        
        foreach (int i in s)
        {
            if (dict.ContainsKey(i % k))
                dict[i % k]++;
            else
                dict.Add(i % k, 1);
        }
        
        int halfK = k / 2;
        // Check the special cases first so we don't need to keep checking
        // for them in the loop.
        if (dict.ContainsKey(0))
            count++;
        if (halfK * 2 == k && dict.ContainsKey(halfK))
        {
            count++;
            halfK--;
        }
        for (int i = 1; i <= halfK; i++)
        {
            if (dict.ContainsKey(i) && dict.ContainsKey(k - i))
            {
                if (dict[i] >= dict[k - i])
                    count += dict[i];
                else
                    count += dict[k - i];
            }
            else if (dict.ContainsKey(i))
                count += dict[i];
            else if (dict.ContainsKey(k - i))
                count += dict[k - i];
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

        List<int> s = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(sTemp => Convert.ToInt32(sTemp)).ToList();

        int result = Result.nonDivisibleSubset(k, s);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
