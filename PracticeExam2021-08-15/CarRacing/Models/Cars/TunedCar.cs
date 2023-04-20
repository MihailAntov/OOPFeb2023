using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarRacing.Models.Cars
{
    public class TunedCar : Car
    {
        private const double TunedCarStartingFuel = 65;
        private const double TunedCarFuelConsumption = 7.5;
        public TunedCar(string make, string model, string vin, int horsePower) : 
            base(make, model, vin, horsePower, TunedCarStartingFuel, TunedCarFuelConsumption)
        {
        }

        public override void Drive()
        {
            base.Drive();
            HorsePower = (int)Math.Round((double)HorsePower * 0.97);
        }
    }
}
