using System;
using System.Collections.Generic;
using System.Text;

namespace Person
{
    public class Person
    {
        string name;
        int age;

        public string Name { get; set; }
        public int Age { get; set; }
        
        public Person(string name, int age)
        {
            Name = name;
            Age = age;
        }

        public override string ToString()
        {
            StringBuilder print = new StringBuilder();
            print.Append(String.Format("Name: {0}, Age: {1}", this.Name, this.Age));
            return print.ToString();
        }
    }
}
