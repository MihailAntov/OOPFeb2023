﻿using Formula1.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Formula1.Utilities;

namespace Formula1.Models
{
    public class Race : IRace
    {
        private string raceName;
        private int numberOfLaps;
        private bool tookPlace;
        private ICollection<IPilot> pilots;


        public Race(string raceName, int numberOfLaps)
        {
            RaceName = raceName;
            NumberOfLaps = numberOfLaps;
            TookPlace = false;
            pilots = new List<IPilot>();
        }
        public string RaceName
        {
            get { return raceName; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < 5)
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.InvalidRaceName, value));
                }

                raceName = value;
            }
        }

        public int NumberOfLaps
        {
            get { return numberOfLaps; }
            private set
            {
                if(value < 1)
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.InvalidLapNumbers, value));
                }
                numberOfLaps = value;
            }
        }

        public bool TookPlace { get { return tookPlace; } set { tookPlace = value; } }

        public ICollection<IPilot> Pilots
        {
            get { return pilots; }
            
            //private set { pilots = value; }
        }

        public void AddPilot(IPilot pilot)
        {
            Pilots.Add(pilot);
        }

        public string RaceInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"The {RaceName} race has:");
            sb.AppendLine($"Participants: {Pilots.Count}");
            sb.AppendLine($"Number of laps: {NumberOfLaps}");
            string tookPlaceString = TookPlace ? "Yes" : "No";
            sb.AppendLine($"Took place: {tookPlaceString}");

            return sb.ToString().TrimEnd();
        }
    }
}
