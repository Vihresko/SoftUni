using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;
namespace _02Race
{
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> participants = new Dictionary<string, int>();
            string[] inputParticipans = Console.ReadLine().Split(", ");
            foreach (var participan in inputParticipans)
            {
                participants.Add(participan, 0);
            }
            string patternForName = @"[\W\d]";
            string patternForDistance = @"[\D]";
            Regex regexForName = new Regex(patternForName);
            Regex regexForDistance = new Regex(patternForDistance);
            string input = Console.ReadLine();
            while(input != "end of race")
            {
                string nameOfCurrent = regexForName.Replace(input, "");
                int distanceOfCurrent = int.Parse(regexForDistance.Replace(input, "").ToString());
                int sumOfDistance = 0;
                foreach (var digit in distanceOfCurrent.ToString())
                {
                    int currentDigit = int.Parse(digit.ToString());
                    sumOfDistance += currentDigit;
                }
                if (participants.ContainsKey(nameOfCurrent))
                {
                    participants[nameOfCurrent] += sumOfDistance;
                }
                input = Console.ReadLine();
            }
            int counter = 1;

            foreach (var participant in participants.OrderByDescending(x=> x.Value))
            {
                string tail = counter == 1 ? "st" : counter == 2 ? "nd" : "rd";
                Console.WriteLine($"{counter++}{tail} place: {participant.Key}");
                if(counter == 4)
                {
                    break;
                }
            }

        }
    }
}
