﻿using CarRacing.Models.Cars.Contracts;
using CarRacing.Models.Racers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarRacing.Utilities.Messages;

namespace CarRacing.Models.Racers
{
    public abstract class Racer : IRacer
    {
        private string username;
        private string racingBehavior;
        private int drivingExperience;
        private ICar car;

        public Racer(string username, string racingBehavior, int drivingExperience, ICar car)
        {
            Username = username;
            RacingBehavior = racingBehavior;
            DrivingExperience = drivingExperience;
            Car = car;
        }

        public string Username
        {
            get { return username; }
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerName);
                }
                username = value;
            }
        }

        public string RacingBehavior
        {
            get { return racingBehavior; }
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerBehavior);
                }
                racingBehavior = value;
            }
        }

        public int DrivingExperience
        {
            get { return drivingExperience; }
            protected set
            {
                if(value < 0 || value > 100)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerDrivingExperience);
                }
                drivingExperience = value;
            }
        }

        public ICar Car
        {
            get { return car; }
            private set
            {
                if(value == null)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidRacerCar);
                }
                car = value;
            }
        }

        public  bool IsAvailable()
        {
            return Car.FuelAvailable >= Car.FuelConsumptionPerRace;
        }

        public virtual void Race()
        {
            Car.Drive();

        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{this.GetType().Name}: {Username}");
            sb.AppendLine($"--Driving behavior: {RacingBehavior}");
            sb.AppendLine($"--Driving experience: {DrivingExperience}");
            sb.AppendLine($"--Car: {Car.Make} {Car.Model} ({Car.VIN})");

            return sb.ToString().TrimEnd();
        }
    }
}
