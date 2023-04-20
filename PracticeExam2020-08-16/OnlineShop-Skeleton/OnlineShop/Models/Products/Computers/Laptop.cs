using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Models.Products.Computers
{
    public class Laptop : Computer
    {
        private const int LaptopOverallPerformance = 10;
        public Laptop(int id, string manufacturer, string model, decimal price ) 
            : base(id, manufacturer, model, price, LaptopOverallPerformance)
        {
        }
    }
}
