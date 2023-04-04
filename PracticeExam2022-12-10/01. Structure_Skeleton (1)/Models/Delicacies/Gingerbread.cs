using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasPastryShop.Models.Delicacies
{
    public class Gingerbread : Delicacy
    {
        private const double GingerBreadPrice = 4.00;
        public Gingerbread(string delicacyName) 
            : base(delicacyName, GingerBreadPrice)
        {
        }
    }
}
