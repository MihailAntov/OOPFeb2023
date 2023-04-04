using CarRacing.Models.Cars.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRacing.Models.Racers
{
    public class ProfessionalRacer : Racer
    {
        private const string ProfessionalRacerBehavior = "strict";
        private const int ProfessionalRacerStartingExperience = 30;
        private const int ProfessionalRacerExperiencePerRace = 10;
        public ProfessionalRacer(string username, ICar car) 
            : base(username, ProfessionalRacerBehavior, ProfessionalRacerStartingExperience, car)
        {
        }

        public override void Race()
        {
            base.Race();
            DrivingExperience += ProfessionalRacerExperiencePerRace; 
        }


    }
}
