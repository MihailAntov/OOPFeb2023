using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _03.ShoppingSpree
{
    public class Person
    {
        private string name;
        private decimal money;
        public Person(string name, decimal money)
        {
            Name = name;
            Money = money;
            products = new List<Product>();
        }
        private List<Product> products;
        public string Name
        {
            get { return name; }
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException("Name cannot be empty");
                }
                name = value;
            }
        }

        public decimal Money
        {
            get { return money; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException("Money cannot be negative");
                }
                money = value;
            }
        }
        //public IReadOnlyCollection<Product> Products
        //{
        //    get { return products.AsReadOnly(); }
        //}

        public string Buy(Product product)
        {
            if(product.Cost > Money)
            {
                return $"{Name} can't afford {product.Name}";
            }

            Money-= product.Cost;
            products.Add(product);

            return $"{Name} bought {product.Name}";
        }

        public override string ToString()
        {
            string result = $"{Name} - ";
            
            if(products.Count == 0)
            {
                result += "Nothing bought";
            }
            else
            {
                result += string.Join(", ", products.Select(n => n.Name));
            }

            return result;
        }


    }
}
