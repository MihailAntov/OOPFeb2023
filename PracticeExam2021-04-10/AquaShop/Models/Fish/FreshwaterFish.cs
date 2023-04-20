using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaShop.Models.Fish
{
    public class FreshwaterFish : Fish
    {
        private const int FreshWaterFishInitialSize = 3;
        private const int FreshWaterFishSizePerEat = 3;

        public FreshwaterFish(string name, string species, decimal price) 
            : base(name, species, price)
        {
            Size = FreshWaterFishInitialSize;
        }

        public override void Eat()
        {
            Size += FreshWaterFishSizePerEat;
        }
    }
}
