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
     * Complete the 'surfaceArea' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts 2D_INTEGER_ARRAY A as parameter.
     */

    // Given a 2d grid, A, with an integer A[i,j] in each cell, calculate the outer
    //  surface area of the 3d object produced by stacking A[i,j] 1x1x1 cubes on each
    //  grid cell.
    // For example, if we have a 3x3 grid with a 3 in each cell, we would construct
    //  a 3x3x3 cube with a surface area of 3 * 3 * 3 = 27.
    
    // We may be able to look at all 6 surfaces of each cube.
    // The grid is, at most, 100x100 and A[i,j] is, at most, 100.
    // Therefore, we can have, at most, 1,000,000 cubes and 6,000,000 faces.
    // Still, it would be best to be more efficient if possible.
    //
    // This can probably be done by only checking the four edges of the,
    //  at most, 10,000 grid cells, rather than the faces of the cubes.
    // Iterate over all grid cells. For each cell, check every adjacent
    //  cell by subtracting the number in the adjacent cell from the number
    //  in the current cell.
    // For each cell A[i,j], keep track of the surface area contributed to
    //  the total.
    //  Start by considering the contribution of the current cell's column to
    //  be 2 + (4 * A[i,j]) (Top and bottom of the column plus all sides).
    //  If A[i,j] - (adjacent number) is less than or equal to zero,
    //  subtract A[i,j] (i.e. one entire side) from the current column's
    //  contribution. If A[i,j] - (adjacent number) is positive, subtract
    //  (adjacent number) from the current column's contribution (i.e. subtract
    //  the covered surface area).
    //  Once the current column's contribution is determined (after all adjacent
    //  cells have been checked), add it to the total surface area.
    public static int surfaceArea(List<List<int>> A)
    {
        int totalSurfaceArea = 0;
        int currentContribution = 0;
        for (int i = 0; i < A.Count; i++)
        {
            for (int j = 0; j < A[i].Count; j++)
            {
                currentContribution = 2 + (4 * A[i][j]);
                // Check the adjacent cells.
                // Above
                if (i > 0)
                    currentContribution -= GetOccluded(A[i][j], A[i - 1][j]);
                // Left
                if (j > 0)
                    currentContribution -= GetOccluded(A[i][j], A[i][j - 1]);
                // Below
                if (i + 1 < A.Count)
                    currentContribution -= GetOccluded(A[i][j], A[i + 1][j]);
                // Right
                if (j + 1 < A[i].Count)
                    currentContribution -= GetOccluded(A[i][j], A[i][j + 1]);
                
                totalSurfaceArea += currentContribution;
            }
        }
        return totalSurfaceArea;
    }

    // Get the surface area to subtract from current's contribution.
    private static int GetOccluded(int current, int adjacent)
    {
        int occluded = 0;
        if (current - adjacent <= 0)
            occluded = current;
        else
            occluded = adjacent;
        return occluded;
    }
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int H = Convert.ToInt32(firstMultipleInput[0]);

        int W = Convert.ToInt32(firstMultipleInput[1]);

        List<List<int>> A = new List<List<int>>();

        for (int i = 0; i < H; i++)
        {
            A.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(ATemp => Convert.ToInt32(ATemp)).ToList());
        }

        int result = Result.surfaceArea(A);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}