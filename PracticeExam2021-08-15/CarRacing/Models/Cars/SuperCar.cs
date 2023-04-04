using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRacing.Models.Cars
{
    public class SuperCar : Car
    {
        private const double SuperCarStartingFuel = 80;
        private const double SuperCarFuelConsumption = 10;
        public SuperCar(string make, string model, string vin, int horsePower) 
            : base(make, model, vin, horsePower, SuperCarStartingFuel, SuperCarFuelConsumption)
        {
        }
    }
}
