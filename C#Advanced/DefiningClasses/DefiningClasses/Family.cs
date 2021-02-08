using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DefiningClasses
{
     public class Family
    {
        //fields

        //constructor
        public Family()
        {
            this.Members = new List<Person>();
        }

        //prop
        public List<Person> Members { get; set; }

        //methods
        public void AddMember(Person person)
        {
            this.Members.Add(person);
        }

        public Person GetOldestMember()
        {
            Person oldest = this.Members.OrderByDescending(a => a.Age).FirstOrDefault();
            return oldest;
        }
        //private methods
       
    }
}
