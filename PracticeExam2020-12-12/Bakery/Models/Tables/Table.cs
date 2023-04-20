using Bakery.Models.BakedFoods.Contracts;
using Bakery.Models.Drinks.Contracts;
using Bakery.Models.Tables.Contracts;
using Bakery.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bakery.Models.Tables
{
    public abstract class Table : ITable
    {
        private int capacity;
        private int numberOfPeople;

        private ICollection<IBakedFood> foodOrders;
        private ICollection<IDrink> drinkOrders;

        public Table(int tableNumber, int capacity, decimal pricePerPerson)
        {
            Capacity = capacity;
            TableNumber = tableNumber;
            PricePerPerson = pricePerPerson;
            foodOrders = new List<IBakedFood>();
            drinkOrders = new List<IDrink>();
        }

        protected ICollection<IBakedFood> FoodOrders => foodOrders;
        protected ICollection<IDrink> DrinkOrders => drinkOrders;

        public int TableNumber { get; private set; }

        public int Capacity
        {
            get { return capacity; }
            private set
            {
                if(value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidTableCapacity);
                }
                capacity = value;
            }
        }

        public int NumberOfPeople
        {
            get { return numberOfPeople; }
            private set
            {
                if(value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidNumberOfPeople);
                }
                numberOfPeople = value;
            }
        }

        public decimal PricePerPerson { get; private set; }

        public bool IsReserved { get; private set; }

        public decimal Price => PricePerPerson * NumberOfPeople;

        public void Clear()
        {
            DrinkOrders.Clear();
            FoodOrders.Clear();
            //IsReserved = false;
            
        }

        public decimal GetBill()
        {
            return DrinkOrders.Sum(d => d.Price) + FoodOrders.Sum(f => f.Price);
        }

        public string GetFreeTableInfo()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Table: {TableNumber}");
            sb.AppendLine($"Type: {this.GetType().Name}");
            sb.AppendLine($"Capacity: {Capacity}");
            sb.AppendLine($"Price per Person: {PricePerPerson}");

            return sb.ToString().TrimEnd();
        }

        public void OrderDrink(IDrink drink)
        {
            DrinkOrders.Add(drink);
        }

        public void OrderFood(IBakedFood food)
        {
            FoodOrders.Add(food);
        }

        public void Reserve(int numberOfPeople)
        {
            NumberOfPeople = numberOfPeople;
            IsReserved = true;
        }
    }
}
