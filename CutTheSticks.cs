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
     * Complete the 'cutTheSticks' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts INTEGER_ARRAY arr as parameter.
     */

    public static List<int> cutTheSticks(List<int> arr)
    {
        List<int> answer = new List<int>();
        arr.Sort();
        int i = 0;
        int j;
        int cut;
        
        while (i < arr.Count)
        {
            cut = arr[i];
            j = i;
            // Record the size of the array at the start of this cutting iteration.
            answer.Add(arr.Count - i);
            while (j < arr.Count)
            {
                // If stick j is equal to the cut length, we will dispose
                // of it. Move i to the disposed stick.
                if (cut == arr[j])
                    i = j;
                // Otherwise, we are past the last stick we are disposing of
                // and we will cut stick j.
                else
                   arr[j] -= cut;
                j++;
            }
            // Index i should now be on the last stick we will dispose of.
            // Move i to the shortest remaining stick.
            i++;
        }
        return answer;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();

        List<int> result = Result.cutTheSticks(arr);

        textWriter.WriteLine(String.Join("\n", result));

        textWriter.Flush();
        textWriter.Close();
    }
}
