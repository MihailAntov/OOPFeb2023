using ChristmasPastryShop.Core.Contracts;
using ChristmasPastryShop.Models.Booths;
using ChristmasPastryShop.Models.Booths.Contracts;
using ChristmasPastryShop.Repositories;
using ChristmasPastryShop.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Text;

using ChristmasPastryShop.Utilities.Messages;
using System.Linq;
using ChristmasPastryShop.Models.Delicacies.Contracts;
using ChristmasPastryShop.Models.Cocktails.Contracts;
using ChristmasPastryShop.Models.Delicacies;
using ChristmasPastryShop.Models.Cocktails;

namespace ChristmasPastryShop.Core
{
    public class Controller : IController
    {
        private IRepository<IBooth> booths;
        private IRepository<IDelicacy> delicacies;
        private IRepository<ICocktail> cocktails;
        private string[] allowedDelicacyTypes = { "Gingerbread", "Stolen" };
        private string[] allowedCocktailTypes = { "MulledWine", "Hibernation" };
        private string[] allowedCocktailSizes = { "Small", "Middle", "Large" };
        
        public Controller()
        {
            booths = new BoothRepository();
            delicacies = new DelicacyRepository();
            cocktails = new CocktailRepository();   
        }
        
        public string AddBooth(int capacity)
        {
            int id = booths.Models.Count + 1;
            IBooth booth = new Booth(id, capacity);
            booths.AddModel(booth);

            return String.Format(OutputMessages.NewBoothAdded, id, capacity);
        }

        public string AddCocktail(int boothId, string cocktailTypeName, string cocktailName, string size)
        {
            if (!allowedCocktailTypes.Contains(cocktailTypeName))
            {
                return string.Format(OutputMessages.InvalidCocktailType, cocktailTypeName);
            }

            if(!allowedCocktailSizes.Contains(size))
            {
                return string.Format(OutputMessages.InvalidCocktailSize, size);
            }

            if (cocktails.Models.Any(c => c.Name == cocktailName && c.Size == size))
            {
                return string.Format(OutputMessages.CocktailAlreadyAdded, size, cocktailName);
            }

            ICocktail cocktail = null!;
            switch (cocktailTypeName)
            {
                case nameof(MulledWine):
                    cocktail = new MulledWine(cocktailName,size);
                    break;
                case nameof(Hibernation):
                    cocktail = new Hibernation(cocktailName,size);
                    break;
            }

            cocktails.AddModel(cocktail);
            IBooth booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            booth.CocktailMenu.AddModel(cocktail);

            return string.Format(OutputMessages.NewCocktailAdded,size, cocktailName, cocktailTypeName);
        }

        public string AddDelicacy(int boothId, string delicacyTypeName, string delicacyName)
        {
            if(!allowedDelicacyTypes.Contains(delicacyTypeName))
            {
                return String.Format(OutputMessages.InvalidDelicacyType, delicacyTypeName);
            }

            if(delicacies.Models.Any(c=>c.Name == delicacyName))
            {
                return String.Format(OutputMessages.DelicacyAlreadyAdded, delicacyName);
            }

            IDelicacy delicacy = null!;
            switch(delicacyTypeName)
            {
                case nameof(Gingerbread):
                    delicacy = new Gingerbread(delicacyName);
                    break;
                case nameof(Stolen):
                    delicacy = new Stolen(delicacyName);
                    break;
            }

            delicacies.AddModel(delicacy);
            IBooth booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            booth.DelicacyMenu.AddModel(delicacy);

            return String.Format(OutputMessages.NewDelicacyAdded, delicacyTypeName,delicacyName);
        }

        public string BoothReport(int boothId)
        {
            IBooth booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            return booth.ToString();
        }

        public string LeaveBooth(int boothId)
        {
            IBooth booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);
            double bill = booth.CurrentBill;
            booth.Charge();
            booth.ChangeStatus();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Bill {bill:f2} lv");
            sb.AppendLine(string.Format(OutputMessages.BoothIsAvailable, boothId));

            return sb.ToString().TrimEnd();
        }

        public string ReserveBooth(int countOfPeople)
        {
            IBooth booth = booths.Models
                .Where(b => !b.IsReserved && b.Capacity >= countOfPeople)
                .OrderBy(b => b.Capacity)
                .ThenByDescending(b => b.BoothId)
                .FirstOrDefault();

            if (booth == null)
            {
                return string.Format(OutputMessages.NoAvailableBooth,countOfPeople);    
            }

            booth.ChangeStatus();

            return string.Format(OutputMessages.BoothReservedSuccessfully, booth.BoothId, countOfPeople);


        }

        public string TryOrder(int boothId, string order)
        {
            string[] orderArgs = order.Split("/");
            string itemTypeName = orderArgs[0]; 
            string itemName = orderArgs[1]; 
            int count = int.Parse(orderArgs[2]);
            string size = string.Empty;

            if(allowedCocktailTypes.Contains(itemTypeName))
            {
                size = orderArgs[3];
            }

            if(!allowedCocktailTypes.Contains(itemTypeName) && !allowedDelicacyTypes.Contains(itemTypeName))
            {
                return String.Format(OutputMessages.NotRecognizedType, itemTypeName);
            }

            if(!delicacies.Models.Any(d=>d.Name == itemName) && !cocktails.Models.Any(c=>c.Name == itemName))
            {
                return string.Format(OutputMessages.NotRecognizedItemName, itemTypeName, itemName);
            }

            IBooth booth = booths.Models.FirstOrDefault(b => b.BoothId == boothId);

            if(allowedCocktailTypes.Contains(itemTypeName))
            {
                
                ICocktail cocktail = booth.CocktailMenu.Models
                    .FirstOrDefault(c => c.Name == itemName && c.GetType().Name == itemTypeName && c.Size == size);
                if(cocktail == null)
                {
                    return String.Format(OutputMessages.CocktailStillNotAdded, size, itemName);
                }

                booth.UpdateCurrentBill(cocktail.Price * count);

                return String.Format(OutputMessages.SuccessfullyOrdered, boothId, count, itemName);
                
            }


            if(allowedDelicacyTypes.Contains(itemTypeName))
            {
                IDelicacy delicacy = booth.DelicacyMenu.Models
                    .FirstOrDefault(d => d.Name == itemName && d.GetType().Name == itemTypeName);

                if(delicacy == null)
                {
                    return string.Format(OutputMessages.DelicacyStillNotAdded, itemTypeName, itemName);
                }

                booth.UpdateCurrentBill(delicacy.Price * count);

                return string.Format(OutputMessages.SuccessfullyOrdered, boothId, count, itemName);
            }

            return string.Empty;


        }
    }
}
