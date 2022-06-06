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
     * Complete the 'strangeCounter' function below.
     *
     * The function is expected to return a LONG_INTEGER.
     * The function accepts LONG_INTEGER t as parameter.
     */
    
    // The optimal (constant time) way to do this would be to come up with
    // a recurrence to calculate the value of the timer at t, then find a
    // closed form, if one exists. Otherwise, just write a loop or a recursive
    // function to directly compute t in linear time (O(t), where t can be up to 10^12).
    // The linear solution may not be sufficient. It may be necessary to either find
    // a recurrence and a closed form or write a loop that somehow skips cycles or parts
    // of cycles of the counter.
    // This is supposedly an "easy" problem with an 81% success rate... don't spend too long
    // trying to come up with a closed form.
    //
    // Can't find a recurrence or a closed form... Just start with a loop or a recursive function.
    // Maybe try to come up with a recurrence based on the recursive function or come up with a
    // summation formula based on the loop.
    
    /*
    // Iterative
    public static long strangeCounter(long t)
    {
        long value = 3;
        long cycleStartValue = 3;
        for (long time = 2; time <= t; time++)
        {
            if (value == 1)
            {
                value = cycleStartValue * 2;
                cycleStartValue = value;
            }
            else
            {
                value--;
            }
        }
        return value;
    }
    */
    
    /*
    // Recursive
    public static long strangeCounter(long t, long time = 1,
        long value = 3, long startValue = 3)
    {
        if (time == t)
        {
            return value;
        }
        else
        {
            if (value - 1 == 0)
                return strangeCounter(t, time + 1, startValue * 2, startValue * 2);
            else
                return strangeCounter(t, time + 1, value - 1, startValue);
        }        
    }
    */
    
    // O(lg(n)) solution from the discussion section:
    //  Start with a counter variable set to 3 and a time variable set to t.
    //  While time >= counter, subtract counter from time and multiply counter by 2.
    //      This leaves us with the starting value of the counter in the last cycle
    //      and the amount of time spent in the last cycle.
    //  Subtract the remaining time from the counter variable to get the value of
    //      the counter at time t.
    public static long strangeCounter(long t)
    {
        long counter = 3;
        long time = t;
        
        while (time > counter)
        {
            time -= counter;
            counter *= 2;
        }
        
        return counter - time + 1;
    }
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        long t = Convert.ToInt64(Console.ReadLine().Trim());

        long result = Result.strangeCounter(t);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}