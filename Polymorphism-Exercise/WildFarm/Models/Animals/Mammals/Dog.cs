using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Interfaces;

namespace WildFarm.Models
{
    public  class Dog : Mammal
    {
        private const double DogWeightGainOnFeed = 0.40;
        public Dog(string name, double weight, string livingRegion)
            : base(name, weight, 0, livingRegion)
        {
            WeightGainOnFeed = DogWeightGainOnFeed;
        }
        public override string AskForFood()
        {
            return "Woof!";
        }

        public override void Feed(IFood food)
        {
            if (food is Meat)
            {
                base.Feed(food);
                return;
            }

            Console.WriteLine(CantEat(food));
        }
    }
}
