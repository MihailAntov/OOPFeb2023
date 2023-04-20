using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects
{
    public class AsteriskFood : Food
    {
        private const char AsteriskFoodSymbol = '*';
        private const int AsteriskFoodPoints = 2;
        public AsteriskFood(Wall wall)
            : base(wall, AsteriskFoodSymbol, AsteriskFoodPoints)
        {
        }
    }
}
