using EasterRaces.Models.Cars.Contracts;
using EasterRaces.Models.Drivers.Contracts;
using EasterRaces.Utilities.Consts;
using EasterRaces.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasterRaces.Models.Drivers
{
    public class Driver : IDriver
    {
        private string name;
        private ICar car;
        private int numberOfWins;

        public Driver(string name)
        {
            Name = name;
            numberOfWins = 0;
        }
        
        public string Name
        {
            get { return name; }
            private set
            {
                if(string.IsNullOrEmpty(value) || value.Length < 5)
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.InvalidName, value, ValidationConstants.MinDriverNameLength));
                }
                name = value;
            }
        }

        public ICar Car => car;

        public int NumberOfWins => numberOfWins;

        public bool CanParticipate => Car != null;

        public void AddCar(ICar car)
        {
            if(car == null)
            {
                throw new ArgumentNullException(ExceptionMessages.CarInvalid);
            }
            this.car = car;
        }

        public void WinRace()
        {
            numberOfWins++;
        }
    }
}
