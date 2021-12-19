using System;
using System.Collections.Generic;

namespace Animals
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            List<Animal> data = new List<Animal>();
            string input = string.Empty;
            while (true)
            {
                input = Console.ReadLine();
                if(input == "Beast!")
                {
                    break;
                }
                string type = input;

                input = Console.ReadLine();
                string[] cmds = input.Split(" ");
                string name = cmds[0];
                int age = int.Parse(cmds[1]);
                string gender = cmds[2];

                Animal animal = null;
                switch (type)
                {
                    case "Cat":
                        if(gender == "Tomcat")
                        {
                            animal = new Tomcat(name, age);
                        }
                        else if(gender == "Kitten")
                        {
                            animal = new Kitten(name, age);
                        }
                        else
                        {
                            animal = new Cat(name, age, gender);
                        }
                        break;
                    case "Dog":
                        animal = new Dog(name, age, gender);
                        break;
                    case "Frog":
                        animal = new Frog(name, age, gender);
                        break;
                }
                data.Add(animal);
            }

            foreach (var item in data)
            {
                Console.WriteLine(item.GetType().Name);
                Console.WriteLine($"{item.Name} {item.Age} {item.Gender}");
                item.ProduceSound();
            }
        }
    }
}
