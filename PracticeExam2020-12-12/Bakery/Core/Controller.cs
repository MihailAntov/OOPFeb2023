using Bakery.Core.Contracts;
using Bakery.Models.BakedFoods;
using Bakery.Models.BakedFoods.Contracts;
using Bakery.Models.Drinks;
using Bakery.Models.Drinks.Contracts;
using Bakery.Models.Tables;
using Bakery.Models.Tables.Contracts;
using Bakery.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Bakery.Core
{
    public class Controller : IController
    {
        private List<IBakedFood> bakedFoods;
        private List<IDrink> drinks;
        private List<ITable> tables;
        private decimal totalEarnings;

        public Controller()
        {
            bakedFoods = new List<IBakedFood>();
            drinks = new List<IDrink>();
            tables = new List<ITable>();
        }
        
        public string AddDrink(string type, string name, int portion, string brand)
        {
            IDrink drink = null!;
            switch(type)
            {
                case nameof(Water):
                    drink = new Water(name, portion,brand);
                    break;
                case nameof(Tea):
                    drink = new Tea(name, portion, brand);
                    break;
            }
            drinks.Add(drink);
            return String.Format(OutputMessages.DrinkAdded, name, brand);
        }

        public string AddFood(string type, string name, decimal price)
        {
            IBakedFood food = null!;
            switch (type)
            {
                case nameof(Cake):
                    food = new Cake(name, price);
                    break;
                case nameof(Bread):
                    food = new Bread(name, price);
                    break;
            }

            bakedFoods.Add(food);
            return String.Format(OutputMessages.FoodAdded, name, type);
        }

        public string AddTable(string type, int tableNumber, int capacity)
        {
            ITable table = null!;
            switch(type)
            {
                case nameof(InsideTable):
                    table = new InsideTable(tableNumber, capacity);
                    break;
                case nameof(OutsideTable):
                    table = new OutsideTable(tableNumber, capacity);
                    break;
            }
            tables.Add(table);
            return String.Format(OutputMessages.TableAdded, tableNumber);
        }

        public string GetFreeTablesInfo()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var table in tables.Where(t=>!t.IsReserved))
            {
                sb.AppendLine(table.GetFreeTableInfo());
            }
            return sb.ToString().TrimEnd();
        }

        public string GetTotalIncome()
        {
            //decimal total = tables.Sum(t => t.GetBill());
            return $"Total income: {totalEarnings:f2}lv";
        }

        public string LeaveTable(int tableNumber)
        {
            ITable table = tables.FirstOrDefault(t => t.TableNumber == tableNumber);
            decimal bill = table.GetBill() + table.Price;
            table.Clear();
            totalEarnings += bill;

            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Table: {tableNumber}");
            sb.AppendLine($"Bill: {bill:f2}");

            return sb.ToString().TrimEnd();
        }

        public string OrderDrink(int tableNumber, string drinkName, string drinkBrand)
        {
            ITable table = tables.FirstOrDefault(t => t.TableNumber == tableNumber);
            if (table == null)
            {
                return String.Format(OutputMessages.WrongTableNumber, tableNumber);
            }

            IDrink drink = drinks.FirstOrDefault(d => d.Brand == drinkBrand);
            if (drink == null)
            {
                return String.Format(OutputMessages.NonExistentDrink,drinkName, drinkBrand);
            }

            table.OrderDrink(drink);
            return String.Format(OutputMessages.FoodOrderSuccessful, tableNumber, $"{drinkName} {drinkBrand}");
        }

        public string OrderFood(int tableNumber, string foodName)
        {
            ITable table = tables.FirstOrDefault(t => t.TableNumber == tableNumber);
            if(table == null)
            {
                return String.Format(OutputMessages.WrongTableNumber,tableNumber);
            }

            IBakedFood food = bakedFoods.FirstOrDefault(f => f.Name == foodName);
            if(food == null)
            {
                return String.Format(OutputMessages.NonExistentFood, foodName);
            }

            table.OrderFood(food);
            return String.Format(OutputMessages.FoodOrderSuccessful, tableNumber, foodName);
        }

        public string ReserveTable(int numberOfPeople)
        {
            ITable table = tables.FirstOrDefault(t => !t.IsReserved && t.Capacity >= numberOfPeople);
            if(table == null)
            {
                return String.Format(OutputMessages.ReservationNotPossible, numberOfPeople);
            }

            table.Reserve(numberOfPeople);
            return String.Format(OutputMessages.TableReserved, table.TableNumber, numberOfPeople);

        }
    }
}
