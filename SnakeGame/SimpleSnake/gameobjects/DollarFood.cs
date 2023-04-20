using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects
{
    public class DollarFood : Food
    {
        private const char DollarFoodSymbol = '$';
        private const int DollarFoodPoints = 1;
        public DollarFood(Wall wall) : base(wall, DollarFoodSymbol, DollarFoodPoints)
        {
        }
    }
}
