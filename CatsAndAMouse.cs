/*
Two cats and a mouse are at various positions on a line. You will be given their starting positions. Your task is to determine which cat will reach the mouse first, assuming the mouse does not move and the cats travel at equal speed. If the cats arrive at the same time, the mouse will be allowed to move and it will escape while they fight.

You are given
queries in the form of , , and representing the respective positions for cats and , and for mouse . Complete the function

to return the appropriate answer to each query, which will be printed on a new line.

    If cat 

catches the mouse first, print Cat A.
If cat

    catches the mouse first, print Cat B.
    If both cats reach the mouse at the same time, print Mouse C as the two cats fight and mouse escapes.

Example



The cats are at positions (Cat A) and (Cat B), and the mouse is at position . Cat B, at position will arrive first since it is only unit away while the other is

units away. Return 'Cat B'.

Function Description

Complete the catAndMouse function in the editor below.

catAndMouse has the following parameter(s):

    int x: Cat 

's position
int y: Cat
's position
int z: Mouse

    's position

Returns

    string: Either 'Cat A', 'Cat B', or 'Mouse C'

Input Format

The first line contains a single integer,
, denoting the number of queries.
Each of the subsequent lines contains three space-separated integers describing the respective values of (cat 's location), (cat 's location), and (mouse

's location).

Constraints

Sample Input 0

2
1 2 3
1 3 2

Sample Output 0

Cat B
Mouse C

Explanation 0

Query 0: The positions of the cats and mouse are shown below: image

Cat

will catch the mouse first, so we print Cat B on a new line.

Query 1: In this query, cats
and reach mouse

at the exact same time: image

Because the mouse escapes, we print Mouse C on a new line.
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

class Solution {

    // Complete the catAndMouse function below.
    static string catAndMouse(int x, int y, int z) {
        // The mouse does not move and the cats move at equal speeds.
        // It sounds like this is just asking "Is integer x or y closer to z?"
        if (Math.Abs(x - z) < Math.Abs(y - z))
            return "Cat A";
        else if (Math.Abs(y - z) < Math.Abs(x - z))
            return "Cat B";
        else
            return "Mouse C";
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int q = Convert.ToInt32(Console.ReadLine());

        for (int qItr = 0; qItr < q; qItr++) {
            string[] xyz = Console.ReadLine().Split(' ');

            int x = Convert.ToInt32(xyz[0]);

            int y = Convert.ToInt32(xyz[1]);

            int z = Convert.ToInt32(xyz[2]);

            string result = catAndMouse(x, y, z);

            textWriter.WriteLine(result);
        }

        textWriter.Flush();
        textWriter.Close();
    }
}
