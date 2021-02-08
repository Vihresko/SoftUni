using System;

namespace DateModifierProblem
{
    class Program
    {
        static void Main(string[] args)
        {
            string firstDate = Console.ReadLine();
            string secondDate = Console.ReadLine();
            int result = DateModifier.GetDiffrenceBetweanDates(firstDate, secondDate);
            Console.WriteLine(result);
        }
    }
}
