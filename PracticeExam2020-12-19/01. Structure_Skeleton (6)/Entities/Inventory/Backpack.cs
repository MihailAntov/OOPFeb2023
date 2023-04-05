using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarCroft.Entities.Inventory
{
    public class Backpack : Bag
    {
        private const int BackpackCapacity = 100;
        public Backpack() 
            : base(BackpackCapacity)
        {
        }
    }
}
