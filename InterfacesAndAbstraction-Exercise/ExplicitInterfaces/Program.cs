using System;

namespace ExplicitInterfaces
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string input; 

            while((input = Console.ReadLine())!= "End")
            {
                string[] inputArgs = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                string name = inputArgs[0];
                string country = inputArgs[1];
                int age = int.Parse(inputArgs[2]);

                Citizen citizen = new Citizen(name, country, age);
                IPerson person = citizen;
                IResident resident = citizen;
                Console.WriteLine(person.GetName());
                Console.WriteLine(resident.GetName());

            }
        }
    }
}