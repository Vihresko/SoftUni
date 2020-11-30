using System;
using System.Collections.Generic;
using System.Numerics;
using System.Linq;
using System.Text.RegularExpressions;

namespace EmojiDetector
{
    class Program
    {
        static void Main(string[] args)
        {
            string patternForRegex = @"([:|*])\1[A-Z][a-z]{2,}\1{2}";
            Regex regex = new Regex(patternForRegex);

            List<string> library = new List<string>();

            string inputString = Console.ReadLine();
            BigInteger coolThreshold = 1;
            foreach (var item in inputString)
            {
                if (char.IsDigit(item))
                {
                    coolThreshold = coolThreshold * int.Parse(item.ToString());
                }
            }
            Console.WriteLine($"Cool threshold: {coolThreshold}");
            var potentionalEmojies = regex.Matches(inputString).ToList();
            foreach (var item in potentionalEmojies)
            {
             library.Add(item.ToString());
            }

            List<string> cooliesEmojies = new List<string>();
            foreach (var okEmoji in library)
            {
                if(GetThresOnEmoji(okEmoji) > coolThreshold)
                {
                    cooliesEmojies.Add(okEmoji);
                }
            }

            Console.WriteLine($"{library.Count} emojis found in the text. The cool ones are:");
            foreach (var cool in cooliesEmojies)
            {
                Console.WriteLine(cool);
            }
        }

        public static BigInteger GetThresOnEmoji(string _input)
        {
            BigInteger result = 1;
            foreach (var character in _input)
            {
                if(character != ':' && character != '*')
                {
                    int currentValue = (int)character;
                    result = result + currentValue;
                }
            }
            return result;
        }
        
    }
}
