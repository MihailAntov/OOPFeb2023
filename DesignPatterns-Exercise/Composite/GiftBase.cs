using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Composite
{
    public abstract class GiftBase
    {
        private string name;
        private decimal price;
        public GiftBase(string name, decimal price)
        {
            Name = name;
            Price = price;
        }

        protected string Name { get { return name; } private set { name = value; } }
        protected decimal Price { get { return price; } private set { price = value; } }
        public abstract decimal CalculateTotalPrice();
    }
}
