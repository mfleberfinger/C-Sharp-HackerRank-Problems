/*
A person wants to determine the most expensive computer keyboard and USB drive that can be purchased with a give budget. Given price lists for keyboards and USB drives and a budget, find the cost to buy them. If it is not possible to buy both items, return

.

Example


The person can buy a , or a . Choose the latter as the more expensive option and return

.

Function Description

Complete the getMoneySpent function in the editor below.

getMoneySpent has the following parameter(s):

    int keyboards[n]: the keyboard prices
    int drives[m]: the drive prices
    int b: the budget

Returns

    int: the maximum that can be spent, or 

    if it is not possible to buy both items

Input Format

The first line contains three space-separated integers
, , and , the budget, the number of keyboard models and the number of USB drive models.
The second line contains space-separated integers , the prices of each keyboard model.
The third line contains space-separated integers

, the prices of the USB drives.

Constraints

The price of each item is in the inclusive range

    .

Sample Input 0

10 2 3
3 1
5 2 8

Sample Output 0

9

Explanation 0

Buy the
keyboard and the USB drive for a total cost of

.

Sample Input 1

5 1 1
4
5

Sample Output 1

-1

Explanation 1

There is no way to buy one keyboard and one USB drive because
, so return .
*/

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

class Solution {

    /*
     * Complete the getMoneySpent function below.
     */
    static int getMoneySpent(int[] keyboards, int[] drives, int b) {
        // Brute force.
        int max = -1;
        foreach (int k in keyboards) {
            foreach (int d in drives) {
                if (k + d <= b && k + d > max)
                    max = k + d;
            }
        }
        return max;
    }

    static void Main(string[] args) {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        string[] bnm = Console.ReadLine().Split(' ');

        int b = Convert.ToInt32(bnm[0]);

        int n = Convert.ToInt32(bnm[1]);

        int m = Convert.ToInt32(bnm[2]);

        int[] keyboards = Array.ConvertAll(Console.ReadLine().Split(' '), keyboardsTemp => Convert.ToInt32(keyboardsTemp))
        ;

        int[] drives = Array.ConvertAll(Console.ReadLine().Split(' '), drivesTemp => Convert.ToInt32(drivesTemp))
        ;
        /*
         * The maximum amount of money she can spend on a keyboard and USB drive, or -1 if she can't purchase both items
         */

        int moneySpent = getMoneySpent(keyboards, drives, b);

        textWriter.WriteLine(moneySpent);

        textWriter.Flush();
        textWriter.Close();
    }
}
