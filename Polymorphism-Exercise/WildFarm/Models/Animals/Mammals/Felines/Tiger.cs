using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WildFarm.Models.Interfaces;

namespace WildFarm.Models
{
    public class Tiger : Feline
    {
        private const double TigerWeightGainOnFeed = 1.00;
        public Tiger(string name, double weight, string livingRegion, string breed)
            : base(name, weight, 0, livingRegion, breed)
        {
            WeightGainOnFeed = TigerWeightGainOnFeed;
        }
        public override string AskForFood()
        {
            return "ROAR!!!";
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
