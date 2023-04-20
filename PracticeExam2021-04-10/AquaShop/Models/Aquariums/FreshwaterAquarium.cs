using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaShop.Models.Aquariums
{
    public class FreshwaterAquarium : Aquarium
    {
        private const int FreshWaterCapacity = 50;
        public FreshwaterAquarium(string name) 
            : base(name, FreshWaterCapacity)
        {
        }
    }
}
