using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceStation.Models.Astronauts
{
    public class Biologist : Astronaut
    {
        private const double BiologistStartingOxygen = 70; 
        public Biologist(string name) 
            : base(name, BiologistStartingOxygen)
        {
        }

        public override void Breath()
        {
            Oxygen -= 5;
        }
    }
}
