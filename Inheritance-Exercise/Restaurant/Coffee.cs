﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Restaurant
{
    public class Coffee : HotBeverage
    {
        private const double coffeeMililiters = 50;
        private const decimal coffeePrice = 3.50M;
        public Coffee(string name, double caffeine) : base(name, coffeePrice, coffeeMililiters)  
        {
            Caffeine = caffeine;
            
        }

        
        
        public double Caffeine { get; set; }
    }
}
