﻿using Gym.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Models.Athletes
{
    public class Boxer : Athlete
    {
        private const int BoxerStamina = 60;
        public Boxer(string fullName, string motivation, int numberOfMedals) 
            : base(fullName, motivation, numberOfMedals, BoxerStamina)
        {

        }

        public override void Exercise()
        {
            Stamina += 15;
            if(Stamina > 100)
            {
                Stamina = 100;
                throw new ArgumentException(ExceptionMessages.InvalidStamina);
            }
        }
    }
}
