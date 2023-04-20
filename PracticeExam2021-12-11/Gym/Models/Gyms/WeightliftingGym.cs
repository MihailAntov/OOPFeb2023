using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Models.Gyms
{
    public class WeightliftingGym : Gym
    {
        private const int WeightLiftingGymCapacity = 20;
        public WeightliftingGym(string name) 
            : base(name, WeightLiftingGymCapacity)
        {
        }
    }
}
