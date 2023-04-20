using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarCroft.Entities.Inventory
{
    public class Satchel : Bag
    {
        private const int SatchelCapacity = 20;
        public Satchel() 
            : base(SatchelCapacity)
        {
        }
    }
}
