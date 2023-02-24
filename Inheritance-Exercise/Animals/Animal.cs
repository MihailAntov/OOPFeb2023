using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Animals
{
    public abstract class Animal
    {
        private string gender;
        private int age;
        public Animal(string name, int age, string gender)
        {
            Name = name;
            Age = age;
            Gender = gender;
        }

        public string Name { get; set; }
        public int Age
        {
            get
            {
                return age;
            }
            set
            {
                if(value < 0 )
                {
                    throw new Exception("Invalid input!");
                }

                age = value;
            }
        }
        public string Gender
        {
            get
            {
                return gender;
            }
            set
            {
                if(value != "Male" && value != "Female")
                {
                    throw new Exception("Invalid input!");
                }

                gender = value;
            }
        }

        public abstract string ProduceSound();
        public override string ToString()
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine(this.GetType().Name);
            str.AppendLine($"{Name} {Age} {Gender}");
            str.AppendLine(this.ProduceSound());
            return str.ToString().TrimEnd();
            
        }

    }
}
