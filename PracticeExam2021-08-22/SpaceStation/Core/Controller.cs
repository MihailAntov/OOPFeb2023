using SpaceStation.Core.Contracts;
using SpaceStation.Models.Astronauts;
using SpaceStation.Models.Astronauts.Contracts;
using SpaceStation.Models.Planets.Contracts;
using SpaceStation.Repositories;
using SpaceStation.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SpaceStation.Utilities.Messages;
using SpaceStation.Models.Planets;
using SpaceStation.Models.Mission.Contracts;
using SpaceStation.Models.Mission;

namespace SpaceStation.Core
{
    public class Controller : IController
    {
        IRepository<IAstronaut> astronauts;
        IRepository<IPlanet> planets;
        private string[] astronautTypes =
        {
            nameof(Biologist),
            nameof(Meteorologist),
            nameof(Geodesist)
        };
        private int planetsExplored = 0;

        public Controller()
        {
            astronauts = new AstronautRepository();
            planets = new PlanetRepository();
        }


        
        public string AddAstronaut(string type, string astronautName)
        {
            if(!astronautTypes.Contains(type))
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautType);
            }

            IAstronaut astronaut = null!;
            switch(type)
            {
                case nameof(Biologist):
                    astronaut = new Biologist(astronautName);
                    break;

                case nameof(Meteorologist):
                    astronaut = new Meteorologist(astronautName);
                    break;

                case nameof(Geodesist):
                    astronaut = new Geodesist(astronautName);
                    break;
            }

            astronauts.Add(astronaut);
            return String.Format(OutputMessages.AstronautAdded, type, astronaut.Name);
        }

        public string AddPlanet(string planetName, params string[] items)
        {
            IPlanet planet = new Planet(planetName);
            foreach(string item in items)
            {
                planet.Items.Add(item);
            }
            planets.Add(planet);
            return String.Format(OutputMessages.PlanetAdded, planetName);
        }

        public string ExplorePlanet(string planetName)
        {
            var astronautsToExplore = astronauts.Models
                .Where(a => a.Oxygen > 60).ToList();

            if(astronautsToExplore.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAstronautCount);
            }

            IPlanet planet = planets.FindByName(planetName);
            IMission mission = new Mission();
            mission.Explore(planet, astronautsToExplore);

            int deadAstronauts = astronautsToExplore.Count(a => !a.CanBreath);
            planetsExplored++;
            return String.Format(OutputMessages.PlanetExplored,planetName, deadAstronauts);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{planetsExplored} planets were explored!");
            sb.AppendLine("Astronauts info:");
            foreach(var astronaut in astronauts.Models)
            {
                sb.AppendLine(astronaut.ToString());
            }
            return sb.ToString().TrimEnd();
        }

        public string RetireAstronaut(string astronautName)
        {
            IAstronaut astronaut = astronauts.FindByName(astronautName);
            if(astronaut == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.InvalidRetiredAstronaut,astronautName));
            }

            astronauts.Remove(astronaut);
            return String.Format(OutputMessages.AstronautRetired, astronautName);
        }
    }
}
