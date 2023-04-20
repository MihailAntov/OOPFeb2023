using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Models.Drinks
{
    public class Tea : Drink
    {
        private const decimal TeaPrice = 2.50M;
        public Tea(string name, int portion , string brand) 
            : base(name, portion, TeaPrice, brand)
        {
        }
    }
}
