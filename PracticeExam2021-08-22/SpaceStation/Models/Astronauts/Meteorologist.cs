using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceStation.Models.Astronauts
{
    public class Meteorologist : Astronaut
    {
        private const double MeteorologistStartingOxygen = 90;
        public Meteorologist(string name) 
            : base(name, MeteorologistStartingOxygen)
        {
        }
    }
}
