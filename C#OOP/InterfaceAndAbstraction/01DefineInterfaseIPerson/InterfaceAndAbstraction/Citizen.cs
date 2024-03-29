﻿namespace PersonInfo
{
    public class Citizen : IPerson
    {
        public Citizen(string name, int age, string id, string birthdate)
        {
            Name = name;
            Age = age;
            Id = id;
            Birthdate = birthdate;
        }

        public Citizen(string name, int age, string id)
        {
            Name = name;
            Age = age;
            Id = id;
        }
        public string Name { get; private set; }
        
        public int Age { get; private set; }
        

        public string Id { get; private set; }
       

        public string Birthdate { get; private set; }

        public int Food { get; private set; }

        public int BuyFood()
        {
            return 10;
        }
    }
}
