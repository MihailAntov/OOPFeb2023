using ChristmasPastryShop.Models.Cocktails.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ChristmasPastryShop.Utilities.Messages;

namespace ChristmasPastryShop.Models.Cocktails
{
    public class Cocktail : ICocktail
    {
        private string name;
        private string size;
        private double price;

        public Cocktail(string cocktailname, string size, double price)
        {
            Name = cocktailname;
            Size = size;
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

        public string Size
        {
            get { return size; }
            private set
            {
                size = value;
                //TODO : Add validation for allowed values
            }
        }


        public double Price
        {
            get { return price; }
            private set
            {
                switch (Size)
                {
                    case "Large":
                        price = value;
                        break;
                    case "Middle":
                        price = value * 2.0 / 3.0;
                        break;
                    case "Small":
                        price = value / 3.0;
                        break;
                }
            }
        }

        public override string ToString()
        {
            return $"--{Name} ({Size}) - {Price:f2} lv";
        }
    }
}
