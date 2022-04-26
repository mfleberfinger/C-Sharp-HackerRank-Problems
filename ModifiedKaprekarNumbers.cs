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
    // x^y where x and y are non-negative integers
    public static long exp(long x, long y)
    {
        long result = x;
        if (y == 0)
            result = 1;
        else for (long i = 1; i < y; i++)
            result *= x;
        return result;
    }
    
    public static (long left, long right) splitInt(long toSplit)
    {
        // Either 2 * d or (2 * d) - 1
        long length = 1;
        long d;
        // from instructions: rightLength = d
        long rightLength;
        // from instructions: leftLength = length - rightLength
        long leftLength;
        for (; toSplit / exp(10, length) > 0; length++);
        if (length % 2 != 0)
            rightLength = d = (length + 1) / 2;
        else
            rightLength = d = length / 2;
        
        leftLength = length - rightLength;
        
        long left = 0;
        long right = 0;
        left = toSplit / exp(10, rightLength);
        right = toSplit - (left * exp(10, rightLength));
        
        return (left, right);
    }

    /*
     * Complete the 'kaprekarNumbers' function below.
     *
     * The function accepts following parameters:
     *  1. INTEGER p
     *  2. INTEGER q
     */

    public static void kaprekarNumbers(int p, int q)
    {
        StringBuilder sb = new StringBuilder();
        (long left, long right) split;
        
        for (long i = p; i <= q; i++)
        {
            split = splitInt(exp(i, 2));
            if (split.left + split.right == i)
                sb.Append(i.ToString() + " ");
        }
        
        if (sb.Length == 0)
            sb.Append("INVALID RANGE");
        else
            sb.Remove(sb.Length - 1, 1);

        Console.WriteLine(sb.ToString());
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        int p = Convert.ToInt32(Console.ReadLine().Trim());

        int q = Convert.ToInt32(Console.ReadLine().Trim());

        Result.kaprekarNumbers(p, q);
    }
}
