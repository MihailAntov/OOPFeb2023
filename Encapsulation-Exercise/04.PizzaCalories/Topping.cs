using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.PizzaCalories
{
    public class Topping
    {
        private string toppingType;
        private double weight;
        private const int baseCaloriesPerGram = 2;
        private static Dictionary<string, double> modifiers = new Dictionary<string, double>
            {
                {"meat",1.2 },
                {"veggies",0.8 },
                {"cheese",1.1 },
                {"sauce",0.9 },
            };

        public Topping(string toppingType, double weight)
        {
            ToppingType = toppingType;
            Weight = weight;
        }

        private string ToppingType
        {
            get { return toppingType; }
            set
            {
                if (!modifiers.ContainsKey(value.ToLower()))
                {
                    throw new ArgumentException($"Cannot place {value} on top of your pizza.");
                }

                toppingType = value;
            }
        }

        private double Weight
        {
            get { return weight; }
            set
            {
                if (value < 1 || value > 50)
                {
                    throw new ArgumentException($"{ToppingType} weight should be in the range[1..50].");
                }

                weight = value;
            }
        }

        public double Calories
        {
            get
            {
                double caloriesPerGram = baseCaloriesPerGram * modifiers[toppingType.ToLower()];
                return caloriesPerGram * weight;
            }
        }
    }
}
