using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasPastryShop.Models.Delicacies
{
    public class Stolen : Delicacy
    {
        private const double StolenPrice = 3.50;
        public Stolen(string delicacyName) 
            : base(delicacyName, StolenPrice)
        {
        }
    }
}
