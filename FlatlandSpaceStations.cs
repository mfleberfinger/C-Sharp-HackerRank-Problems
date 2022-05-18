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

class Solution {

    // Cities are numbered from 0 to n - 1    
    static int flatlandSpaceStations(int n, int[] c)
    {
        // distances[i] will be the minimum distance to a space station from city i.
        List<int> distances = new List<int>();
        int city = 0;
        int spaceStation = 0;
        int max = int.MinValue;
        
        // Sort the array in ascending order.
        Array.Sort(c);
        
        while (city < n)
        {
            if (spaceStation < c.Length - 1 &&
                Math.Abs(city - c[spaceStation + 1]) < Math.Abs(city - c[spaceStation]))
            {
                spaceStation++;
            }
            distances.Add(Math.Abs(city - c[spaceStation]));
            city++;
        }
        
        foreach(int distance in distances)
        {
            if (distance > max)
                max = distance;
        }
        
        return max;
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] nm = Console.ReadLine().Split(' ');

        int n = Convert.ToInt32(nm[0]);

        int m = Convert.ToInt32(nm[1]);

        int[] c = Array.ConvertAll(Console.ReadLine().Split(' '), cTemp => Convert.ToInt32(cTemp))
        ;
        int result = flatlandSpaceStations(n, c);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
