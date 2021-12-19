using System;
using System.Collections.Generic;

namespace PersonInfo
{
    public class StartUp
    {
        static void Main(string[] args)
        {
            List<IBuyer> buyers = new List<IBuyer>();
            int n = int.Parse(Console.ReadLine());

            for (int i = 0; i < n; i++)
            {
                var inputMembers = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string name = inputMembers[0];
                int age = int.Parse(inputMembers[1]);

                if(inputMembers.Length > 3)
                {
                    string id = inputMembers[2];
                    string birthDay = inputMembers[3];
                    buyers.Add(new Citizen(name, age, id));
                }
                else
                {
                    string group = inputMembers[2];
                    buyers.Add(new Rebel(name, age, group));
                }
            }

            int totalFood = 0;

            while (true)
            {
                string inputName = Console.ReadLine();
                if(inputName == "End")
                {
                    break;
                }

                foreach (var item in buyers)
                {
                    if(item is Citizen)
                    {
                        string name = ((Citizen)item).Name;
                        if(name == inputName)
                        {
                            totalFood += item.BuyFood();
                        }
                    }
                    else if(item is Rebel)
                    {
                        string name = ((Rebel)item).Name;
                        if (name == inputName)
                        {
                            totalFood += item.BuyFood();
                        }
                    }
                    
                }
            }

            Console.WriteLine(totalFood);

        }
    }
}
