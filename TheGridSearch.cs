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
     * Complete the 'gridSearch' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts following parameters:
     *  1. STRING_ARRAY G
     *  2. STRING_ARRAY P
     */
/*
    public static string gridSearch(List<string> G, List<string> P)
    {
        // Simplifying assumption (may be wrong): If all rows of the pattern
        // appear in the grid, they will not be horizontally shifted with
        // respect to one another. E.g. if the pattern is ["12", "34", "56"],
        // the grid will not be something like ["1200", "0340", "0056"].
        // Given this assumption, we can just check that each consecutive piece
        // of the pattern appears in consecutive rows.
        //
        // That was a bad assumption. The test cases include the following input:
        //        1
        //        4 6
        //        123456
        //        567890
        //        234567
        //        194729
        //        4 4
        //        1234
        //        5678
        //        2345
        //        4729
        int matched = 0;
        int i = 0;
        while (matched < P.Count && i < G.Count)
        {
            matched = 0;
            while (matched < P.Count && matched > -1)
            {
                if (G[i + matched].Contains(P[matched]))
                    matched++;
                else
                    matched = -1;
            }
            i++;
        }
        
        return matched == P.Count ? "YES" : "NO";
    }
*/

    // Report all starting indices of substr in str.
    // Returns an empty list if substr is not found in str.
    private static List<int> IndicesOf(string str, string substr)
    {
        List<int> indices = new List<int>();
        int index = str.IndexOf(substr, 0);
        
        while(index > -1 && index < str.Length - 1)
        {
            indices.Add(index);
            index = str.IndexOf(substr, index + 1);
        }
        
        return indices;
    }

    public static string gridSearch(List<string> G, List<string> P)
    {
        int matched = 0;
        HashSet<int> startingColumns;
        int i = 0;
        while (matched < P.Count && i < G.Count)
        {
            matched = 0;
            startingColumns = new HashSet<int>();
            while (matched < P.Count && matched > -1)
            {
                // Like the previous attempt but keep track of all of the columns in which the
                // previous row of the pattern begins. Only consider a match on this
                // row valid if it begins on at least one of the columns in which a match
                // in the previous row occurred. Before going to the next row, forget all
                // of the column numbers that are not shared between the current and previous
                // row as possibilities.
                // This process of making sure all rows match each of the previous rows is
                // equivalent to set intersection, so we use HashSet's set operations.
                if (startingColumns.Count > 0)
                    startingColumns.IntersectWith(IndicesOf(G[i + matched], P[matched]));
                else
                    startingColumns.UnionWith(IndicesOf(G[i + matched], P[matched]));
                
                if (startingColumns.Count > 0)
                    matched++;
                else
                    matched = -1;
            }
            i++;
        }
        
        return matched == P.Count ? "YES" : "NO";
    }
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

            int R = Convert.ToInt32(firstMultipleInput[0]);

            int C = Convert.ToInt32(firstMultipleInput[1]);

            List<string> G = new List<string>();

            for (int i = 0; i < R; i++)
            {
                string GItem = Console.ReadLine();
                G.Add(GItem);
            }

            string[] secondMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

            int r = Convert.ToInt32(secondMultipleInput[0]);

            int c = Convert.ToInt32(secondMultipleInput[1]);

            List<string> P = new List<string>();

            for (int i = 0; i < r; i++)
            {
                string PItem = Console.ReadLine();
                P.Add(PItem);
            }

            string result = Result.gridSearch(G, P);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
