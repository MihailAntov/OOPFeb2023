using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaShop.Models.Fish
{
    public class SaltwaterFish : Fish
    {
        private const int SaltwaterFishInitialSize = 5;
        private const int SaltwaterFishSizePerEat = 2;
        public SaltwaterFish(string name, string species, decimal price) 
            : base(name, species, price)
        {
            Size = SaltwaterFishInitialSize;
        }

        public override void Eat()
        {
            Size += SaltwaterFishSizePerEat; 
        }
    }
}
