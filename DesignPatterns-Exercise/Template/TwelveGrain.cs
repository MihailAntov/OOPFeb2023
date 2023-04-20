using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    public class TwelveGrain : Bread
    {
        public override void Bake()
        {
            Console.WriteLine("Baking the 12 grain bread. (25 minutes) ");
        }

        public override void MixIngredients()
        {
            Console.WriteLine("Gathering ingredients for 12 grain bread");
        }
    }
}
