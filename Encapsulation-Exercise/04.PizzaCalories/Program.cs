using System;



namespace _04.PizzaCalories;

public class Program
{
    static void Main(string[] args)
    {

        try
        {

            string[] pizzaArgs = Console.ReadLine()
            .Split(" ");
            string pizzaName = pizzaArgs[1];

            Pizza pizza = new Pizza(pizzaName);

            string input;

            while ((input = Console.ReadLine()) != "END")
            {


                string[] inputArgs = input
                    .Split(" ", StringSplitOptions.RemoveEmptyEntries);


                string type = inputArgs[0];

                if (type == "Dough")
                {
                    string flourType = inputArgs[1];
                    string technqiue = inputArgs[2];
                    double weight = double.Parse(inputArgs[3]);
                    Dough dough = new Dough(flourType, technqiue, weight);
                    pizza.SetDough(dough);
                    
                }
                else if (type == "Topping")
                {
                    string toppingType = inputArgs[1];
                    double weight = double.Parse(inputArgs[2]);
                    Topping topping = new Topping(toppingType, weight);
                    pizza.AddTopping(topping);
                    
                }

            }

            Console.WriteLine($"{pizza.Name} - {pizza.TotalCalories:f2} Calories.");


        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }

}

