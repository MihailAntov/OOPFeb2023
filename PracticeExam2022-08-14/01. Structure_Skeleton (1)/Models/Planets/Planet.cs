using PlanetWars.Models.MilitaryUnits;
using PlanetWars.Models.MilitaryUnits.Contracts;
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

namespace PlanetWars.Models.Planets
{
    public class Planet : IPlanet
    {
        private string name;
        private double budget;
        //private double militaryPower;

        private IRepository<IMilitaryUnit> army;
        private IRepository<IWeapon> weapons;
        

        public Planet(string name, double budget)
        {
            Name = name;
            Budget = budget;
            army = new UnitRepository();
            weapons = new WeaponRepository();
        }
        
        public string Name
        {
            get { return name; }
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidPlanetName);
                }
                name = value;   
            }
        }

        public double Budget
        {
            get { return budget; }
            private set
            {
                if(value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBudgetAmount);
                }
                budget = value; 
            }
        }

        public double MilitaryPower => CalculateMilitaryPower();

        private double CalculateMilitaryPower()
        {
            double total = Army.Sum(u => u.EnduranceLevel) + Weapons.Sum(w => w.DestructionLevel);
            if(Army.Any(u=>u.GetType() == typeof(AnonymousImpactUnit)))
            {
                total *= 1.3;
            }

            if(Weapons.Any(w=>w.GetType() == typeof(NuclearWeapon)))
            {
                total *= 1.45;
            }

            return Math.Round(total, 3);
        }
        public IReadOnlyCollection<IMilitaryUnit> Army => army.Models;

        public IReadOnlyCollection<IWeapon> Weapons => weapons.Models;

        public void AddUnit(IMilitaryUnit unit)
        {
            army.AddItem(unit);
        }

        public void AddWeapon(IWeapon weapon)
        {
            weapons.AddItem(weapon);
        }

        public string PlanetInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Planet: {Name}");
            sb.AppendLine($"--Budget: {Budget} billion QUID");
            string forcesString = Army.Any() ? string.Join(", ", Army.Select(u => u.GetType().Name)) : "No units";
            sb.AppendLine($"--Forces: {forcesString}");
            //sb.AppendLine($"");
            string equipmentString = Weapons.Any() ? string.Join(", ", Weapons.Select(w => w.GetType().Name)) : "No weapons";
            sb.AppendLine($"--Combat equipment: {equipmentString}");
            sb.AppendLine($"--Military Power: {MilitaryPower}");

            return sb.ToString().TrimEnd();
        }

        public void Profit(double amount)
        {
            Budget += amount;
        }

        public void Spend(double amount)
        {
            if(Budget < amount)
            {
                throw new InvalidOperationException(ExceptionMessages.UnsufficientBudget);
            }

            Budget -= amount;
        }

        public void TrainArmy()
        {
            foreach(var unit in army.Models)
            {
                unit.IncreaseEndurance();
            }
        }
    }
}
