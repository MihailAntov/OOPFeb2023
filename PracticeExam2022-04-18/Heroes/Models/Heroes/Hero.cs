using Heroes.Models.Contracts;
using Heroes.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heroes.Models.Heroes
{
    public abstract class Hero : IHero
    {
        private string name;
        private int health;
        private int armour;
        private IWeapon weapon;

        public Hero(string name, int health, int armour)
        {
            Name = name;
            Health = health;
            Armour = armour;
        }
        
        public string Name
        {
            get { return name; }
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.HeroNameNull);
                }
                name = value;
            }
        }

        public int Health
        {
            get { return health; }
            private set
            {
                if (value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.HeroHealthBelowZero);
                }
                health = value;
            }
        }

        public int Armour
        {
            get { return armour; }
            private set
            {
                if (value < 0)
                {
                     throw new ArgumentException(ExceptionMessages.HeroArmourBelowZero);
                }
                armour = value;
            }
        }

        public IWeapon Weapon
        {
            get { return weapon; }
            private set
            {
                if (value == null)
                {
                    throw new ArgumentException(ExceptionMessages.WeaponNull);
                }
                weapon = value;
            }
        }

        public bool IsAlive => Health > 0;

        public void AddWeapon(IWeapon weapon)
        {
            Weapon = weapon;
        }

        public void TakeDamage(int points)
        {
            int remainingArmour = Armour;
            int remaingHealth = Health;
            
            while(points > 0)
            {
                if(remainingArmour > 0)
                {
                    remainingArmour--;
                    points--;
                    continue;
                }

                remaingHealth--;
                points--;
                
            }

            if(remaingHealth <= 0)
            {
                remaingHealth = 0;
            }
            Armour = remainingArmour;
            Health = remaingHealth;
        }

        public override string ToString()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"{ this.GetType().Name }: { Name }");
            sb.AppendLine($"--Health: {Health }");
            sb.AppendLine($"--Armour: { Armour }");
            string weaponName = Weapon == null ? "Unarmed" : Weapon.Name;
            sb.AppendLine($"--Weapon: {weaponName}");

            return sb.ToString().TrimEnd();
        }
    }
}
