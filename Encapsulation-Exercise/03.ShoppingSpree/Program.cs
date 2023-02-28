
using System;
using System.Collections.Generic;
using System.Linq;
namespace _03.ShoppingSpree
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<Person> people = new List<Person>();
            List<Product> products = new List<Product>();

            string[] peopleArgs = Console.ReadLine()
                .Split(";", StringSplitOptions.RemoveEmptyEntries);

            foreach(string arg in peopleArgs)
            {
                string[] personArgs = arg.Split("=", StringSplitOptions.RemoveEmptyEntries);
                string name = personArgs[0];
                decimal money = decimal.Parse(personArgs[1]);

                try
                {
                    Person person = new Person(name, money);
                    people.Add(person);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }
                
            }

            string[] productsArgs = Console.ReadLine()
                .Split(";", StringSplitOptions.RemoveEmptyEntries);

            foreach (string arg in productsArgs)
            {
                string[] productArgs = arg.Split("=", StringSplitOptions.RemoveEmptyEntries);
                string name = productArgs[0];
                decimal cost = decimal.Parse(productArgs[1]);

                try
                {
                    Product product = new Product(name, cost);
                    products.Add(product);
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                    return;
                }

            }

            string input;

            while((input = Console.ReadLine())!= "END")
            {
                string[] purchaseArgs = input.Split(" ", StringSplitOptions.RemoveEmptyEntries);
                Person person = people.FirstOrDefault(p => p.Name == purchaseArgs[0]);
                Product product = products.FirstOrDefault(p => p.Name == purchaseArgs[1]);

                Console.WriteLine(person.Buy(product));
            }

            foreach(Person person in people)
            {
                Console.WriteLine(person);
            }




        }
    }
}