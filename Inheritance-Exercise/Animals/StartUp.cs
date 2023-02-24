using System;
using System.Reflection;
using System.Collections.Generic;
namespace Animals
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            string input;
            List<Animal> animals = new List<Animal>();
            while((input = Console.ReadLine()) != "Beast!")
            {
                try
                {
                    string[] animalArgs = Console.ReadLine()
                                        .Split(" ", StringSplitOptions.RemoveEmptyEntries);
                    string name = animalArgs[0];
                    int age = int.Parse(animalArgs[1]);
                    string gender = animalArgs[2];

                    string typeName = $"Animals.{input}, Animals, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null";
                    Type type = Type.GetType(typeName);

                    var animal = Activator.CreateInstance(type,
                        name, age, gender);

                    animals.Add((Animal)animal);
                }
                catch 
                {
                    Console.WriteLine("Invalid input!");
                }
                


            }

            foreach(Animal animal in animals)
            {
                Console.WriteLine(animal.ToString());
            }

            
        }
    }
}
