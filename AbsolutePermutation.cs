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
     * Complete the 'absolutePermutation' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. INTEGER k
     */

    // Given k, only 2 numbers are possible for each index i:
    //  pos[i] must either equal (i + k) or (i - k).
    // If we assume that we have 2 possibilities for each index, regardless of
    //  our previous choices, testing every allowed permutation would take 2^n
    //  iterations. However, each choice constrains the choices we can make for
    //  following indices.
    // There may be a dynamic programming or pruned backtracking solution to this.
    // Dynamic programming is probably not possible here (or, at least, the table
    //  would be more than two dimensions and the runtime would be too long).
    //
    // Possible solution (based on a hint in the discussion):
    //  Alternate between k additions and k subtractions. E.g. n = 100 k = 2
    //  pos[i] =    {3 4 1 2 7 8 5 6 11 12 9 10...}
    //  i:           1 2 3 4 5 6 7 8 9  10 11 12
    //  operation:   + + - - + + - - +  +  -   -
    public static List<int> absolutePermutation(int n, int k)
    {
        List<int> pos;
        bool possible;
       
       // Start with subtraction. This would give us the smallest numbers in the
       //   highest places.
       possible = BuildPermutation(n, k, false, out pos);
        
        // If starting with subtraction was not possible, try starting with addition.
        if (!possible)
            possible = BuildPermutation(n, k, true, out pos);
        
        if (!possible)
            pos = new List<int>() { -1 };
        
        return pos;
    }
    
    private static bool BuildPermutation(int n, int k, bool addFirst, out List<int> pos)
    {
        pos = new List<int>();
        bool possible = true;
        
        // Special case: If k == 0, just set all (one-indexed) pos[i] to i.
        if (k == 0)
        {
            for (int i = 1; i <= n; i++)
                pos.Add(i);
        }
        else
        {
            // Initialize a list of used numbers. The problem wants us to use
            // 1-indexing, so we'll just ignore index 0 to make this easier.
            List<bool> used = new List<bool>();
            for (int j = 0; j <= n; j++)
                used.Add(false);
            bool plus = addFirst;
            int i = 1;
            int next;
            while (i <= n && possible)
            {
                if (plus)
                    next = i + k;
                else
                    next = i - k;

                if (next > n || next < 1 || used[next])
                {
                    possible = false;
                }
                else
                {
                    pos.Add(next);
                    used[next] = true;
                }
                
                if (i % k == 0)
                    plus = !plus;
                i++;
            }
        }
        return possible;
    }

/*
    // Incorrect:
    // Add k until the (k + 1)th index (by 1-indexing).
    // Starting at 1-index k + 1, subtract k.
    // If at any point, this can't be done, consider this problem instance
    //  impossible.
    public static List<int> absolutePermutation(int n, int k)
    {
        List<int> pos = new List<int>();
        // Initialize a list of used numbers. The problem wants us to use
        // 1-indexing, so we'll just ignore index 0 to make this easier.
        List<bool> used = new List<bool>();
        for (int j = 0; j <= n; j++)
            used.Add(false);
        
        int i = 1;
        bool possible = true;
        while (i <= k && possible)
        {
            if (i + k > n || used[i + k])
            {
                possible = false;
            }
            else
            {
                pos.Add(i + k);
                used[i + k] = true;
            }
            i++;
        }
        while (i <= n && possible)
        {
            if (i - k < 1 || used[i - k])
            {
                possible = false;
            }
            else
            {
                pos.Add(i - k);
                used[i - k] = true;
            }
            i++;
        }
        
        if (!possible)
            pos = new List<int>() { -1 };
        
        return pos;
    } */

/*
    // Recursive solution:
    //  This will be too inefficient to handle anywhere near 10,000 values but
    //  should be useful to help understand the problem.
    // REMEMBER THAT HACKERRANK OUTPUTS "Wrong Answer" WHEN A STACK OVERFLOW OCCURS,
    //  INSTEAD OF REPORTING THE RUNTIME ERROR.
    private static List<bool> used = new List<bool>();
    public static List<int> absolutePermutation(int n, int k)
    {
        used = new List<bool>();
        // Initialize the list of used numbers. The problem wants us to use
        // 1-indexing, so we'll just ignore index 0 to make this easier.
        for (int i = 0; i <= n; i++)
            used.Add(false);
        return absolutePermutationRecursive(n, n, k);
    }
    private static List<int> absolutePermutationRecursive(int n0, int n, int k, int i = 1)
    {        
        List<int> pos = new List<int>();
        List<int> posPlus;
        List<int> posMinus;

        // Get the lowest cost absolute permutation attainable by adding or
        //  subtracting k at the current position. Compare the two and return
        //  the cheapest one.
        
        // Base case: n = 1
        if (n == 1)
        {
            if (i - k >= 1 && !used[i - k])
                pos.Add(i - k);
            else if (i + k <= n0 && !used[i + k])
                pos.Add(i + k);
            else
                pos.Add(-1);
        }
        else
        {
            // Plus k
            if (i + k <= n0 && !used[i + k])
            {
                used[i + k] = true;
                posPlus = absolutePermutationRecursive(n0, n - 1, k, i + 1);
                used[i + k] = false;
            }
            else
            {
                posPlus = new List<int>() { -1 };
            }
            // Minus k
            if (i - k >= 1 && !used[i - k])
            {
                used[i - k] = true;
                posMinus = absolutePermutationRecursive(n0, n - 1, k, i + 1);
                used[i - k] = false;
            }
            else
            {
                posMinus = new List<int>() { -1 };
            }
            
            // Both paths are possible.
            // Choose the permutation that is lexicographically smaller.
            if (posPlus[0] != -1 && posMinus[0] != -1)
            {
                int j = 0;
                // posPlus and posMinus should be length n - 1 and we want to
                // stop indexing on the last element, not after it.
                while (j < n - 2 && posPlus[j] == posMinus[j])
                    j++;

                // If posPlus is smaller use i + k.
                if (posPlus[j] < posMinus[j])
                {
                    pos.Add(i + k);
                    foreach(int l in posPlus)
                        pos.Add(l);
                }
                // If posMinus is smaller, or the two are equal, use i - k.
                // It is important to use i - k if the two subresults are equal
                //  because, obviously, i - k is smaller (costs less) than i + k.
                else
                {
                    pos.Add(i - k);
                    foreach(int l in posMinus)
                        pos.Add(l);
                }
            }
            // Only the positive path is possible.
            else if (posPlus[0] != -1)
            {
                pos.Add(i + k);
                foreach(int j in posPlus)
                    pos.Add(j);
            }
            // Only the negative path is possible.
            else if (posMinus[0] != -1)
            {
                pos.Add(i - k);
                foreach(int j in posMinus)
                    pos.Add(j);
            }
            // Positive and negative are both impossible (there is no solution).
            else
            {
                pos = new List<int>() { -1 };
            }
        }
        
        return pos;
    }*/
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int t = Convert.ToInt32(Console.ReadLine().Trim());

        for (int tItr = 0; tItr < t; tItr++)
        {
            string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

            int n = Convert.ToInt32(firstMultipleInput[0]);

            int k = Convert.ToInt32(firstMultipleInput[1]);

            List<int> result = Result.absolutePermutation(n, k);

            textWriter.WriteLine(String.Join(" ", result));
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
