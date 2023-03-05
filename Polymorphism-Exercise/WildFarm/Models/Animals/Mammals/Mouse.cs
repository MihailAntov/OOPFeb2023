using System;

using WildFarm.Models.Interfaces;

namespace WildFarm.Models
{
    public class Mouse : Mammal
    {
        private const double MouseWeightGainOnFeed = 0.10;
        public Mouse(string name, double weight, string livingRegion)
            : base(name, weight, 0, livingRegion)
        {
            WeightGainOnFeed = MouseWeightGainOnFeed;
        }
        public override string AskForFood()
        {
            return "Squeak";
        }

        public override void Feed(IFood food)
        {
            if(food is Vegetable || food is Fruit)
            {
                base.Feed(food);
                return;
            }

            Console.WriteLine(CantEat(food));
        }
    }
}
