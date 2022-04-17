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
     * Complete the 'appendAndDelete' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts following parameters:
     *  1. STRING s
     *  2. STRING t
     *  3. INTEGER k
     */

/*
    // This is edit distance (i.e. dynamic programming).
    // We need to use dynamic programming to determine the cost of
    // changing s to t. Knowing the cost, we can determine whether it is possible
    // to complete the edit in k moves.
    //
    // Is this right? Dynamic programming will give us the
    // optimal number of edits to make each possible prefix of s and t match
    // but what if there's a nonoptimal path of exactly k from s to t?
    // For example, if we can delete everything from s to make an empty string
    // and append all the characters from t with fewer than k moves, then we can
    // get the total to k by performing an arbitrary number of deletes on the empty
    // string before doing our appends.
    //
    // If we can delete and re-add everything in fewer than k moves, we can always
    // get to k.
    // 
    // If we can turn s into t with k - 2n moves (where n is some integer), we can
    // turn s into t, then perform n deletes followed by n appends to get back to t.
    //
    // If we can turn s into t with k - 2n - 1 moves (where n is some integer), we can
    // turn s into the t.Length character prefix of t, then do n deletes followed by
    // n appends to get to t.
    //
    // Therefore, we can always get from s to t in k moves if we can get there in
    // fewer than k moves.
    //
    // Trivially, if we can't get from s to t in k or fewer moves, then we can't
    // get from s to t in k moves.
    //
    // Therefore, we can use dynamic programming to find the optimal number of moves
    // and, if that number of moves is less than or equal to k, we can get from s to t
    // in exactly k moves.
    //
    // s = s[1] + s[2] + ... + s[m]
    // t = t[1] + t[2] + ... + t[n]
    public static string appendAndDelete(string s, string t, int k)
    {
        int[,] table = new int[s.Length + 1, t.Length + 1];
                
        // Base cases.
        // The costs of going from an i character prefix of s to t[0] (an empty string)
        for (int i = 0; i <= s.Length; i++)
            table[i, 0] = i;
        // The costs of going from an empty string to a j character prefix of t.
        // [0, 0] overlaps with the other loop but this is okay because [0, 0] is always 0.
        for (int j = 0; j <= t.Length; j++)
            table[0, j]= j;
        
        // Now that we have the base cases, build the table.
        int delCost, insCost;
        for (int i = 1; i <= s.Length; i++)
        {
            for (int j = 1; j <= t.Length; j++)
            {
                // If we are deleting from the length i prefix of s to make it
                // match the length j prefix of t, then j = i - 1 and we
                // must have already performed operations to make the length i - 1
                // prefix of s match the length j prefix of t. So delCost is the
                // cost of making the length i - 1 prefix of s match the length j
                // prefix of t, plus the cost of this deletion.
                delCost = table[i - 1, j] + 1;
                // If we are inserting into a length i prefix of s to make it a length
                // i + 1 prefix matching the length j prefix of t, then i = j - 1 and we
                // already performed the operation to make the length i prefix of s
                // match the length j - 1 prefix of t. So insCost is the cost of making
                // the length i prefix of s match the length j - 1 prefix of t.
                insCost = table[i, j - 1] + 1;
                
                // If s[i] and s[j] are the same, then we don't need to perform any
                // operation to make them match. Making the length i prefix match
                // the length j prefix has the same cost as matching the length i - 1
                // and length j - 1 prefixes.
                if (s[i - 1] == t[j - 1])
                    table[i, j] = table[i - 1, j - 1];
                else if (delCost < insCost)
                    table[i, j] = delCost;
                else
                    table[i, j] = insCost;
            }
        }
        
        // If we can turn s into t in k moves or fewer, we can turn s into t in
        // exactly k moves.
        return table[s.Length, t.Length] <= k ? "Yes" : "No";
    }
*/

    // This doesn't require dynamic programming at all.
    // The problem is more constrained (we can only delete or insert at the end
    // and cannot perform substitutions) than the general edit distance problem,
    // which breaks the algorithm above because it allows deletion or insertion at
    // any index.
    // To determine the number of moves we need to make s match t (if s and t have
    // the same length), we just need to find the first letter of s that does not
    // match the corresponding letter of t and multiply the length of the suffix
    // starting with that letter by 2 to represent deletion and reinsertion of that
    // suffix.
    public static string appendAndDelete(string s, string t, int k)
    {
        int cost = 0;
        int i = 0;
        string yesNo;
        if (s.Length > t.Length)
        {
            // cost = (deletions to make lengths equal) + 2 * (suffix length after deletions)
            cost = s.Length - t.Length;
            while (i < t.Length && s[i] == t[i])
                i++;
            cost += 2 * (t.Length - i);
        }
        else if (s.Length < t.Length)
        {
            // cost = 2 * (suffix length before appends) + (appends to make length equal)
            cost = t.Length - s.Length;
            while (i < s.Length && s[i] == t[i])
                i++;
            cost += 2 * (s.Length - i);
        }
        else
        {
            // cost = 2 * (suffix length)
            while (i < s.Length && s[i] == t[i])
                i++;
            cost += 2 * (s.Length - i);
        }
        
        // If k - cost is odd and k is not large enough to delete the entire
        // string and perform an arbitrary number of deletions on the empty string
        // and append all characters of t, then we can't transform s to t in exactly
        // k moves.
        if (cost < k && (k - cost) % 2 != 0 && k < (s.Length + t.Length))
            yesNo = "No";
        else
            yesNo = cost <= k ? "Yes" : "No";        
        return yesNo;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string s = Console.ReadLine();

        string t = Console.ReadLine();

        int k = Convert.ToInt32(Console.ReadLine().Trim());

        string result = Result.appendAndDelete(s, t, k);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
