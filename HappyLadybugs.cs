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
     * Complete the 'happyLadybugs' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts STRING b as parameter.
     */

    // It is possible to make all of the ladybugs happy as long as statements 1
    // and 2 below are true or statement 0 is true:
    //  0. The given string is already solved.
    //  1. There is at least one empty space.
    //  2. Each letter that appears in the string appears at least twice.
    public static string happyLadybugs(string b)
    {
        bool possible = true;
        
        // Are any moves required to make the ladybugs happy (statement 0 above)?
        // For each character, check whether either of its adjacent spaces contains
        // the same character.
        for (int i = 0; i < b.Length; i++)
        {
            possible = possible &&
            (
                // We don't care what's adjacent to a blank space.
                (b[i] == '_') ||
                // Is there a matching character to our left?
                (i - 1 >= 0 && b[i] == b[i - 1]) ||
                // Is there a matching character to our right?
                (i + 1 < b.Length && b[i] == b[i + 1])
            );
        }
        
        // If the ladybugs are not already happy, can the string be rearranged
        // to make the ladybugs happy (statements 1 and 2 above)?
        const int asciiOffset = 65; // The ASCII value of 'A' is 65.
        bool hasEmpty = false;
        int[] colors = new int[26];
        for (int i = 0; i < colors.Length; i++)
            colors[i] = 0;
        if (!possible)
        {
            possible = true;
            foreach(char c in b)
            {
                if (c != '_')
                    colors[(int)c - asciiOffset]++;
                else
                    hasEmpty = true;
            }
            
            // Impossible if no empty space exists.
            possible = possible && hasEmpty;
            // Impossible if we have exactly one of any character.
            foreach(int i in colors)
            {
                possible = possible && i != 1;
            }
        }
        
        return possible ? "YES" : "NO";
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int g = Convert.ToInt32(Console.ReadLine().Trim());

        for (int gItr = 0; gItr < g; gItr++)
        {
            int n = Convert.ToInt32(Console.ReadLine().Trim());

            string b = Console.ReadLine();

            string result = Result.happyLadybugs(b);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}