/*
Given an array of bird sightings where every element represents a bird type id, determine the id of the most frequently sighted type. If more than 1 type has been spotted that maximum amount, return the smallest of their ids.

Example
There are two each of types and , and one sighting of type . Pick the lower of the two types seen twice: type

.

Function Description

Complete the migratoryBirds function in the editor below.

migratoryBirds has the following parameter(s):

    int arr[n]: the types of birds sighted

Returns

    int: the lowest type id of the most frequently sighted birds

Input Format

The first line contains an integer,
, the size of .
The second line describes as

space-separated integers, each a type number of the bird sighted.

Constraints

It is guaranteed that each type is , , , , or

    .

Sample Input 0

6
1 4 4 4 5 3

Sample Output 0

4

Explanation 0

The different types of birds occur in the following frequencies:

    Type 

:
bird
Type
:
birds
Type
:
bird
Type
:
birds
Type
:

    bird

The type number that occurs at the highest frequency is type
, so we print

as our answer.

Sample Input 1

11
1 2 3 4 5 4 3 2 1 3 4

Sample Output 1

3

Explanation 1

The different types of birds occur in the following frequencies:

    Type 

: Type : Type : Type : Type : Two types have a frequency of , and the lower of those is type .
*/

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
     * Complete the 'migratoryBirds' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts INTEGER_ARRAY arr as parameter.
     */

    // Given an array of bird sightings where every element represents a bird type
    // id, return the id of the most frequently sighted type. If more than 1 type
    // has been spotted that maximum amount, return the smallest of their ids.
    public static int migratoryBirds(List<int> birds)
    {  
        // It's not clear if the input is always sorted. Make sure.
        birds.Sort();
        
        // Now that we know the input is sorted, we can sweep through it just once.
        int maxCount = 0;
        int mostSightedId = birds[0];
        int previousId = birds[0];
        int count = 0;
        foreach (int bird in birds) {
            if (bird == previousId)
                count++;
            else {
                if (count > maxCount) {
                    maxCount = count;
                    // We don't need to check for a lower ID here because we know
                    // that this ID is greater than or equal to the previous IDs because
                    // our list is sorted.
                    mostSightedId = previousId;
                }
                previousId = bird;
                count = 1;
            }
        }
        
        // We may still need to check the last bird.
        if (previousId == birds.Last() && birds.Count > 1 && count > maxCount)
            mostSightedId = previousId;
        
        return mostSightedId;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int arrCount = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> arr = Console.ReadLine().TrimEnd().Split(' ').ToList().Select(arrTemp => Convert.ToInt32(arrTemp)).ToList();

        int result = Result.migratoryBirds(arr);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
