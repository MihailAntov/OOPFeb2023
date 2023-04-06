using Easter.Core.Contracts;
using Easter.Models.Bunnies;
using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes;
using Easter.Models.Dyes.Contracts;
using Easter.Models.Eggs;
using Easter.Models.Eggs.Contracts;
using Easter.Models.Workshops;
using Easter.Models.Workshops.Contracts;
using Easter.Repositories;
using Easter.Repositories.Contracts;
using Easter.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easter.Core
{
    public class Controller : IController
    {
        private IRepository<IBunny> bunnies;
        private IRepository<IEgg> eggs;

        private string[] validBunnyTypes =
        {
            nameof(HappyBunny),
            nameof(SleepyBunny)
        };

        public Controller()
        {
            bunnies = new BunnyRepository();
            eggs = new EggRepository();
        }
        
        public string AddBunny(string bunnyType, string bunnyName)
        {
            if(!validBunnyTypes.Contains(bunnyType))
            {
                throw new InvalidOperationException(ExceptionMessages.InvalidBunnyType);
            }

            IBunny bunny = null!;
            switch(bunnyType)
            {
                case nameof(HappyBunny):
                    bunny = new HappyBunny(bunnyName);
                    break;
                case nameof(SleepyBunny):
                    bunny = new SleepyBunny(bunnyName);
                    break;
            }
            bunnies.Add(bunny);

            return string.Format(OutputMessages.BunnyAdded, bunnyType, bunnyName);
        }

        public string AddDyeToBunny(string bunnyName, int power)
        {
            IBunny bunny = bunnies.FindByName(bunnyName);
            if(bunny == null)
            {
                throw new InvalidOperationException(ExceptionMessages.InexistentBunny);
            }

            IDye dye = new Dye(power);
            bunny.AddDye(dye);
            return string.Format(OutputMessages.DyeAdded, power, bunnyName);
        }

        public string AddEgg(string eggName, int energyRequired)
        {
            IEgg egg = new Egg(eggName,energyRequired);
            eggs.Add(egg);
            return string.Format(OutputMessages.EggAdded, eggName);
        }

        public string ColorEgg(string eggName)
        {
            IEgg egg = eggs.FindByName(eggName);
            List<IBunny> validBunnies = bunnies.Models
                .Where(b => b.Energy >= 50)
                .OrderByDescending(b=>b.Energy)
                .ToList();
            IWorkshop workshop = new Workshop();
            if (validBunnies.Count == 0)
            {
                throw new InvalidOperationException(ExceptionMessages.BunniesNotReady);
            }
            
            while(!egg.IsDone())
            {
                IBunny currentBunny = validBunnies.FirstOrDefault();
                
                workshop.Color(egg, currentBunny);
                if(currentBunny.Energy  == 0)
                {
                    bunnies.Remove(currentBunny);
                }
                validBunnies.Remove(currentBunny);

                if(validBunnies.Count == 0)
                {
                    break;
                }
            }

            string eggStatus = egg.IsDone() ?
                string.Format(OutputMessages.EggIsDone, eggName)
                : string.Format(OutputMessages.EggIsNotDone, eggName);

            return eggStatus;
            
        }

        public string Report()
        {
            int countColoredEggs = eggs.Models.Where(e => e.IsDone()).Count();
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{countColoredEggs} eggs are done!");
            sb.AppendLine("Bunnies info:");
            foreach(var bunny in bunnies.Models)
            {
                sb.AppendLine($"Name: {bunny.Name}");
                sb.AppendLine($"Energy: {bunny.Energy}");
                sb.AppendLine($"Dyes: {bunny.Dyes.Count(d=>!d.IsFinished())} not finished");
            }

            return sb.ToString().TrimEnd();
        }
    }
}
