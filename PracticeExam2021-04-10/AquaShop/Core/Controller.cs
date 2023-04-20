using AquaShop.Core.Contracts;
using AquaShop.Models.Aquariums;
using AquaShop.Models.Aquariums.Contracts;
using AquaShop.Models.Decorations;
using AquaShop.Models.Decorations.Contracts;
using AquaShop.Models.Fish;
using AquaShop.Models.Fish.Contracts;
using AquaShop.Repositories;
using AquaShop.Repositories.Contracts;
using AquaShop.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AquaShop.Core
{
    public class Controller : IController
    {

        private IRepository<IDecoration> decorations;
        private ICollection<IAquarium> aquariums;

        private string[] validAquariumTypes =
        {
            nameof(FreshwaterAquarium),
            nameof(SaltwaterAquarium)
        };

        private string[] validDecorationTypes =
        {
            nameof(Ornament),
            nameof(Plant)
        };

        private string[] validFishTypes =
        {
            nameof(FreshwaterFish),
            nameof(SaltwaterFish)
        };

        public Controller()
        {
            decorations = new DecorationRepository();
            aquariums = new List<IAquarium>();
        }
        public string AddAquarium(string aquariumType, string aquariumName)
        {
            if(!validAquariumTypes.Contains(aquariumType))
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidAquariumType);
            }
            IAquarium aquarium = null!;
            switch (aquariumType)
            {
                case nameof(SaltwaterAquarium):
                    aquarium = new SaltwaterAquarium(aquariumName);
                    break;
                case nameof(FreshwaterAquarium):
                    aquarium = new FreshwaterAquarium(aquariumName);
                    break;
            }
            aquariums.Add(aquarium);

            return String.Format(OutputMessages.SuccessfullyAdded, aquariumType);
        }

        public string AddDecoration(string decorationType)
        {
            if(!validDecorationTypes.Contains(decorationType))
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidDecorationType);
            }

            IDecoration decoration = null!;
            switch(decorationType)
            {
                case nameof(Plant):
                    decoration = new Plant();
                    break;
                case nameof(Ornament):
                    decoration = new Ornament();
                    break;
            }

            decorations.Add(decoration);

            return String.Format(OutputMessages.SuccessfullyAdded, decorationType);
        }

        public string AddFish(string aquariumName, string fishType, string fishName, string fishSpecies, decimal price)
        {
            if(!validFishTypes.Contains(fishType))
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidFishType);
            }

            IFish fish = null!;
            IAquarium aquarium = aquariums.FirstOrDefault(f => f.Name == aquariumName);
            switch(fishType)
            {
                case nameof(SaltwaterFish):
                    fish = new SaltwaterFish(fishName, fishSpecies, price);
                    aquarium = aquarium as SaltwaterAquarium;
                    break;
                case nameof(FreshwaterFish):
                    fish = new FreshwaterFish(fishName, fishSpecies, price);
                    aquarium = aquarium as FreshwaterAquarium;
                    break;
            }

            if(aquarium == null)
            {
                return OutputMessages.UnsuitableWater;
            }

            aquarium.AddFish(fish);
            return String.Format(OutputMessages.EntityAddedToAquarium, fishType, aquariumName);

            
        }

        public string CalculateValue(string aquariumName)
        {
            IAquarium aquarium = aquariums.FirstOrDefault(a => a.Name == aquariumName);
            decimal value = aquarium.Fish.Sum(f => f.Price) + aquarium.Decorations.Sum(d => d.Price);

            return string.Format(OutputMessages.AquariumValue, aquariumName, value);
        }

        public string FeedFish(string aquariumName)
        {
            IAquarium aquarium = aquariums.FirstOrDefault(a => a.Name == aquariumName);
            aquarium.Feed();

            return string.Format(OutputMessages.FishFed, aquarium.Fish.Count);
        }

        public string InsertDecoration(string aquariumName, string decorationType)
        {
            IDecoration decoration = decorations.FindByType(decorationType);
            if(decoration == null)
            {
                throw new InvalidOperationException(String.Format(ExceptionMessages.InexistentDecoration, decorationType));
            }

            IAquarium aquarium = aquariums.FirstOrDefault(a => a.Name == aquariumName);

            aquarium.AddDecoration(decoration);
            decorations.Remove(decoration);
            return String.Format(OutputMessages.EntityAddedToAquarium, decorationType, aquariumName);
        }

        public string Report()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var aquarium in aquariums)
            {
                sb.AppendLine(aquarium.GetInfo());
            }

            return sb.ToString().TrimEnd();
        }
    }
}
