using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easter.Models.Bunnies
{
    public class HappyBunny : Bunny
    {
        private const int HappyBunnyStartingEnergy = 100;

        public HappyBunny(string name) 
            : base(name, HappyBunnyStartingEnergy)
        {
        }
    }
}
