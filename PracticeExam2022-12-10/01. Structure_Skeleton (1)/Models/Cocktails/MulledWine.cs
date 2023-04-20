using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasPastryShop.Models.Cocktails
{
    public class MulledWine : Cocktail
    {
        private const double MulledWinePrice = 13.50;
        public MulledWine(string cocktailname, string size) 
            : base(cocktailname, size, MulledWinePrice)
        {
        }
    }
}
