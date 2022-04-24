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
     * Complete the 'encryption' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts STRING s as parameter.
     */

    public static string encryption(string s)
    {
        string noSpaces = s.Replace(" ", "");
        int minDim = (int)Math.Floor(Math.Sqrt(noSpaces.Length));
        int maxDim = (int)Math.Ceiling(Math.Sqrt(noSpaces.Length));
        int rows, cols;
        StringBuilder encoded = new StringBuilder();
        // We cannot have more rows than columns.
        // We can have more columns than rows.
        // rows * columns must be at least equal to noSpaces.Length.
        // If multiple grids satisfy the above constraints, choose the one
        // with the smallest area.
        
        // Given our minimum and maximum dimensions, we only have three possible
        // grids.
        // Try the grids in order from smallest to largest.
        rows = cols = minDim;
        if (rows * cols < noSpaces.Length)
            cols = maxDim;
        if (rows * cols < noSpaces.Length)
            rows = maxDim;
        
        for (int i = 0; i < cols; i++) // ith column
        {
            for (int j = 0; j < rows && i + (j * cols) < noSpaces.Length; j++) // jth row
            {
                encoded.Append(noSpaces[i + (j * cols)]);
            }
            encoded.Append(" ");
        }
        // Remove the dangling space at the end.
        encoded.Remove(encoded.Length - 1, 1);
        
        return encoded.ToString();
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string s = Console.ReadLine();

        string result = Result.encryption(s);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
