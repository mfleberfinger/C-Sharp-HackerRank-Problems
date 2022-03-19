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
HackerLand University has the following grading policy:

    Every student receives a 

in the inclusive range from to
.
Any
less than

    is a failing grade.

Sam is a professor at the university and likes to round each student's

according to these rules:

    If the difference between the 

and the next multiple of is less than , round up to the next multiple of
.
If the value of
is less than

    , no rounding occurs as the result will still be a failing grade.

Examples

round to
(85 - 84 is less than 3)
do not round (result is less than 40)

    do not round (60 - 57 is 3 or higher)

Given the initial value of
for each of Sam's

students, write code to automate the rounding process.

Function Description

Complete the function gradingStudents in the editor below.

gradingStudents has the following parameter(s):

    int grades[n]: the grades before rounding

Returns

    int[n]: the grades after rounding as appropriate

Input Format

The first line contains a single integer,
, the number of students.
Each line of the subsequent lines contains a single integer,

.

Constraints

Sample Input 0

4
73
67
38
33

Sample Output 0

75
67
40
33

Explanation 0

image

    Student 

received a , and the next multiple of from is . Since , the student's grade is rounded to
.
Student
received a , and the next multiple of from is . Since , the grade will not be modified and the student's final grade is
.
Student
received a , and the next multiple of from is . Since , the student's grade will be rounded to
.
Student
received a grade below , so the grade will not be modified and the student's final grade is .
*/

    /*
     * Complete the 'gradingStudents' function below.
     *
     * The function is expected to return an INTEGER_ARRAY.
     * The function accepts INTEGER_ARRAY grades as parameter.
     */

    public static List<int> gradingStudents(List<int> grades)
    {
        List<int> results = new List<int>();
        int nextMultiple;
        
        foreach (int g in grades) {
            if (g < 38) // Failure. No rounding.
                results.Add(g);
            else {
                // Find the next multiple of 5.
                nextMultiple = ((g / 5) + 1) * 5;
                // If fewer than 3 points away from the next multiple of 5, round up to that multiple.
                if (nextMultiple - g < 3)
                    results.Add(nextMultiple);
                else
                    results.Add(g);
            }
        }
        return results;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int gradesCount = Convert.ToInt32(Console.ReadLine().Trim());

        List<int> grades = new List<int>();

        for (int i = 0; i < gradesCount; i++)
        {
            int gradesItem = Convert.ToInt32(Console.ReadLine().Trim());
            grades.Add(gradesItem);
        }

        List<int> result = Result.gradingStudents(grades);

        textWriter.WriteLine(String.Join("\n", result));

        textWriter.Flush();
        textWriter.Close();
    }
}
