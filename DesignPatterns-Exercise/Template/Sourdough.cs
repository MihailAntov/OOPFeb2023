using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    public class Sourdough : Bread
    {
        public override void Bake()
        {
            Console.WriteLine("Baking the sourdough bread. (20 minutes) ");
        }

        public override void MixIngredients()
        {
            Console.WriteLine("Gathering ingredients sourdough bread");
        }
    }
}
