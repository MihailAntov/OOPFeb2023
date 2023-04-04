using CarRacing.Core.Contracts;
using CarRacing.Models.Cars;
using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Maps;
using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers;
using CarRacing.Models.Racers.Contracts;
using CarRacing.Repositories;
using CarRacing.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using CarRacing.Utilities.Messages;

namespace CarRacing.Core
{
    public class Controller : IController
    {
        private IMap map;
        private IRepository<ICar> cars;
        private IRepository<IRacer> racers;
        private string[] allowedCarTypes = { nameof(TunedCar), nameof(SuperCar) };
        private string[] allowedRacerTypes = { nameof(ProfessionalRacer), nameof(StreetRacer) };

        public Controller()
        {
            map = new Map();
            cars = new CarRepository();
            racers = new RacerRepository();
        }
        
        public string AddCar(string type, string make, string model, string VIN, int horsePower)
        {
            if(!allowedCarTypes.Contains(type))
            {
                throw new ArgumentException(ExceptionMessages.InvalidCarType);
            }

            ICar car = null!;
            switch(type)
            {
                case nameof(SuperCar):
                    car = new SuperCar(make, model, VIN, horsePower);
                    break;

                case nameof(TunedCar):
                    car = new TunedCar(make, model, VIN, horsePower);
                    break;
            }

            cars.Add(car);
            return String.Format(OutputMessages.SuccessfullyAddedCar, make, model, VIN);
        }

        public string AddRacer(string type, string username, string carVIN)
        {
            ICar car = cars.Models.FirstOrDefault(c => c.VIN == carVIN);
            if(car == null)
            {
                throw new ArgumentException(ExceptionMessages.CarCannotBeFound);
            }
            
            if(!allowedRacerTypes.Contains(type))
            {
                throw new ArgumentException(ExceptionMessages.InvalidRacerType);
            }

            IRacer racer = null!;
            switch(type)
            {
                case nameof(ProfessionalRacer):
                    racer = new ProfessionalRacer(username, car);
                    break;
                case nameof(StreetRacer):
                    racer = new StreetRacer(username, car);
                    break;
            }

            racers.Add(racer);
            return String.Format(OutputMessages.SuccessfullyAddedRacer, username);
        }

        public string BeginRace(string racerOneUsername, string racerTwoUsername)
        {
            IRacer racerOne = racers.FindBy(racerOneUsername);
            if(racerOne == null)
            {
                throw new ArgumentException(String.Format(ExceptionMessages.RacerCannotBeFound, racerOneUsername));
            }

            IRacer racerTwo = racers.FindBy(racerTwoUsername);
            if (racerTwo == null)
            {
                throw new ArgumentException(String.Format(ExceptionMessages.RacerCannotBeFound, racerTwoUsername));
            }

            return map.StartRace(racerOne, racerTwo);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach (var racer in racers.Models
                .OrderByDescending(r=>r.DrivingExperience)
                .ThenBy(r=>r.Username))
            {
                sb.AppendLine(racer.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
