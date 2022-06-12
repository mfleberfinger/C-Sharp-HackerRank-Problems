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
     * Complete the 'twoPluses' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts STRING_ARRAY grid as parameter.
     */

    // It is unclear from the instructions if we must use the largest possible
    //  plus and the next largest possible, given that it doesn't overlap with
    //  the largest, or if we must find the largest product, regardless of whether
    //  that product includes the largest plus. For example, if the largest plus
    //  takes up 9 cells but overlaps with two 5-cell pluses and all other possible
    //  pluses are 1-cell pluses, should we use 9 * 1 = 9 or 5 * 5 = 25?
    //  BBGBBB
    //  BGGBGB
    //  GGGGGG
    //  BGGBGB
    //  BBGBBB
    // Based on comments in the discussion section, we want the largest product.
    //  We don't care if the product does not include the largest plus.
    //
    // How to find the 2 largest pluses?
    // The grid has, at most, 15 * 15 = 225 cells.
    // Iterate over all cells in the grid.
    // For each good cell, iterate up, left, down, and right from that cell until
    //  we encounter a bad cell or the edge of the grid in each direction.
    // The shortest distance to a bad cell or an edge determines the size of the
    //  plus centered on the current cell.
    // Save the location of the center cell and the size of each plus.
    // Find the two non-overlapping pluses whose areas result in the largest product.
    // Calculate and save the product of each permitted pair of pluses. Return the
    //  largest product.
    public static int twoPluses(List<string> grid)
    {
        List<((int i, int j) center, int area)> pluses =
            new List<((int i, int j) center, int area)>();
        List<int> products = new List<int>();
        int maxProduct = 0;
        
        // Get all of the possible pluses.
        // It's possible that this won't be enough...
        // I'm not find every possible plus. For example, I'm not finding
        //  a smaller plus that shares a center with a larger plus. I'm only
        //  finding the largest plus centered on each good square.
        for (int i = 0; i < grid.Count; i++)
        {
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] == 'G')
                {
                    foreach(int area in GetAreas((i, j), grid))
                        pluses.Add(((i, j), area));
                }
            }
        }
        
        for (int k = 0; k < pluses.Count; k++)
        {
            for (int l = k + 1; l < pluses.Count; l++)
            {
                if (!PlusesOverlap(pluses[k].center, pluses[k].area,
                    pluses[l].center, pluses[l].area))
                {
                    products.Add(pluses[k].area * pluses[l].area);
                }
            }
        }
        
        foreach(int product in products)
        {
            if (product > maxProduct)
                maxProduct = product;
        }
        
        return maxProduct;
    }
    
    // Determine whether two pluses overlap. Return true if they do, false otherwise.
    private static bool PlusesOverlap((int i, int j) center1, int size1,
        (int i, int j) center2, int size2)
    {
        // It seems like we would need to check 12 specific cases if we wanted to determine
        //  whether they overlap without explicitly checking every cell... It's more
        //  convenient to check every cell...
        bool overlap = false;
        HashSet<(int, int)> cells1 = new HashSet<(int, int)>();
        int arm;
        int k;
        
        cells1.Add(center1);
        arm = (size1 - 1) / 4;
        for (k = 1; k <= arm; k++)
        {
            cells1.Add((center1.i + k, center1.j));
            cells1.Add((center1.i - k, center1.j));
            cells1.Add((center1.i, center1.j + k));
            cells1.Add((center1.i, center1.j - k));
        }
        
        if (cells1.Contains(center2))
            overlap = true;
        arm = (size2 - 1) / 4;
        k = 1;
        while (!overlap && k <= arm)
        {
            if
            (
                cells1.Contains((center2.i + k, center2.j))
                || cells1.Contains((center2.i - k, center2.j))
                || cells1.Contains((center2.i, center2.j + k))
                || cells1.Contains((center2.i, center2.j - k))
            )
            {
                overlap = true;
            }

            k++;
        }
        
        return overlap;
    }

    /// <Summary>
    /// Calculate the areas of all pluses centered at <c>center</c> in <c>grid</c>.
    /// </Summary
    private static List<int> GetAreas((int i, int j) center, List<string> grid)
    {
        List<int> areas = new List<int>();
        int i, j;
        int up, left, down, right, min;
        bool armMaxReached = false;
        
        for (int arm = 0; !armMaxReached; arm++)
        {
            // up
            i = center.i - 1;
            j = center.j;
            up = 0;
            while (i >= 0 && grid[i][j] == 'G' && Math.Abs(i - center.i) <= arm)
            {
                up++;
                i--;
            }
            if (Math.Abs(i - center.i) < arm)
                armMaxReached = true;
            
            // left
            i = center.i;
            j = center.j - 1;
            left = 0;
            while (j >= 0 && grid[i][j] == 'G' && Math.Abs(j - center.j) <= arm)
            {
                left++;
                j--;
            }
            if (Math.Abs(j - center.j) < arm)
                armMaxReached = true;
            
            // down
            i = center.i + 1;
            j = center.j;
            down = 0;
            while (i < grid.Count && grid[i][j] == 'G' && Math.Abs(i - center.i) <= arm)
            {
                down++;
                i++;
            }
            if (Math.Abs(i - center.i) < arm)
                armMaxReached = true;
            
            // right
            i = center.i;
            j = center.j + 1;
            right = 0;
            while (j < grid[i].Length && grid[i][j] == 'G' && Math.Abs(j - center.j) <= arm)
            {
                right++;
                j++;
            }
            if (Math.Abs(j - center.j) < arm)
                armMaxReached = true;
            
            // Get the length of each arm of the plus (the minimum of up, left, down, and right).
            min = up;
            if (left < min)
                min = left;
            if (down < min)
                min = down;
            if (right < min)
                min = right;
            
            // The total area is made up of the four arms (min * 4) and the center
            //  cell (+ 1) of the plus.
            areas.Add(min * 4 + 1);
        }
        
        return areas;
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

        List<string> grid = new List<string>();

        for (int i = 0; i < n; i++)
        {
            string gridItem = Console.ReadLine();
            grid.Add(gridItem);
        }

        int result = Result.twoPluses(grid);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}