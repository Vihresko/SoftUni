using System;
using System.Collections.Generic;
using System.Text;

namespace PersonInfo
{
    public class Rebel : IBuyer
    {
        public string Name { get; set; }
        public int Age{ get; set; }

        public string Group { get; set; }

        public Rebel(string name,int age, string group)
        {
            Name = name;
            Age = age;
            Group = group;
        }
        public int Food
        {
            get;
            private set;
        }

        public int BuyFood()
        {
            return 5;
        }
    }
}
