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
     * Complete the 'bomberMan' function below.
     *
     * The function is expected to return a STRING_ARRAY.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. STRING_ARRAY grid
     */
    
    // t = 0: initial state
    // t = 2: bombs planted in all squares
    // t = 3: first detonation
    // t = 4: bombs planted in all squares
    // t = 5: second detonation
    // t = 6: bombs planted in all squares
    // t = 7: third detonation
    // ...
    // t = 2k where k >= 1: bombs planted in all squares
    // t = 2k + 1: kth detonation
    
    // Each cell can be in one of three meaningful states.
    private enum State
    {
        // The cell is empty.
        Empty,
        // The cell contains a bomb planted in the previous round.
        Previous,
        // The cell contains a bomb planted in this round.
        Current
    }
/* TOO SLOW    
    public static List<string> bomberMan(int n, List<string> grid)
    {
        // Turn the grid into a List of Lists so we can freely mutate individual cells.
        List<List<State>> cells = new List<State>();
        for (int i = 0; i < grid.Count; i++)
        {
            cells.Add(new List<State>());
            for (int j = 0; j < grid[i].Length; j++)
            {
                if (grid[i][j] == 'O')
                    cells[i].Add(State.Previous);
                else
                    cells[i].Add(State.Empty);
            }
        }
        
        // Simulate n seconds.
        // We shouldn't need to iterate through each second explicitly.
        // The result of each round is entirely determined by the bombs leftover
        //  from the previous round.
        // This lets us divide the number of iterations by 3.
        // This still results in up to 333,333,333 iterations... before the up to
        //  40,000 iterations required at each step to modify the grid...
        //  333,333,333 * 40,000 > 12 trillion total iterations.
        // This is too slow.
        for (int t = 0; t <= n; t += 3)
        {
            // Too slow.
        }
        
        // Turn the grid back into a list of strings and return it.
        List<string> output = new List<string>();
        StringBuilder row;
        for (int i = 0; i < cells.Count; i++)
        {
            row = new StringBuilder();
            for (int j = 0; j < cells[i].Count; j++)
            {
                if (cells[i][j] == State.Empty)
                    row.Append('.');
                else
                    row.Append('O');
            }
            output.Add(row.ToString());
        }
        return output;
    } */

/*
    // Detect loops and halting? This seems needlessly complicated...
    // Start iterating through the simulation.
    // At the start of each round, put the current grid in a hashset.
    // If the current state already exists in the hashset, we have a loop.
    // Save the current state and continue iterating through the simulation.
    // Count the number of rounds it takes to get back to this state.
    // Calculate the last round, prior to the nth second, in which we will find
    //  ourselves in this state
    // Set t (the time iteration variable) to the time at which we will be in
    //  this state for the last time. Iterate from t to n to complete the simulation.
    public static List<string> bomberMan(int n, List<string> grid)
    {
    } */
    
    // Assume that there only ever two possible states for odd times after the
    //  initial state.
    public static List<string> bomberMan(int n, List<string> grid)
    {
        List<string> result = new List<string>();
        if (n < 2)
        {
            result = grid;
        }
        else if (n % 2 == 0)
        {
            StringBuilder filled = new StringBuilder();
            string filledString;
            
            for (int i = 0; i < grid[0].Length; i++)
                filled.Append('O');
                
            filledString = filled.ToString();
            for (int i = 0; i < grid.Count; i++)
                result.Add(filledString);
        }
        else
        {
            // The result at n is either the state at t = 3 or the state at t = 5.
            // I.e. the first detonation or the second detonation (the odd or even detonations).
            // Calculate which one it is.
            // The mth detonation takes place at t = 2m + 1.
            // We want to calculate m: m = (t - 1) / 2
            // We then just determine whether m is even (m % 2 == 0) or odd (m % 2 != 0).
            
            // Decide whether we're interested in the even or odd detonations.
            int m = (n - 1) / 2;
            
            
            // Get the state we're interested in...
            
            // Turn the grid into a List of Lists so we can freely mutate individual cells.
            List<List<State>> cells = new List<List<State>>();
            for (int i = 0; i < grid.Count; i++)
            {
                cells.Add(new List<State>());
                for (int j = 0; j < grid[i].Length; j++)
                {
                    if (grid[i][j] == 'O')
                        cells[i].Add(State.Previous);
                    else
                        cells[i].Add(State.Empty);
                }
            }
            
            int booms;
            // We want the even detonations (detonation 2).
            if (m % 2 == 0)
                booms = 2;
            // We want the even detonations (detonation 1).
            else
                booms = 1;
            StringBuilder row;
            for (int i = 0; i < booms; i++)
            {
                // Add new bombs.
                for (int j = 0; j < cells.Count; j++) // jth row
                {
                    for (int k = 0; k < cells[j].Count; k++) // kth column
                    {
                        if (cells[j][k] == State.Empty)
                        {
                            cells[j][k] = State.Current;
                        }
                    }
                }
                
                // Detonations
                for (int j = 0; j < cells.Count; j++) // jth row
                {
                    for (int k = 0; k < cells[j].Count; k++) // kth column
                    {
                        // If this cell is exploding, clear its neighbors.
                        if (cells[j][k] == State.Previous)
                        {
                            // above
                            if (j > 0 && cells[j - 1][k] != State.Previous)
                                cells[j - 1][k] = State.Empty;
                            // left
                            if (k > 0 && cells[j][k - 1] != State.Previous)
                                cells[j][k - 1] = State.Empty;
                            // below
                            if (j < cells.Count - 1 && cells[j + 1][k] != State.Previous)
                                cells[j + 1][k] = State.Empty;
                            // right
                            if (k < cells[j].Count - 1 && cells[j][k + 1] != State.Previous)
                                cells[j][k + 1] = State.Empty;
                            // self
                            cells[j][k] = State.Empty;
                        }
                    }
                }
                
                // Advance countdowns for remaining bombs.
                for (int j = 0; j < cells.Count; j++) // jth row
                {
                    for (int k = 0; k < cells[j].Count; k++) // kth column
                    {
                        if (cells[j][k] == State.Current)
                        {
                            cells[j][k] = State.Previous;
                        }
                    }
                }
            }
            
            // Turn the grid back into a list of strings so we can return it.
            result = new List<string>();
            for (int i = 0; i < cells.Count; i++)
            {
                row = new StringBuilder();
                for (int j = 0; j < cells[i].Count; j++)
                {
                    if (cells[i][j] == State.Empty)
                        row.Append('.');
                    else
                        row.Append('O');
                }
                result.Add(row.ToString());
            }
        }
        
        return result;
    }
}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] firstMultipleInput = Console.ReadLine().TrimEnd().Split(' ');

        int r = Convert.ToInt32(firstMultipleInput[0]);

        int c = Convert.ToInt32(firstMultipleInput[1]);

        int n = Convert.ToInt32(firstMultipleInput[2]);

        List<string> grid = new List<string>();

        for (int i = 0; i < r; i++)
        {
            string gridItem = Console.ReadLine();
            grid.Add(gridItem);
        }

        List<string> result = Result.bomberMan(n, grid);

        textWriter.WriteLine(String.Join("\n", result));

        textWriter.Flush();
        textWriter.Close();
    }
}