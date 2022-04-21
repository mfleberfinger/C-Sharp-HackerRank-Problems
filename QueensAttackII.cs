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
    // Directions from the queen.
    const int N = 0, NW = 1, W = 2, SW = 3, S = 4, SE = 5, E = 6, NE = 7;

    // Get the square at which a line drawn in the given direction from `position`
    // leaves the board.
    private static (int row, int col) getIntercept((int row, int col) position, int n, int direction)
    {
        // For diagonal directions, the edge at which the line leaves the board
        // is the closer of the two relevant horizontal and vertical edges.
        // If the two edges are the same distance from the given point, the line
        // leaves at the corner square.
        // Furthermore, the exact square at which the line leaves the board is
        // located a number of rows and columns from `position` equal to the
        // horizontal or vertical distance to the closer edge.
        (int row, int col) intercept;
        int dFromVerticalEdge;
        int dFromHorizontalEdge;
        switch (direction)
        {
            case N:
                intercept = (n, position.col);
                break;
            case NW:
                dFromVerticalEdge = position.col - 1;
                dFromHorizontalEdge = n - position.row;
                // Line leaves from left edge.
                if (dFromVerticalEdge < dFromHorizontalEdge)
                    intercept = (position.row + dFromVerticalEdge, 1);
                // Line leaves from top edge.
                else if (dFromVerticalEdge > dFromHorizontalEdge)
                    intercept = (n, position.col - dFromHorizontalEdge);
                // Line leaves from corner.
                else
                    intercept = (n, 1);
                break;
            case W:
                intercept = (position.row, 1);
                break;
            case SW:
                dFromVerticalEdge = position.col - 1;
                dFromHorizontalEdge = position.row - 1;
                // Line leaves from left edge.
                if (dFromVerticalEdge < dFromHorizontalEdge)
                    intercept = (position.row - dFromVerticalEdge, 1);
                // Line leaves from bottom edge.
                else if (dFromVerticalEdge > dFromHorizontalEdge)
                    intercept = (1, position.col - dFromHorizontalEdge);
                // Line leaves from corner.
                else
                    intercept = (1, 1);
                break;
            case S:
                intercept = (1, position.col);
                break;
            case SE:
                dFromVerticalEdge = n - position.col;
                dFromHorizontalEdge = position.row - 1;
                // Line leaves from right edge.
                if (dFromVerticalEdge < dFromHorizontalEdge)
                    intercept = (position.row - dFromVerticalEdge, n);
                // Line leaves from bottom edge.
                else if (dFromVerticalEdge > dFromHorizontalEdge)
                    intercept = (1, position.col + dFromHorizontalEdge);
                // Line leaves from corner.
                else
                    intercept = (1, n);
                break;
            case E:
                intercept = (position.row, n);
                break;
            case NE:
                dFromVerticalEdge = n - position.col;
                dFromHorizontalEdge = n - position.row;
                // Line leaves from right edge.
                if (dFromVerticalEdge < dFromHorizontalEdge)
                    intercept = (position.row + dFromVerticalEdge, n);
                // Line leaves from top edge.
                else if (dFromVerticalEdge > dFromHorizontalEdge)
                    intercept = (n, position.col + dFromHorizontalEdge);
                // Line leaves from corner.
                else
                    intercept = (n, n);
                break;
        }
        return intercept;
    }
    
    // Get the distance from square a to square b.
    // Distance is board game distance (i.e. the starting square is not
    // counted but the ending square is).
    // Assumes that a and b are either in the same row, the same column or the
    // same diagonal.
    private static int getDistance((int row, int col) a, (int row, int col) b)
    {
        // Apparently, the diagonal distance between two squares on a
        // chessboard (assuming they lie along a diagonal on the board) is
        // equal to the the greater of the horizontal and vertical distance
        // between the two squares (https://math.stackexchange.com/a/1346017).
        // This is also technically true for squares located in the same row
        // or column (because one of the distances is zero).
        int v = Math.Abs(a.row - b.row);
        int h = Math.Abs(a.col - b.col);
        return Math.Max(h, v);
    }
    
    // Either return the direction from square a to square b, or return null
    // if b does not lie in one of the 8 defined directions to which we've
    // assigned constants.
    // This will also return null if a and b are on the same square.
    private static int? tryGetDirection((int row, int col) a, (int row, int col) b)
    {
        int? direction = null;
        
        // Check horizontal directions.
        if (a.row == b.row)
        {
            if (b.col > a.col)
                direction = E;
            else if (b.col < a.col)
                direction = W;
        }
        // Check vertical directions.
        else if (a.col == b.col)
        {
            if (b.row > a.row)
                direction = N;
            else if (b.row < a.row)
                direction = S;
        }
        // Check the diagonals.
        // If a and b share a diagonal, they will be as far from each other
        // vertically as they are horizontally.
        else if (Math.Abs(a.row - b.row) == Math.Abs(a.col - b.col))
        {
            if (a.row > b.row && a.col > b.col)
                direction = SW;
            else if (a.row > b.row && a.col < b.col)
                direction = SE;
            else if (a.row < b.row && a.col < b.col)
                direction = NE;
            else if (a.row < b.row && a.col > b.col)
                direction = NW;
        }
        
        return direction;
    }
    
    // Return an array with the closest obstacle (or int.MaxValue) in each of the
    // directions defined at the top of this class.
    private static int[] getClosestObstacles((int row, int col) queenLoc,
        List<List<int>> obstacles)
    {
        int? direction;
        int distance;
        (int row, int col) obsLoc;
        int[] closest = new int[8];
             
        for (int i = 0; i < 8; i++)
            closest[i] = int.MaxValue;
        
        foreach (List<int> obstacle in obstacles)
        {
            obsLoc = (obstacle[0], obstacle[1]);
            direction = tryGetDirection(queenLoc, obsLoc);
            if (direction.HasValue)
            {
                distance = getDistance(queenLoc, obsLoc);
                if (distance < closest[direction.Value])
                    closest[direction.Value] = distance;
            }
        }
        
        return closest;
    }

    /*
     * Complete the 'queensAttack' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. INTEGER k
     *  3. INTEGER r_q
     *  4. INTEGER c_q
     *  5. 2D_INTEGER_ARRAY obstacles
     */

    public static int queensAttack(int n, int k, int r_q, int c_q, List<List<int>> obstacles)
    {
        // This cannot be done by brute force. It is possible to have a
        // 100,000 x 100,000 "chessboard."
        
        // Finding the number of squares blocked and threatened on the horizontals
        // and verticals is probably done with simple arithmetic.
        // The diagonals probably require pythagorean theorem?
        // 
        // Remember that the obstacles list is a list of ordered pairs, giving
        // the locations of obstacles, not a matrix of all squares on the board.
        
        // Possible solution:
        //  Iterate through the obstacles list. For each obstacle, determine whether
        //  that obstacle is in one of the queen's 8 lines of attack. Determine
        //  whether it is the closest obstacle to the queen along that line. If so,
        //  overwrite the previous shortest distance on that line (either store the
        //  8 shortest distances in an array or in 8 separate variables). Initialize
        //  the 8 distances to int.MaxValue.
        //  After iterating through all of the obstacles, iterate through the 8 closest
        //  obstacles. For each closest "obstacle," if the obstacle is closer than the
        //  edge of the chessboard, add the distance from the queen to the obstacle
        //  (minus 1?) to the total number of squares threatened. Otherwise, add the
        //  distance to the edge of the board.
        
        // Remember, chessboard coordinates are one-indexed.
        
        // To index into this array, use the compass direction integer constants
        // defined at the top of this class.
        (int row, int col) queenLoc = (r_q, c_q);
        int[] closest = getClosestObstacles(queenLoc, obstacles);
        int threatened = 0;
        int d;
        
        for (int i = 0; i < 8; i++)
        {
            d = getDistance(queenLoc, getIntercept(queenLoc, n, i));
            if (closest[i] > d)
                threatened += d;
            else
                threatened += closest[i] - 1; // -1 because the square is occupied
        }
        
        return threatened;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int n = Convert.ToInt32(firstMultipleInput[0]);

        int k = Convert.ToInt32(firstMultipleInput[1]);

        string[] secondMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int r_q = Convert.ToInt32(secondMultipleInput[0]);

        int c_q = Convert.ToInt32(secondMultipleInput[1]);

        List<List<int>> obstacles = new List<List<int>>();

        for (int i = 0; i < k; i++)
        {
            obstacles.Add(Console.ReadLine().TrimEnd().Split(' ').ToList().Select(obstaclesTemp => Convert.ToInt32(obstaclesTemp)).ToList());
        }

        int result = Result.queensAttack(n, k, r_q, c_q, obstacles);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
