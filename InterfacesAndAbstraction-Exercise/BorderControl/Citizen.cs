using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BorderControl
{
    public class Citizen : ICheckable, IBirthable, IBuyer
    {
        public Citizen(string name, int age, string id, DateTime birthdate)
        {
            Name = name;
            Age = age;
            Id = id;
            Birthdate = birthdate;
            Food = 0;
        }

        public string Name { get; set; }
        public int Age { get; set; }
        public string Id { get; set; }
        public DateTime Birthdate { get; set; }

        public string Credentials =>  Id;

        public int Food { get; set; }

        public void BuyFood()
        {
            Food += 10;
        }

        public bool CheckId(string lastDigits)
        {
            int checkLength = lastDigits.Length;

            string currentLastDigits = string.Join("",Id.TakeLast(checkLength));
            return currentLastDigits == lastDigits;
        }
    }
}
