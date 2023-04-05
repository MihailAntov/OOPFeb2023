using EasterRaces.Models.Cars.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EasterRaces.Utilities.Messages;
using EasterRaces.Utilities.Consts;

namespace EasterRaces.Models.Cars
{
    public abstract class Car : ICar
    {
        private string model;
        private int horsePower;
        private int minHorsePower;
        private int maxHorsePower;
        private double cubicCentimeters;

        public Car(string model, int horsePower, double cubicCentimeters, int minHorsePower, int maxHorsePower)
        {
            Model = model;
            this.minHorsePower = minHorsePower;
            this.maxHorsePower = maxHorsePower;
            HorsePower = horsePower;
            CubicCentimeters = cubicCentimeters;
        }


        public string Model
        {
            get { return model; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value) || value.Length < ValidationConstants.MinModelNameLength)
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.InvalidModel, value, ValidationConstants.MinModelNameLength));
                }
                model = value;
            }
        }

        public int HorsePower
        {
            get { return horsePower; }
            private set
            {
                if (value < minHorsePower || value > maxHorsePower)
                {
                    throw new ArgumentException(String.Format(ExceptionMessages.InvalidHorsePower, value));
                }
                horsePower = value;
            }
        }


        public double CubicCentimeters 
        {
            get { return cubicCentimeters; }
            private set { cubicCentimeters = value; } 
    }

        public double CalculateRacePoints(int laps)
        {
            return CubicCentimeters / HorsePower * laps;
        }
    }
}
