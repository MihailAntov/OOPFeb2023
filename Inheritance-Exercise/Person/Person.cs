using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Person
{
    public class Person
    {
        private int age;

        public Person( string name, int age)
        {
            Age = age;
            Name = name;
        }

        public int Age 
        {
            get
            {
                return age;
            }
            set
            {
                if(age >=0)
                {
                    age = value;
                }
            }
        }
        public string Name { get; set; }
        public override string ToString()
        {
            return $"Name: {Name}, Age: {Age}";
        }
    }
}
