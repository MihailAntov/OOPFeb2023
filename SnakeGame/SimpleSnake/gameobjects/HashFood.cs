using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimpleSnake.GameObjects
{
    public class HashFood : Food
    {
        private const char HashFoodSymbol = '#';
        private const int HashFoodPoints = 3;
        public HashFood(Wall wall) 
            : base(wall, HashFoodSymbol, HashFoodPoints)
        {
        }
    }
}
