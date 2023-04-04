using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using ChristmasPastryShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChristmasPastryShop.Models.Booths
{
    public class Booth : IBooth
    {
        private int capacity;
        private IRepository<IDelicacy> delicacyMenu;
        private IRepository<ICocktail> cocktailMenu;
        private double currentBill;
        private double turnover;
        private bool isReserved;

        public Booth(int boothId, int capacity)
        {
            BoothId = boothId;
            Capacity = capacity;
            delicacyMenu = new DelicacyRepository();
            cocktailMenu = new CocktailRepository();
            CurrentBill = 0;
            Turnover = 0;
            isReserved = false;


        }

        public int BoothId { get; private set; }

        public int Capacity
        {
            get { return capacity; }
            private set
            {
                if(value <=0)
                {
                    throw new ArgumentException(ExceptionMessages.CapacityLessThanOne);
                }
                capacity = value; 
            }
        }

        public IRepository<IDelicacy> DelicacyMenu => delicacyMenu;

        public IRepository<ICocktail> CocktailMenu => cocktailMenu;

        public double CurrentBill
        {
            get { return currentBill; }
            private set
            {
                currentBill = value;
            }
        }

        public double Turnover
        {
            get { return turnover; }    
            private set { turnover = value; }
        }

        public bool IsReserved
        {
            get { return isReserved; }  
            private set { isReserved = value; } 
        }

        public void ChangeStatus()
        {
            IsReserved = !IsReserved;
        }

        public void Charge()
        {
            Turnover += CurrentBill;
            CurrentBill = 0;
        }

        public void UpdateCurrentBill(double amount)
        {
            CurrentBill += amount;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Booth: {BoothId}");
            sb.AppendLine($"Capacity: {Capacity}");
            sb.AppendLine($"Turnover: {Turnover:f2} lv");
            sb.AppendLine($"-Cocktail menu:");
            foreach(var cocktail in CocktailMenu.Models)
            {
                sb.AppendLine(cocktail.ToString());
            }
            sb.AppendLine($"-Delicacy menu:");
            foreach(var delicacy in DelicacyMenu.Models)
            {
                sb.AppendLine(delicacy.ToString());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
