using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Interfaces;

namespace WildFarm.Models
{
    public class Owl : Bird
    {
        private const double OwlWeightGainOnFeed = 0.25;
        public Owl(string name, double weight, double wingSize)
            :base(name, weight, 0, wingSize)
        {
            WeightGainOnFeed = OwlWeightGainOnFeed;
        }
        public override string AskForFood()
        {
            return "Hoot Hoot";
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
