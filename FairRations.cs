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

    // From the discussion, an instance of this problem is possible if and only if
    // the sum of all values in the array is even.
    // 
    // An even number of loaves must be given out because we must give loaves to
    // two people each time we want to give out a loaf.
    // It is not possible to make an odd number even by adding only even numbers to it.
    // A sum can only be odd if it contains odd numbers.
	// A sum cannot be odd if it only contains even numbers.
	// Therefore, if each subject has an even number of loaves, the sum of the
	// array's values must be even; and; if the sum of the array's values is odd,
	// then someone must have an odd number of loaves. Because we are unable to
	// make an odd sum even, we are also unable to make each subject have an even
	// number of loaves if the sum is initially odd.
    public static string fairRations(List<int> B)
    {
        int sum = 0;
        int minimum = -1;
        
        // A solution exists if and only if the sum of all values in B is even.
        foreach (int i in B)
        {
            sum += i;
        }
        if (sum % 2 == 0)
        {
            // Algorithm from the discussion... I don't know how to determine that this
            // always calculates the minimum correctly...
            // I think this problem is only "easy" and the success rate is so high because
            // The answer is in the discussion and it is easy to implement once it is known.
			// The editorial actually gives a somewhat helpful explanation for this problem.
			// Assuming it hasn't disappeared: https://www.hackerrank.com/challenges/fair-rations/editorial
            minimum = 0;
            for (int i = 0; i < B.Count - 1; i++)
            {
                if (B[i] % 2 != 0)
                {
                    B[i]++;
                    B[i + 1]++;
                    minimum += 2;
                }
            }
        }
        
        return minimum > -1 ? minimum.ToString() : "NO";
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int N = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> B = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(BTemp => Convert.ToInt32(BTemp)).ToList();

        string result = Result.fairRations(B);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
