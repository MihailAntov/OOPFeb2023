using System;

using WildFarm.Models.Interfaces;

namespace WildFarm.Models
{
    public class Cat : Feline
    {
        private const double CatWeightGainOnFeed = 0.30;
        public Cat(string name, double weight, string livingRegion, string breed)
            : base(name, weight, 0, livingRegion, breed)
        {
            WeightGainOnFeed = CatWeightGainOnFeed;
        }
        public override string AskForFood()
        {
            return "Meow";
        }

        public override void Feed(IFood food)
        {
            if (food is Vegetable || food is Meat)
            {
                base.Feed(food);
                return;
            }

            Console.WriteLine(CantEat(food));
        }
    }
}
