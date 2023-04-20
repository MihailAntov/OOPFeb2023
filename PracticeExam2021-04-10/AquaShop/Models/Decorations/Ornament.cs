using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AquaShop.Models.Decorations
{
    public class Ornament : Decoration
    {
        private const int OrnamentComfort = 1;
        private const decimal OrnamentPrice = 5;
        public Ornament() : base(OrnamentComfort, OrnamentPrice)
        {
        }
    }
}
