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
    // x^y
    // Apparently using Math.Pow() and casting to int is too slow...
    public static int pow(int x, int y) {
        if (x == 0)
            return 0;
        if (y == 0)
            return 1;
        int res = 1;
        for (int i = 0; i < y; i++)
            res *= x;
        return res;
    }
    
    public static int reverse(int i) {
        // split[0] will hold the ones place, split[1] the tens place, split[2] the hundreds, etc.
        List<int> split = new List<int>();
        int reversed = 0;
        int digits = 0;

        // Get the number of digits in the integer.
        for (int j = 0; j <= 6; j++) {
            if (i / pow(10, j) > 0)
                digits++;
        }

        // Use modular and integer division to split up the int.
        for (int j = 1; j <= digits + 1; j++) {
            split.Add(i % pow(10, j));
            split[j-1] = split[j-1] / pow(10, j - 1);
        }

        // Put it back together in reverse.
        for (int j = 0; j < digits; j++) {
            reversed += split[j] * pow(10, digits - 1 - j);
        }
    
        return reversed;
    }

    /*
     * Complete the 'beautifulDays' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER i
     *  2. INTEGER j
     *  3. INTEGER k
     */

    public static int beautifulDays(int i, int j, int k)
    {
        int count = 0;
        // Assuming the range [i...j] is inclusive... as the symbols [] used in
        // the instructions imply.
        for (int l = i; l <= j; l++) {
            if ((l - reverse(l)) % k == 0)
                count++;
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

        int i = Convert.ToInt32(firstMultipleInput[0]);

        int j = Convert.ToInt32(firstMultipleInput[1]);

        int k = Convert.ToInt32(firstMultipleInput[2]);

        int result = Result.beautifulDays(i, j, k);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
