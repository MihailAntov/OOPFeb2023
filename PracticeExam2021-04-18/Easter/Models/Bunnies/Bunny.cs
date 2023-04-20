using Easter.Models.Bunnies.Contracts;
using Easter.Models.Dyes.Contracts;
using Easter.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Easter.Models.Bunnies
{
    public abstract class Bunny : IBunny
    {
        private string name;
        private int energy;
        private ICollection<IDye> dyes;
        public Bunny(string name, int energy)
        {
            Name = name;
            Energy = energy;
            dyes = new List<IDye>();
        }
        public string Name
        {
            get { return name; }
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.InvalidBunnyName);
                }
                name = value;
            }
        }
        public int Energy
        {
            get { return energy; }
            protected set
            {
                energy = value;
                if(energy < 0)
                {
                    energy = 0;
                }
            }
        }

        public ICollection<IDye> Dyes => dyes;

        public void AddDye(IDye dye)
        {
            dyes.Add(dye);
        }

        public virtual void Work()
        {
            Energy -= 10;
        }
    }
}
