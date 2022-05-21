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
     * Complete the 'cavityMap' function below.
     *
     * The function is expected to return a STRING_ARRAY.
     * The function accepts STRING_ARRAY grid as parameter.
     */

    public static List<string> cavityMap(List<string> grid)
    {
        List<string> newGrid = new List<string>();
        StringBuilder row;
        bool isCavity;
        
        for (int i = 0; i < grid.Count; i++)
        {
            row = new StringBuilder(grid[i]);
            for (int j = 0; j < grid.Count; j++)
            {
                isCavity = false;
                // Cells on the edges of the grid are not eligible. Don't check them.
                if (i > 0 && i < grid.Count - 1 &&
                    j > 0 && j < grid.Count - 1)
                {
                    // The characters are all digits, so we can just do character comparison
                    // directly here... there is no need to parse to ints.
                    isCavity = grid[i][j] > grid[i - 1][j] && grid[i][j] > grid[i][j - 1] &&
                        grid[i][j] > grid[i + 1][j] && grid[i][j] > grid[i][j + 1];
                }
                if (isCavity)
                    row[j] = 'X';
            }
            newGrid.Add(row.ToString());
        }
        
        return newGrid;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<string> grid = new List<string>();

        for (int i = 0; i < n; i++)
        {
            string gridItem = Console.ReadLine();
            grid.Add(gridItem);
        }

        List<string> result = Result.cavityMap(grid);

        textWriter.WriteLine(String.Join("\n", result));

        textWriter.Flush();
        textWriter.Close();
    }
}
