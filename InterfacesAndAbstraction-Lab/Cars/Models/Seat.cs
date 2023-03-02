using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cars.Models
{
    internal class Seat : ICar
    {
        public Seat(string model, string color)
        {
            Color = color;
            Model = model;
        }

        public string Color { get; set; }
        public string Model { get; set; }

        public string End()
        {
            return "Breaaak!";
        }

        public string Start()
        {
            return "Engine start";
        }

        public override string ToString()
        {
            return $"{Color} Seat {Model}";
        }
    }
}
