using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Template
{
    public class WholeWheat : Bread
    {
        public override void Bake()
        {
            Console.WriteLine("Baking the whole wheat bread. (25 minutes) ");
        }

        public override void MixIngredients()
        {
            Console.WriteLine("Gathering ingredients for whole wheat bread");
        }
    }
}
