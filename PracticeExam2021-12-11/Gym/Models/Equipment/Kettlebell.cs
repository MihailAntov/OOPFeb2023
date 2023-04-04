using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Models.Equipment
{
    internal class Kettlebell : Equipment
    {

        private const double KettlebellWeight = 10000;
        private const decimal KettlebellPrice = 80;

        public Kettlebell() : base(KettlebellWeight, KettlebellPrice)
        {
        }
    }
}
