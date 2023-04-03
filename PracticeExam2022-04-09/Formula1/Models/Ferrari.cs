using Formula1.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Formula1.Models
{
    public class Ferrari : FormulaOneCar, IFormulaOneCar
    {
        public Ferrari(string model, int horsepower, double engineDisplacement) 
            : base(model, horsepower, engineDisplacement)
        {
        }
    }
}
