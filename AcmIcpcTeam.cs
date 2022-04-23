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
     * Complete the 'acmTeam' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts STRING_ARRAY topic as parameter.
     */

    // Each element of the people array is a string of 0s and 1s
    // indicating the topics known by each person. A '1' means the
    // person knows the topic and a '0' means they do not.
    // Each string will be of the same length.
    // The people in the array can be paired up to form teams.
    // Find the best team of two, i.e. the pair with the largest number of
    // known topics between them. Count the number of possible best teams.
    // Return 2 integers, [topics, teams], giving the maximum number of topics
    // the best team knows and the number of teams that could be formed such that
    // they know that number of topics.
    public static List<int> acmTeam(List<string> people)
    {
        // The solution is probably to test all n^2 pairs and count the number
        // of best teams.
        int mostTopics = 0;
        int teams = 0;
        int knownTopics;
        
        for (int i = 0; i < people.Count; i++)
        {
            for (int j = i + 1; j < people.Count; j++)
            {
                knownTopics = 0;
                for (int k = 0; k < people[0].Length; k++)
                {
                    if (people[i][k] == '1' || people[j][k] == '1')
                        knownTopics++;
                }
                if (knownTopics > mostTopics)
                {
                    mostTopics = knownTopics;
                    teams = 1;
                }
                else if (knownTopics == mostTopics)
                {
                    teams++;
                }
            }
        }
        
        return new List<int>() { mostTopics, teams };
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int n = Convert.ToInt32(firstMultipleInput[0]);

        int m = Convert.ToInt32(firstMultipleInput[1]);

        List<string> topic = new List<string>();

        for (int i = 0; i < n; i++)
        {
            string topicItem = Console.ReadLine();
            topic.Add(topicItem);
        }

        List<int> result = Result.acmTeam(topic);

        textWriter.WriteLine(String.Join("\n", result));

        textWriter.Flush();
        textWriter.Close();
    }
}
