using Gym.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gym.Models.Athletes
{
    public class Weightlifter : Athlete
    {
        private const int WeightliferStamina = 50;
        public Weightlifter(string fullName, string motivation, int numberOfMedals) 
            : base(fullName, motivation, numberOfMedals, WeightliferStamina)
        {
        }

        public override void Exercise()
        {
            Stamina += 10;
            if(Stamina > 100)
            {
                Stamina = 100;
            }
            throw new ArgumentException(ExceptionMessages.InvalidStamina);
        }
    }
}
