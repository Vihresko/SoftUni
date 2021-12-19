using System;
using System.Collections.Generic;
using System.Text;

namespace Animals
{
    public abstract class Animal
    {
        string name;
        int age;
        string gender;
        public Animal(string name, int age, string gender)
        {
            this.Name = name;
            this.Age = age;
            this.Gender = gender;
        }
        public string Name 
        {
            get => this.name;
            set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    Console.WriteLine("Invalid input");
                }
                this.name = value;
            } 
        }
        public int Age
        {
            get { return this.age; } 
            set
            {
                if (value <= 0)
                {
                   Console.WriteLine( new Exception("Invalid input"));
                }
                this.age = value;
            }
        }
        public string Gender
        {
            get { return this.gender; }
            set
            {
                if (value == "")
                {
                    Console.WriteLine("Invalid input");
                }
                this.gender = value;
            }
        }

        public abstract void ProduceSound();
        
    }
}
