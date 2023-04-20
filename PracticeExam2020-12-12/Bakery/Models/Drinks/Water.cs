using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Models.Drinks
{
    public class Water : Drink
    {
        private const decimal WaterPrice = 1.50M;
        public Water(string name, int portion, string brand) 
            : base(name, portion, WaterPrice, brand)
        {
        }
    }
}
