/*
A teacher asks the class to open their books to a page number. A student can either start turning pages from the front of the book or from the back of the book. They always turn pages one at a time. When they open the book, page

is always on the right side:

image

When they flip page
, they see pages and . Each page except the last page will always be printed on both sides. The last page may only be printed on the front, given the length of the book. If the book is pages long, and a student wants to turn to page

, what is the minimum number of pages to turn? They can start at the beginning or the end of the book.

Given
and , find and print the minimum number of pages that must be turned in order to arrive at page

.

Example


Untitled Diagram(4).png

Using the diagram above, if the student wants to get to page
, they open the book to page , flip page and they are on the correct page. If they open the book to the last page, page , they turn page and are at the correct page. Return

.

Function Description

Complete the pageCount function in the editor below.

pageCount has the following parameter(s):

    int n: the number of pages in the book
    int p: the page number to turn to

Returns

    int: the minimum number of pages to turn

Input Format

The first line contains an integer
, the number of pages in the book.
The second line contains an integer,

, the page to turn to.

Constraints

Sample Input 0

6
2

Sample Output 0

1

Explanation 0

If the student starts turning from page
, they only need to turn

page:

Untitled Diagram(6).png

If a student starts turning from page
, they need to turn

pages:

Untitled Diagram(3).png

Return the minimum value,

.

Sample Input 1

5
4

Sample Output 1

0

Explanation 1

If the student starts turning from page
, they need to turn

pages:

Untitled Diagram(4).png

If they start turning from page

, they do not need to turn any pages:

Untitled Diagram(5).png

Return the minimum value,
.
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
     * Complete the 'pageCount' function below.
     *
     * The function is expected to return an INTEGER.
     * The function accepts following parameters:
     *  1. INTEGER n
     *  2. INTEGER p
     */

    /*
    
        page 1 always on the left

        if last page is odd, it's on the right
        else last page on left

        Cases:

            Starting from page 1, getting to an even page number, p, requires p / 2 page
                turns.
            Starting from page 1, getting to an odd page number, p, requires (p - 1) / 2
                page turns.
                - This is one case: floor(p / 2) i.e. integer division p / 2

            Starting from an even numbered last page (a page on the left), n, getting to
                an even page number, p, requires (n - p) / 2.
            
            Starting from an even numbered last page (a page on the left), n, getting to
                an odd page number, p, requires (n - (p - 1)) / 2.
                - If the even-even case is correct, this is correct because all of the
                    odd numbered pages have an even numbered page to their left that is
                    one smaller than the odd page. Page 1 can be considered to have a
                    page 0 to its left for the purpose of this explanation/calculation.
            
            Starting from an odd numbered last page (a page on the right), n, getting to
                an even page number, p, requires ((n - 1) - p) / 2.
                - If the even-even case is correct, this is correct because n - 1 is the
                    even page facing the odd page, n.
            
            Starting from an odd numbered last page (a page on the right), n, getting to
                an odd page number, p, requires
                ((n - 1) - (p - 1)) / 2 = (n - 1 - p + 1) / 2 = (n - p) / 2.
    */
    public static int pageCount(int n, int p)
    {
        int fromFront;
        int fromBack;
        
        fromFront = p / 2;
        
        // even n
        if (n % 2 == 0) {
            // even p
            if (p % 2 == 0)
                fromBack = (n - p) / 2;
            // odd p
            else
                fromBack = (n - (p - 1)) / 2;
        }
        // odd n
        else {
            if (p % 2 == 0)
                fromBack = (n - 1 - p) / 2;
            else
                fromBack = (n - p) / 2;
        }
        
        return fromFront < fromBack ? fromFront : fromBack;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int n = Convert.ToInt32(Console.ReadLine().Trim());

        int p = Convert.ToInt32(Console.ReadLine().Trim());

        int result = Result.pageCount(n, p);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
