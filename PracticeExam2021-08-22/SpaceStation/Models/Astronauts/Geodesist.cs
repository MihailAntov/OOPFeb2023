using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceStation.Models.Astronauts
{
    public class Geodesist : Astronaut
    {
        private const double GeodesistStartingOxyge = 50;
        public Geodesist(string name) 
            : base(name, GeodesistStartingOxyge)
        {
        }

       
    }
}
