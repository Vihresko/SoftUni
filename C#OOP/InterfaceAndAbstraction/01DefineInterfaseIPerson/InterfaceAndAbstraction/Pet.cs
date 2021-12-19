using System;
using System.Collections.Generic;
using System.Text;

namespace PersonInfo
{
    public class Pet : IBirthable
    {
        public Pet(string name, string birthday)
        {
            Birthdate = birthday;
            Name = name;
        }
        public string Birthdate {get; private set;}

        public string Name { get; private set; }
    }
}
