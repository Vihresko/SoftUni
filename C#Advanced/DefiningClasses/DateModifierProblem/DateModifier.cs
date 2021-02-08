using System;
using System.Collections.Generic;
using System.Text;

namespace DateModifierProblem
{
     public static class DateModifier
    {
        public static int GetDiffrenceBetweanDates(string date1, string date2)
        {
            DateTime dataOne = DateTime.Parse(date1);
            DateTime dataTwo = DateTime.Parse(date2);
            TimeSpan diff = dataOne - dataTwo;
            return Math.Abs(diff.Days);
        }
    }
}
