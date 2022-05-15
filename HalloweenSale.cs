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
     * Complete the 'howManyGames' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER p
     *  2. INTEGER d
     *  3. INTEGER m
     *  4. INTEGER s
     */

    // The instructions say that subtraction continues until prices is "less than
    // or equal to m dollars." This is wrong... the test cases (and the example in
    // the instructions) treat m as a hard minimum. In my function, "m" is called "minimum".
    public static int howManyGames(int startPrice, int decrease, int minimum,
        int budget)
    {
        int maxGames = 0;
        int price;
        int wallet = budget;
        
        for (price = startPrice;
            price > minimum && wallet >= price;
            wallet -= price, price -= decrease)
        {
            maxGames++;
        }
        
        if (price < minimum)
            price = minimum;
        
        maxGames += wallet / price;
        
        return maxGames;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int p = Convert.ToInt32(firstMultipleInput[0]);

        int d = Convert.ToInt32(firstMultipleInput[1]);

        int m = Convert.ToInt32(firstMultipleInput[2]);

        int s = Convert.ToInt32(firstMultipleInput[3]);

        int answer = Result.howManyGames(p, d, m, s);

        textWriter.WriteLine(answer);

        textWriter.Flush();
        textWriter.Close();
    }
}
