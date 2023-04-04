using PlanetWars.Core.Contracts;
using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
using PlanetWars.Models.Planets;
using PlanetWars.Models.Planets.Contracts;
using PlanetWars.Models.Weapons;
using PlanetWars.Models.Weapons.Contracts;
using PlanetWars.Repositories;
using PlanetWars.Repositories.Contracts;
using PlanetWars.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PlanetWars.Core
{
    public class Controller : IController
    {
        private IRepository<IPlanet> planets;
        private string[] allowedUnitTypes = { nameof(StormTroopers), nameof(SpaceForces), nameof(AnonymousImpactUnit) };
        private string[] allowedWeaponTypes = { nameof(NuclearWeapon), nameof(BioChemicalWeapon), nameof(SpaceMissiles) };
        public Controller()
        {
            planets = new PlanetRepository();
        }
        
        public string AddUnit(string unitTypeName, string planetName)
        {
            IPlanet planet = planets.FindByName(planetName);
            if(planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if (!allowedUnitTypes.Contains(unitTypeName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, unitTypeName));
            }

            if(planet.Army.Any(u=> u.GetType().Name == unitTypeName))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.UnitAlreadyAdded, unitTypeName, planetName));
            }

            IMilitaryUnit unit = null!;
            switch(unitTypeName)
            {
                case nameof(StormTroopers):
                    unit = new StormTroopers();
                    break;

                case nameof(SpaceForces):
                    unit = new SpaceForces();
                    break;

                case nameof(AnonymousImpactUnit):
                    unit = new AnonymousImpactUnit();
                    break;
            }

            planet.AddUnit(unit);
            planet.Spend(unit.Cost);

            return String.Format(OutputMessages.UnitAdded, unitTypeName, planetName);


        }

        public string AddWeapon(string planetName, string weaponTypeName, int destructionLevel)
        {
            IPlanet planet = planets.FindByName(planetName);
            if(planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if(!allowedWeaponTypes.Contains(weaponTypeName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.ItemNotAvailable, weaponTypeName));
            }

            if(planet.Weapons.Any(w=> w.GetType().Name == weaponTypeName))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.WeaponAlreadyAdded, weaponTypeName, planetName));
            }

            IWeapon weapon = null!;
            switch(weaponTypeName)
            {
                case nameof(BioChemicalWeapon):
                    weapon = new BioChemicalWeapon(destructionLevel);
                    break;

                case nameof(NuclearWeapon):
                    weapon = new NuclearWeapon(destructionLevel);
                    break;

                case nameof(SpaceMissiles):
                    weapon = new SpaceMissiles(destructionLevel);
                    break;
            }

            planet.AddWeapon(weapon);
            planet.Spend(weapon.Price);

            return String.Format(OutputMessages.WeaponAdded, planetName, weaponTypeName);
        }

        public string CreatePlanet(string name, double budget)
        {
            if(planets.Models.Any(p=>p.Name == name))
            {
                return String.Format(OutputMessages.ExistingPlanet, name);
            }
            IPlanet planet = new Planet(name, budget);
            planets.AddItem(planet);
            return String.Format(OutputMessages.NewPlanet, planet.Name);
        }

        public string ForcesReport()
        {
            StringBuilder sb = new StringBuilder();

            sb.AppendLine("***UNIVERSE PLANET MILITARY REPORT***");
            foreach (var planet in planets.Models
                .OrderByDescending(p=>p.MilitaryPower)
                .ThenBy(p=>p.Name))
            {
                sb.AppendLine(planet.PlanetInfo());
            }

            return sb.ToString().TrimEnd();
        }

        public string SpaceCombat(string planetOne, string planetTwo)
        {
            IPlanet first = planets.FindByName(planetOne);
            IPlanet second = planets.FindByName(planetTwo);

            if(first.MilitaryPower == second.MilitaryPower)
            {
                bool firstHasNuclearWeapons = first.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon));
                bool secondHasNuclearWeapons = second.Weapons.Any(w => w.GetType().Name == nameof(NuclearWeapon));

                if(firstHasNuclearWeapons  == secondHasNuclearWeapons)
                {
                    first.Spend(first.Budget / 2);
                    second.Spend(second.Budget / 2);
                    return OutputMessages.NoWinner;
                }
                else if(firstHasNuclearWeapons)
                {
                    //first wins
                    return WarResults(first, second);
                }
                else
                {
                    //second wins
                    return WarResults(second, first);
                }

            }
            
            if(first.MilitaryPower > second.MilitaryPower)
            {
                //first wins
                return WarResults(first, second);
            }
            else
            {
                //second wins
                return WarResults(second, first);
            }
        }

        private string WarResults(IPlanet winner, IPlanet loser)
        {
            winner.Spend(winner.Budget / 2);
            winner.Profit(loser.Budget / 2);
            winner.Profit(loser.Weapons.Sum(w=>w.Price) + loser.Army.Sum(u=>u.Cost));
            planets.RemoveItem(loser.Name);
            return String.Format(OutputMessages.WinnigTheWar, winner.Name, loser.Name);
        }

        



        public string SpecializeForces(string planetName)
        {
            IPlanet planet = planets.FindByName(planetName);
            if (planet == null)
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.UnexistingPlanet, planetName));
            }

            if(!planet.Army.Any())
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.NoUnitsFound));
            }

            planet.Spend(1.25);
            planet.TrainArmy();

            return String.Format(OutputMessages.ForcesUpgraded, planetName);
        }
    }
}
