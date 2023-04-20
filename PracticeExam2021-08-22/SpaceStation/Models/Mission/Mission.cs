using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Planets.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpaceStation.Models.Mission
{
    public class Mission : IMission
    {
        public void Explore(IPlanet planet, ICollection<IAstronaut> astronauts)
        {
            foreach(var astronaut in astronauts)
            {
                while(astronaut.Oxygen > 0)
                {
                    string item = planet.Items.FirstOrDefault();
                    if(item == null)
                    {
                        return;
                    }

                    astronaut.Bag.Items.Add(item);
                    planet.Items.Remove(item);
                    astronaut.Breath();
                }
            }
        }
    }
}
