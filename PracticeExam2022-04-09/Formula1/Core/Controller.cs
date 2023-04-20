using Formula1.Core.Contracts;
using Formula1.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Formula1.Utilities;
using Formula1.Models.Contracts;
using Formula1.Models;

namespace Formula1.Core
{
    public class Controller : IController
    {
        private PilotRepository pilotRepository;
        private FormulaOneCarRepository formulaOneCarRepository;
        private RaceRepository raceRepository;


        private string[] allowedCarTypes = { "Ferrari", "Williams" };
        public Controller()
        {
            pilotRepository = new PilotRepository();
            formulaOneCarRepository = new FormulaOneCarRepository();
            raceRepository = new RaceRepository();
        }
        
        public string AddCarToPilot(string pilotName, string carModel)
        {
            IPilot pilot = pilotRepository.FindByName(pilotName);
            if(pilot == null || pilot.Car != null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.PilotDoesNotExistOrHasCarErrorMessage, pilotName));
            }

            IFormulaOneCar car = formulaOneCarRepository.FindByName(carModel);
            if(car == null)
            {
                throw new NullReferenceException(String.Format(ExceptionMessages.CarDoesNotExistErrorMessage, carModel));
            }

            pilot.AddCar(car);
            formulaOneCarRepository.Remove(car);
            return String.Format(OutputMessages.SuccessfullyPilotToCar, pilotName, car.GetType().Name, carModel);
        }

        public string AddPilotToRace(string raceName, string pilotFullName)
        {
            IRace race = raceRepository.FindByName(raceName);
            if(race == null)
            {
                throw new NullReferenceException(String.Format(ExceptionMessages.RaceDoesNotExistErrorMessage, raceName));
            }

            IPilot pilot = pilotRepository.FindByName(pilotFullName);

            if(pilot == null || !pilot.CanRace || race.Pilots.Contains(pilot))
            {
                throw new InvalidOperationException(string.Format(ExceptionMessages.PilotDoesNotExistErrorMessage, pilotFullName));
            }

            race.AddPilot(pilot);
            return String.Format(OutputMessages.SuccessfullyAddPilotToRace, pilotFullName, raceName);
        }

        public string CreateCar(string type, string model, int horsepower, double engineDisplacement)
        {
            if(formulaOneCarRepository.Models.Any(c=> c.Model == model))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.CarExistErrorMessage, model));
            }

            if(!allowedCarTypes.Contains(type))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.InvalidTypeCar, type));
            }

            IFormulaOneCar car = null!;
            switch(type)
            {
                case nameof(Ferrari):
                    car = new Ferrari(model, horsepower, engineDisplacement);
                    break;
                case nameof(Williams):
                    car = new Williams(model, horsepower, engineDisplacement);
                    break;
            }

            formulaOneCarRepository.Add(car);
            return String.Format(OutputMessages.SuccessfullyCreateCar, type, model);
        }

        public string CreatePilot(string fullName)
        {
            if(pilotRepository.Models.Any(p=>p.FullName == fullName))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.PilotExistErrorMessage, fullName));
            }

            IPilot pilot = new Pilot(fullName);
            pilotRepository.Add(pilot);

            return String.Format(OutputMessages.SuccessfullyCreatePilot, fullName);
        }

        public string CreateRace(string raceName, int numberOfLaps)
        {
            if(raceRepository.Models.Any(r=>r.RaceName == raceName))
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.RaceExistErrorMessage, raceName));
            }

            IRace race = new Race(raceName, numberOfLaps);
            raceRepository.Add(race);
            return String.Format(OutputMessages.SuccessfullyCreateRace, raceName);
        }

        public string PilotReport()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var pilot in pilotRepository.Models.OrderByDescending(p=>p.NumberOfWins))
            {
                sb.AppendLine(pilot.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string RaceReport()
        {
            StringBuilder sb = new StringBuilder();
            
            foreach(IRace race in raceRepository.Models
                .Where(r=> r.TookPlace))
            {
                sb.AppendLine(race.RaceInfo());
            }

            return sb.ToString().TrimEnd();
        }

        public string StartRace(string raceName)
        {
            IRace race = raceRepository.FindByName(raceName);
            if(race == null)
            {
                throw new NullReferenceException(String.Format(ExceptionMessages.RaceDoesNotExistErrorMessage,raceName));
            }

            if(race.Pilots.Count < 3)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.InvalidRaceParticipants, raceName));
            }

            if(race.TookPlace)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.RaceTookPlaceErrorMessage, raceName));
            }

            var winners = race.Pilots
                .OrderByDescending(p => p.Car.RaceScoreCalculator(race.NumberOfLaps))
                .Take(3)
                .ToArray();

            IPilot first = winners[0];
            IPilot second = winners[1];
            IPilot third = winners[2];

            first.WinRace();

            race.TookPlace = true;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Pilot {first.FullName} wins the {raceName} race.");
            sb.AppendLine($"Pilot {second.FullName} is second in the {raceName} race.");
            sb.AppendLine($"Pilot {third.FullName} is third in the {raceName} race.");

            return sb.ToString().TrimEnd();
        }
    }
}
