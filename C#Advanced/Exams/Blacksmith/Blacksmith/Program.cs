namespace Blacksmith
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    class Program
    {
        static void Main(string[] args)
        {
            Dictionary<string, int> forgedWeapon = new Dictionary<string, int>();
            var steels = new Queue<int>();
            var carbons = new Stack<int>();

            Dictionary<int, Dictionary<string, int>> swords = GetTargetedSwords();
            CollectResources(steels,carbons);

            //StartCraft
            while (steels.Count > 0 && carbons.Count > 0)
            {
                int steel = steels.Peek();
                int carbon = carbons.Peek();
                forgedWeapon = swords.FirstOrDefault(s => s.Key == steel + carbon).Value;
               if(forgedWeapon == null)
                {
                    steels.Dequeue();
                    carbons.Push(carbons.Pop() + 5);
                }
                else
                {
                    steels.Dequeue();
                    carbons.Pop();
                    forgedWeapon[forgedWeapon.Keys.First()] += 1;
                }
            }

            //EndCraft
            PrintHowMuchSwordsAreForged(swords);
            PrintResourceStatus(steels, "Steel");
            PrintResourceStatus(carbons, "Carbon");
            PrintForgedSwords(swords);
        }

        private static void PrintHowMuchSwordsAreForged(Dictionary<int, Dictionary<string, int>> notes)
        {
            int result = 0;
            foreach (var item in notes)
            {
                result += item.Value.Values.First();
            }
            if (result > 0)
            {
                Console.WriteLine($"You have forged {result} swords.");
            }
            else
            {
                Console.WriteLine("You did not have enough resources to forge a sword.");
            }
        }

        private static void CollectResources(Queue<int> steels, Stack<int> carbons)
        {
            int[] steelInput = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
            foreach (var steel in steelInput)
            {
                steels.Enqueue(steel);
            }

            int[] carbonInput = Console.ReadLine().Split(" ").Select(int.Parse).ToArray();
            foreach (var carbon in carbonInput)
            {
                carbons.Push(carbon);
            }
        }

        private static Dictionary<int, Dictionary<string, int>> GetTargetedSwords()
        {
            Dictionary<int, Dictionary<string, int>> result = new Dictionary<int, Dictionary<string, int>>()
            {
                {70, new Dictionary<string, int>() {{"Gladius",0 }} },
                {80, new Dictionary<string, int>() { {"Shamshir",0 }} },
                {90, new Dictionary<string, int>() {{"Katana", 0 }} },
                {110, new Dictionary<string, int>()  {{"Sabre", 0 }} },
                {150,new Dictionary<string, int>() {{"Broadsword",0 }} } 
            };
            return result;
        }
        
        private static void PrintResourceStatus<T>(IEnumerable<T> resources, string resourceName)
        {
            if(resources.Count() <= 0)
            {
                Console.WriteLine($"{resourceName} left: none");
            }
            else
            {
                Console.Write($"{resourceName} left: ");
                Console.WriteLine(String.Join(", ", resources));
            }
           
        }

        private static void PrintForgedSwords(Dictionary<int, Dictionary<string, int>> swords)
        {
            foreach (var sword in swords.OrderBy(s=> s.Value.Keys.First()))
            {
                if(sword.Value.Any(s=> s.Value > 0))
                {
                    string swordName = sword.Value.Keys.First();
                    int countForged = sword.Value.Values.First();
                    Console.WriteLine($"{swordName}: {countForged}");
                }
            }
        }
    }
}
