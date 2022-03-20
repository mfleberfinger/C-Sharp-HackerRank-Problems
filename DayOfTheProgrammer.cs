/*
Marie invented a Time Machine and wants to test it by time-traveling to visit Russia on the Day of the Programmer (the 256th day of the year) during a year in the inclusive range from 1700 to 2700.

From 1700 to 1917, Russia's official calendar was the Julian calendar; since 1919 they used the Gregorian calendar system. The transition from the Julian to Gregorian calendar system occurred in 1918, when the next day after January 31st was February 14th. This means that in 1918, February 14th was the 32nd day of the year in Russia.

In both calendar systems, February is the only month with a variable amount of days; it has 29 days during a leap year, and 28 days during all other years. In the Julian calendar, leap years are divisible by 4; in the Gregorian calendar, leap years are either of the following:

    Divisible by 400.
    Divisible by 4 and not divisible by 100.

Given a year,
, find the date of the 256th day of that year according to the official Russian calendar during that year. Then print it in the format dd.mm.yyyy, where dd is the two-digit day, mm is the two-digit month, and yyyy is

.

For example, the given
= 1984. 1984 is divisible by 4, so it is a leap year. The 256th day of a leap year after 1918 is September 12, so the answer is

.

Function Description

Complete the dayOfProgrammer function in the editor below. It should return a string representing the date of the 256th day of the year given.

dayOfProgrammer has the following parameter(s):

    year: an integer

Input Format

A single integer denoting year

.

Constraints

    1700 \le y \le 2700

Output Format

Print the full date of Day of the Programmer during year
in the format dd.mm.yyyy, where dd is the two-digit day, mm is the two-digit month, and yyyy is

.

Sample Input 0

2017

Sample Output 0

13.09.2017

Explanation 0

In the year

= 2017, January has 31 days, February has 28 days, March has 31 days, April has 30 days, May has 31 days, June has 30 days, July has 31 days, and August has 31 days. When we sum the total number of days in the first eight months, we get 31 + 28 + 31 + 30 + 31 + 30 + 31 + 31 = 243. Day of the Programmer is the 256th day, so then calculate 256 - 243 = 13 to determine that it falls on day 13 of the 9th month (September). We then print the full date in the specified format, which is 13.09.2017.

Sample Input 1

2016

Sample Output 1

12.09.2016

Explanation 1

Year

= 2016 is a leap year, so February has 29 days but all the other months have the same number of days as in 2017. When we sum the total number of days in the first eight months, we get 31 + 29 + 31 + 30 + 31 + 30 + 31 + 31 = 244. Day of the Programmer is the 256th day, so then calculate 256 - 244 = 12 to determine that it falls on day 12 of the 9th month (September). We then print the full date in the specified format, which is 12.09.2016.

Sample Input 2

1800

Sample Output 2

12.09.1800

Explanation 2

Since 1800 is leap year as per Julian calendar. Day lies on 12 September.
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
     * Complete the 'dayOfProgrammer' function below.
     *
     * The function is expected to return a STRING.
     * The function accepts INTEGER year as parameter.
     */

    public static string dayOfProgrammer(int year)
    {
        /* Can't use DateTime... Apparently, .NET's Julian and Gregorian calendars
            include some weird date changes of their own around 1917 and,
            probably among other things, the .NET Julian calendar doesn't give the
            result expected by this problem in 1917. It's also possible that the
            problem is oversimplifying and not calculating dates correctly.
        // We'll do this using the standard .NET time functionality.
        // It seems like the appropriate way to solve this problem in
        // "the real world" anyway.
        GregorianCalendar gregorian = new GregorianCalendar();
        JulianCalendar julian = new JulianCalendar();
        DateTime dateTime;
        
            if (year < 1918) {
                dateTime = new DateTime(year, 1, 1, julian);
                dateTime = dateTime.AddDays(255);
            }
            else if (year == 1918) {
                dateTime = new DateTime(year, 1, 1, gregorian);
                // Thirteen days were skipped when the transition from the
                // Julian to Gregorian calendar was made in Russia. Therefore,
                // we need to add 13 days to what would have been the 256th day
                //by the Gregorian calendar.
                dateTime = dateTime.AddDays(255 + 13);
            }
            else {
                dateTime = new DateTime(year, 1, 1, gregorian);
                dateTime = dateTime.AddDays(255);
            }

        
        return dateTime.ToString("dd.MM.yyyy"); */
        
        // Lengths of months in days.
        /*const int JAN = 31, FEB = 28, FEBLEAP = 29, MAR = 31, APR = 30, MAY = 31,
            JUN = 30, JUL = 31, AUG = 31, SEP = 30, OCT = 31, NOV = 30, DEC = 31;*/
        
        string date;
        
        if (year < 1918) {
            // Leap years.
            if (year % 4 == 0)
                date = "12.09." + year.ToString();
            // Normal years.
            else
                date = "13.09." + year.ToString();
        }
        else if (year == 1918) {
            date = "26.09.1918";
        }
        else {
            // Leap years.
            if ((year % 4 == 0  && year % 100 != 0) || year % 400 == 0)
                date = "12.09." + year.ToString();
            // Normal years.
            else
                date = "13.09." + year.ToString();
        }
        
        return date;
    }

}

class Solution
{
    public static void Main(string[] args)
    {
        TextWriter textWriter = new StreamWriter(@System.Environment.GetEnvironmentVariable("OUTPUT_PATH"), true);

        int year = Convert.ToInt32(Console.ReadLine().Trim());

        string result = Result.dayOfProgrammer(year);

        textWriter.WriteLine(result);

        textWriter.Flush();
        textWriter.Close();
    }
}
