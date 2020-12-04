using System;
using System.Text.RegularExpressions;
using System.Linq;
using System.Collections.Generic;
using System.Text;

namespace mirrorWords
{
    class Program
    {
        static void Main(string[] args)
        {
            string pattern = @"(#|@)([A-Za-z]{3,})\1\1([A-Za-z]{3,})\1";
            Regex regex = new Regex(pattern);
            string input = Console.ReadLine();
            MatchCollection pairs = regex.Matches(input);
            List<string> mirrorWords = new List<string>();

            foreach (Match item in pairs)
            {
                string word1 = item.Groups[2].ToString();
                string word2 = item.Groups[3].ToString();
                string reversator = string.Empty;
                for (int i = word2.Length-1; i >= 0 ; i--)
                {
                    reversator += word2[i];
                }
                if(word1 == reversator)
                {
                    mirrorWords.Add(word1);
                    mirrorWords.Add(word2);
                }
            }
            if(pairs.Count > 0)
            {
                Console.WriteLine($"{pairs.Count} word pairs found!");
                List<string> outputMirrors = new List<string>();
                if(mirrorWords.Count > 0)
                {
                    Console.WriteLine($"The mirror words are:");
                    for (int i = 0; i < mirrorWords.Count; i+=2)
                    {
                        outputMirrors.Add($"{mirrorWords[i]} <=> {mirrorWords[i + 1]}");
                    }
                    Console.WriteLine(string.Join(", ", outputMirrors));
                }
                else
                {
                    Console.WriteLine($"No mirror words!");
                }

            }
            else
            {
                Console.WriteLine("No word pairs found!");
                Console.WriteLine($"No mirror words!");
            }
        }
    }
}
