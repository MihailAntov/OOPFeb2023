using Heroes.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Heroes.Utilities.Messages;
namespace Heroes.Models.Weapons
{
    public abstract class Weapon : IWeapon
    {
        private string name;
        private int durability;

        public Weapon(string name, int durability)
        {
            Name = name;
            Durability = durability;
        }
        
        public string Name
        {
            get { return name; }
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.WeaponTypeNull);
                }
                name = value;
            }
        }

        public int Durability
        {
            get { return durability; }  
            protected set
            {
                if(value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.DurabilityBelowZero);
                }
                durability = value;
            }
        }

        public abstract int DoDamage();
        
    }
}
