using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetWars.Models.MilitaryUnits
{
    public class StormTroopers : MilitaryUnit
    {
        private const double StormTrooperCost = 2.5;
        public StormTroopers() : base(StormTrooperCost)
        {
        }
    }
}
