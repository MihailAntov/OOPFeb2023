﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasterRaces.Models.Cars
{
    public class MuscleCar : Car
    {
        private const double MuscleCarCubicCentimeters = 5000;
        private const int MuscleCarMinHorsePower = 400;
        private const int MuscleCarMaxHorsePower = 600;
        public MuscleCar(string model, int horsePower) 
            : base(model, horsePower, MuscleCarCubicCentimeters, MuscleCarMinHorsePower, MuscleCarMaxHorsePower)
        {

        }
    }
}
