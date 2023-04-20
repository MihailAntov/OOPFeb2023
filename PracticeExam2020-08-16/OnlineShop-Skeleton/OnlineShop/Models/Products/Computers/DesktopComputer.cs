using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineShop.Models.Products.Computers
{
    public class DesktopComputer : Computer
    {
        private const int DesktopComputerOverallPerformance = 15;
        
        public DesktopComputer(int id, string manufacturer, string model, decimal price ) 
            : base(id, manufacturer, model, price, DesktopComputerOverallPerformance)
        {
        }


    }
}
