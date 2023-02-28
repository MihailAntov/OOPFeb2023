﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _04.PizzaCalories
{
    public class Pizza
    {
        private string name;

        private Dough dough;

        private List<Topping> toppings;

        public Pizza(string name)
        {
            Name = name;
            toppings = new List<Topping>();
        }
        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length > 15)
                {
                    throw new ArgumentException("Pizza name should be between 1 and 15 symbols.");
                }
                name = value;
            }
        }

        public void AddTopping(Topping topping)
        {
            if (ToppingCount >= 10)
            {
                throw new InvalidOperationException("Number of toppings should be in range [0..10].");
            }
            toppings.Add(topping);
        }

        public void SetDough(Dough dough)
        {
            this.Dough = dough;
        }

        private Dough Dough { get { return dough; } set { dough = value; } }

        public int ToppingCount { get { return toppings.Count; } }
        public double TotalCalories { 
            get
            {
                return dough.Calories + toppings.Select(t => t.Calories).Sum();
            } 
        }

        
    }
}
