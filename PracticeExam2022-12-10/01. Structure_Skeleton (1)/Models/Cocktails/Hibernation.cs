using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasPastryShop.Models.Cocktails
{
    public class Hibernation : Cocktail
    {
        private const double HibernationPrice = 10.50;
        public Hibernation(string cocktailname, string size) 
            : base(cocktailname, size, HibernationPrice)
        {
        }
    }
}
