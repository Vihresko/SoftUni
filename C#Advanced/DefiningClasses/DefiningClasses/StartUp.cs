using System;
using System.Collections.Generic;
using System.Linq;

namespace DefiningClasses
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());
            List<Person> people = new List<Person>();

            for (int i = 0; i < n; i++)
            {
                string[] input = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string name = input[0];
                int age = int.Parse(input[1]);
                Person currentPerson = new Person(name, age);

                people.Add(currentPerson);
            }
            people = people.OrderBy(n => n.Name).Where(n => n.Age > 30).ToList();
            foreach (var person in people)
            {
                Console.WriteLine($"{person.Name} - {person.Age}");
            }
        }
    }
}
