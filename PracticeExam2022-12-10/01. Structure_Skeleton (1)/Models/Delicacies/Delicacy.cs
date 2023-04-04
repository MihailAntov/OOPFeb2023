using ChristmasPastryShop.Models.Delicacies.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChristmasPastryShop.Utilities.Messages;

namespace ChristmasPastryShop.Models.Delicacies
{
    public abstract class Delicacy : IDelicacy
    {
        private string name;
        private double price;

        public Delicacy(string delicacyName, double price)
        {
            Name = delicacyName;
            Price = price;
        }
        public string Name
        {
            get { return name; }
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.NameNullOrWhitespace);
                }
                name = value;
            }
        }

        public double Price { get { return price; } private set { price = value; } }

        public override string ToString()
        {
            return $"--{Name} - {Price:f2} lv";
        }
    }
}
