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
     * Complete the 'biggerIsGreater' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts STRING w as parameter.
     */

    public static string biggerIsGreater(string w)
    {
        // This problem actually has a name: "next lexicographical permutation"
        // Algorithm:
        //  Identify the longest non-increasing suffix (the longest substring
        //      whose characters are in descending order).
        //  If this is the entire string, then w is already the last permutation
        //      and there is no answer.
        //  Otherwise:
        //      We cannot increase the suffix any more because it is the last permutation
        //          of the characters it contains.
        //      Call the element immediately left of the suffix the "pivot."
        //      Find the smallest element in the suffix that is greater than
        //          the pivot (if there is more than one such element, choose the
        //          rightmost one) and swap it with the pivot. This minimally increases
        //          the size of the prefix.
        //      Sort the suffix in ascending order, turning it into the least/first
        //          permutation of its elements (because it is already in descending
        //          order and we placed the pivot to maintain descending order, we can
        //          just reverse the order of the suffix's characters).
        //      We have now found the next lexicographical permutation.
        
        char[] chars = w.ToCharArray();
        int startOfSuffix = chars.Length - 1;
        int i = chars.Length - 2;
        bool found = false;
        
        while (i >= 0 && !found)
        {
            if (chars[i] < chars[startOfSuffix])
                found = true;
            else
                startOfSuffix = i;
            i--;
        }
        
        string result = "no answer";
        int pivot;
        char tmp;
        if (found)
        {
            pivot = startOfSuffix - 1;
            i = startOfSuffix;
            while (i + 1 < chars.Length && chars[i + 1] > chars[pivot])
                i++;
            tmp = chars[pivot];
            chars[pivot] = chars[i];
            chars[i] = tmp;
            // Reverse the suffix.
            i = startOfSuffix;
            int j = chars.Length - 1;
            for (; i < j; i++, j--)
            {
                tmp = chars[i];
                chars[i] = chars[j];
                chars[j] = tmp;
            }
            result = new string(chars);
        }
        
        return result;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int T = Convert.ToInt32(Console.ReadLine().Trim());

        for (int TItr = 0; TItr < T; TItr++)
        {
            string w = Console.ReadLine();

            string result = Result.biggerIsGreater(w);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
