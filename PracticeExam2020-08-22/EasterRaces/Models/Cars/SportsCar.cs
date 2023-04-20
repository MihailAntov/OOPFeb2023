using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EasterRaces.Models.Cars
{
    public class SportsCar : Car
    {
        private const double SportsCarCubicCentimeters = 3000;
        private const int SportsCarMinHorsePower = 250;
        private const int SportsCarMaxHorsePower = 450;
        public SportsCar(string model, int horsePower)
            : base(model, horsePower, SportsCarCubicCentimeters, SportsCarMinHorsePower, SportsCarMaxHorsePower)
        {

        }
    }
}
