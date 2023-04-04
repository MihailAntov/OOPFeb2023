using CarRacing.Models.Cars.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRacing.Models.Racers
{
    public class StreetRacer : Racer
    {
        private const string StreetRacerBehavior = "aggressive";
        private const int StreetRacerStartingExperience = 10;
        private const int StreetRacerExperiencePerRace = 5;
        public StreetRacer(string username, ICar car) 
            : base(username, StreetRacerBehavior, StreetRacerStartingExperience, car)
        {
        }

        public override void Race()
        {
            base.Race();
            base.DrivingExperience += StreetRacerExperiencePerRace;
        }
    }
}
