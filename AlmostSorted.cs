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
     * Complete the 'almostSorted' function below.
     *
     * The function accepts INTEGER_ARRAY arr as parameter.
     */

    // A swap is possible if, and only if, there are exactly two elements out of order.
    // Reversing is possible and desired if, and only if, sorting with a single
    //  swap is not possible and exactly one contiguous run of numbers sorted
    //  in descending order exists.
    public static void almostSorted(List<int> arr)
    {
        bool canSwap = false;
        (int, int) indices = (-1, -1);
        bool sorted = false;
        bool canReverse = true;
        int misplaced = 0;
                
        // Iterate through the array and see how many elements are misplaced.
        // If none are misplaced, it is sorted.
        // If exactly two are misplaced, we can swap.
        // The array is guaranteed to have at least two elements, so we don't need to
        // take the case where we are given a one element array into account.
        // Swapping is only possible if exactly two numbers are in the wrong place.
        for (int i = 1; i < arr.Count && misplaced < 3; i++)
        {
            if (arr[i - 1] > arr[i])
            {
                misplaced++;
                if (indices.Item1 == -1)
                    // arr[i - 1] is too large and must move right.
                    indices.Item1 = i - 1;
                else
                    // arr[i] is too small and must move left.
                    indices.Item2 = i;
            }
        }      
        
        // If the array is not already sorted and we cannot swap, see if we can reverse.
        if (misplaced == 0)
        {
            sorted = true;
        }
        else if (misplaced < 3)
        {
            // If we only found one place where elements are misplaced, we need to
            //  swap adjacent items.
            if (misplaced == 1)
                indices.Item2 = indices.Item1 + 1;
            
            // Make sure the swap will put the misplaced numbers in the right places.
            if
            (
                arr[indices.Item1] < arr[indices.Item2]
                // Value left of item1 greater than value at item2
                || (indices.Item1 > 0 && arr[indices.Item1 - 1] > arr[indices.Item2])
                // Value right of item1 less than value at item2
                || (arr[indices.Item1 + 1] < arr[indices.Item2])
                // Value left of item2 greater than value at item1
                || (arr[indices.Item2 - 1] > arr[indices.Item1])
                // Value right of item2 less than value at item1
                || (indices.Item2 < arr.Count - 1 && arr[indices.Item2 + 1] < arr[indices.Item1])
            )
            {
                canSwap = false;
            }
            else
            {
                canSwap = true;
            }
        }
        
        if (!sorted && !canSwap)
        {
            // See if we can reverse.
            // This is only possible if exactly one contiguous run of numbers sorted
            //  in descending order exists.
            // Additionally, the number at the end of the descending run must be
            //  greater than or equal to the number immediately before the descending run.
            indices = (-1, -1);
            for (int i = 1; i < arr.Count && canReverse; i++)
            {
                if (arr[i - 1] > arr[i])
                {
                    // If we haven't found a descending run, this is the beginning
                    //  of the first descending run.
                    if (indices.Item1 == -1)
                        indices.Item1 = i - 1;
                    // If we already found the end of a descending run and we find
                    //  something else out of order, we can't reverse.
                    else if (indices.Item2 > -1)
                        canReverse = false;
                }
                else if (indices.Item1 > -1 && indices.Item2 == -1)
                {
                    indices.Item2 = i - 1;
                }
            }
            // The descending run ends at the last item.
            if (indices.Item2 == -1)
                indices.Item2 = arr.Count - 1;
        }
        
        if (canReverse)
        {
            // Make sure that the the number at the end of the descending run is
            //  greater than or equal to the number immediately before the descending run.
            if (indices.Item1 > 0 && arr[indices.Item2] < arr[indices.Item1 - 1])
                canReverse = false;
            // Make sure that the number immediately after the descending run is
            //  greater than or equal to the number at the beginning of the descending run.
            if (indices.Item2 < arr.Count - 1 && arr[indices.Item2 + 1] < arr[indices.Item1])
                canReverse = false;
        }
        
        if (sorted)
            Console.WriteLine("yes");
        else if (canSwap)
            // + 1 because the problem insists on one-indexing.
            Console.WriteLine("yes\nswap {0} {1}", indices.Item1 + 1, indices.Item2 + 1);
        else if (canReverse)
            // + 1 because the problem insists on one-indexing.
            Console.WriteLine("yes\nreverse {0} {1}", indices.Item1 + 1, indices.Item2 + 1);
        else
            Console.WriteLine("no");
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        int n = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();

        Result.almostSorted(arr);
    }
}