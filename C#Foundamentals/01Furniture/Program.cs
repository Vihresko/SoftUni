using System;
using System.Text.RegularExpressions;

namespace _01Furniture
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = Console.ReadLine();
            string pattern = @">>([a-zA-Z]+)<<(\d+\.?\d*)!(\d+)";
            Regex checker = new Regex(pattern);
            
            double totalSum = 0.00;
            Console.WriteLine("Bought furniture:");
            while(input != "Purchase")
            {
                Match match = checker.Match(input);
                if (match.Success)
                {
                    string furniture = match.Groups[1].Value;
                    double prize = double.Parse(match.Groups[2].Value);
                    int quantity = int.Parse(match.Groups[3].Value);
                    totalSum += prize * quantity;
                    Console.WriteLine(furniture);
                }
                input = Console.ReadLine();
            }
            Console.WriteLine($"Total money spend: {totalSum:f2}");
        }
    }
}
