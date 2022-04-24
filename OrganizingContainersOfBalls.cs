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
     * Complete the 'organizingContainers' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts 2D_INTEGER_ARRAY container as parameter.
     */

    public static string organizingContainers(List<List<int>> container)
    {
        // Key observations:
        //  The the total number of balls in any container cannot change because
        //      we must add a ball each time we remove a ball (i.e. swap).
        //  In other words, we must have a set number of balls in each container.
        //  If we count how many balls of each type we have, we can compare the
        //      number of balls of each type to the number of balls allowed in
        //      each container. If the container and ball type numbers can be
        //      matched exactly, then the sort is possible. Otherwise, it is
        //      impossible.
        // 
        // Count the total number of balls in each container. Put these counts
        //  in a list.
        // Count the number of balls of each type across all containers. Put these
        //  counts in another list.
        // Sort the two lists.
        // Compare the sorted lists. If they are the same, Possible. Else, Impossible.
        
        List<int> ballsPerContainer = new List<int>();
        List<int> ballsOfType = new List<int>();
        
        // Initialize the lists.
        for (int i = 0; i < container.Count; i++)
        {
            ballsPerContainer.Add(0);
            ballsOfType.Add(0);
        }
        
        for (int i = 0; i < container.Count; i++)
        {
            for (int j = 0; j < container.Count; j++)
            {
                ballsPerContainer[i] += container[i][j];
                ballsOfType[j] += container[i][j];
            }
        }
        
        ballsPerContainer.Sort();
        ballsOfType.Sort();
        
        bool possible = true;
        int k = 0;
        while (possible && k < ballsPerContainer.Count)
        {
            if (ballsPerContainer[k] != ballsOfType[k])
                possible = false;
            k++;
        }

        return possible ? "Possible" : "Impossible";
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int q = Convert.ToInt32(Console.ReadLine().Trim());

        for (int qItr = 0; qItr < q; qItr++)
        {
            int n = Convert.ToInt32(Console.ReadLine().Trim());

            List<List<int>> container = new List<List<int>>();

            for (int i = 0; i < n; i++)
            {
                container.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(containerTemp => Convert.ToInt32(containerTemp)).ToList());
            }

            string result = Result.organizingContainers(container);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
