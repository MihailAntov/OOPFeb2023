using System;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
namespace BorderControl
{
    public class StartUp
    {
        public static void Main(string[] args)
        {
            int n = int.Parse(Console.ReadLine());

            
            
            
            List<IBuyer> buyers = new List<IBuyer>();

            for(int i = 0; i < n; i++)
            {
                string[] inputArgs = Console.ReadLine().Split(" ", StringSplitOptions.RemoveEmptyEntries);

                string name = inputArgs[0];
                int age = int.Parse(inputArgs[1]);

                if(inputArgs.Length == 3)
                {
                    string group = inputArgs[2];
                    Rebel rebel = new Rebel(name, age, group);
                    buyers.Add(rebel);
                }
                else
                {
                    string id = inputArgs[2];
                    DateTime birthdate = DateTime.ParseExact(inputArgs[3],"dd/MM/yyyy",CultureInfo.InvariantCulture);
                    Citizen citizen = new Citizen(name, age, id, birthdate);
                    buyers.Add(citizen);
                }
            }
            string input;
            while ((input = Console.ReadLine())!= "End")
            {
                IBuyer buyer = buyers.FirstOrDefault(b => b.Name == input);
                if(buyer == null)
                {
                    continue;
                }
                buyer.BuyFood();
            }

            Console.WriteLine(buyers.Sum(b=>b.Food));

            
        }
    }
}