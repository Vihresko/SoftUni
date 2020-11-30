using System;
using System.Text.RegularExpressions;

namespace _03SoftUniBarIncome
{
    class Program
    {
        static void Main(string[] args)
        {
            string input = string.Empty;
            double totalMoney = 0;
            string name = string.Empty;
            string product = string.Empty;
            int quantity = 0;
            double price = 0;

            string pattern = @"%([A-Z][a-z]+)%[^|$%.]*<(\w+)>[^|$%.]*\|(\d+)\|[^|$%.]*?(\d+\.?[\d]+)\$";
            Regex regex = new Regex(pattern);



            while((input = Console.ReadLine()) != "end of shift")
            {
                Match currentOrderInfo = regex.Match(input);
                if (currentOrderInfo.Success)
                {
                    name = currentOrderInfo.Groups[1].Value;
                    product = currentOrderInfo.Groups[2].Value;
                    quantity = int.Parse(currentOrderInfo.Groups[3].Value);
                    price = double.Parse(currentOrderInfo.Groups[4].Value);
                    Console.WriteLine($"{name}: {product} - {(quantity*price):f2}");
                    totalMoney += quantity * price;
                }
            }
            Console.WriteLine($"Total income: {totalMoney:f2}");
        }
    }
}
