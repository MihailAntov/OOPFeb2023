using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Models.Products.Components
{
    public class PowerSupply : Component
    {
        private const double performanceMultiplier = 1.05;
        public PowerSupply(int id, string manufacturer, string model, decimal price, double overallPerformance, int generation)
            : base(id, manufacturer, model, price, overallPerformance * performanceMultiplier, generation)
        {

        }
    }
}
