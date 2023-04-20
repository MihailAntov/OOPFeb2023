using CarRacing.Models.Maps.Contracts;
using CarRacing.Models.Racers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CarRacing.Utilities.Messages;
using CarRacing.Models.Racers;

namespace CarRacing.Models.Maps
{
    public class Map : IMap
    {
        public Map()
        {

        }
        
        public string StartRace(IRacer racerOne, IRacer racerTwo)
        {
            if(!racerOne.IsAvailable() && !racerTwo.IsAvailable())
            {
                return OutputMessages.RaceCannotBeCompleted;
            }

            if(!racerOne.IsAvailable())
            {
                racerTwo.Race();
                return String.Format(OutputMessages.OneRacerIsNotAvailable, racerTwo.Username, racerOne.Username);
            }

            if(!racerTwo.IsAvailable())
            {
                racerOne.Race();
                return String.Format(OutputMessages.OneRacerIsNotAvailable, racerOne.Username, racerTwo.Username);

            }
            //??
            

            

            double racerOneChance = racerOne.Car.HorsePower
                                    * racerOne.DrivingExperience
                                    * GetModiferFromBehavior(racerOne.RacingBehavior);

            double racerTwoChance = racerTwo.Car.HorsePower
                                    * racerTwo.DrivingExperience
                                    * GetModiferFromBehavior(racerTwo.RacingBehavior);

            string winner;
            if(racerOneChance > racerTwoChance)
            {
                winner = racerOne.Username;
            }
            else
            {
                winner = racerTwo.Username;
            }

            racerOne.Race();
            racerTwo.Race();

            return String.Format(OutputMessages.RacerWinsRace,racerOne.Username,racerTwo.Username,winner);
        }

        private static double GetModiferFromBehavior(string behavior)
        {
            if(behavior == "strict")
            {
                return 1.2;
            }
            else if (behavior == "aggressive")
            {
                return 1.1;
            }
            else
            {
                return 0;
            }
        }
    }
}
