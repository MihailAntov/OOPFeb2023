using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easter.Models.Bunnies
{
    public class SleepyBunny : Bunny
    {
        private const int SleppyBunnyStartingEnergy = 50;

        public SleepyBunny(string name) 
            : base(name, SleppyBunnyStartingEnergy)
        {
        }
        public override void Work()
        {
            Energy -= 15;
        }
    }
}
