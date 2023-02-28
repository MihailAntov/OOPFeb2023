using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PizzaCalories
{
    public class Dough
    {
        private string flourType;
        private string technique;
        private double weight;
        private const int baseCaloriesPerGram = 2;
        private static Dictionary<string, double> modifiers = new Dictionary<string, double>
            {
                {"white",1.5 },
                {"wholegrain",1.0 },
                {"crispy",0.9 },
                {"chewy",1.1 },
                {"homemade",1.0 }
            };

        public Dough(string flourType, string technique, double weight)
        {
            FlourType = flourType;
            Technique = technique;
            Weight = weight;
        }

        public string FlourType
        {
            get { return flourType; }
            private set
            {
                if (value.ToLower() != "white" && value.ToLower() != "wholegrain")
                {
                    throw new ArgumentException("Invalid type of dough.");
                }

                flourType = value;
            }
        }
        public string Technique
        {
            get { return technique; }
            private set
            {
                if (value.ToLower() != "crispy" && value.ToLower() != "chewy" && value.ToLower() != "homemade")
                {
                    throw new ArgumentException("Invalid type of dough.");
                }

                technique = value;
            }
        }
        public double Weight
        {
            get { return weight; }
            private set
            {
                if (value < 1 || value > 200)
                {
                    throw new ArgumentException("Dough weight should be in the range [1..200].");
                }

                weight = value;
            }
        }

        public double Calories
        {
            get
            {
                double caloriesPerGram = baseCaloriesPerGram * modifiers[FlourType.ToLower()] * modifiers[Technique.ToLower()];
                return caloriesPerGram * weight;
            }
        }


    }
}
