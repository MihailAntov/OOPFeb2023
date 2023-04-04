using Heroes.Core.Contracts;
using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using Heroes.Models.Map;
using Heroes.Models.Weapons;
using Heroes.Repositories;
using Heroes.Repositories.Contracts;
using Heroes.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Heroes.Core
{
    public class Controller : IController
    {
        private IRepository<IHero> heroes;
        private IRepository<IWeapon> weapons;

        private string[] existingHeroTypes =
        {
            nameof(Barbarian),
            nameof(Knight)
        };

        private string[] existingWeaponTypes =
        {
            nameof(Claymore),
            nameof(Mace)
        };

        public Controller()
        {
            heroes = new HeroRepository();
            weapons = new WeaponRepository();
        }
        
        public string AddWeaponToHero(string weaponName, string heroName)
        {
            IHero hero = heroes.FindByName(heroName);
            if(hero == null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroDoesNotExist,heroName));
            }

            IWeapon weapon = weapons.FindByName(weaponName);
            if(weapon == null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.WeaponDoesNotExist, weaponName));
            }

            if(hero.Weapon != null)
            {
                throw new InvalidOperationException(string.Format(OutputMessages.HeroAlreadyHasWeapon, heroName));
            }

            hero.AddWeapon(weapon);
            weapons.Remove(weapon);

            return string.Format(OutputMessages.WeaponAddedToHero, heroName, weapon.GetType().Name.ToLower());
        }

        public string CreateHero(string type, string name, int health, int armour)
        {
            if (heroes.Models.Any(h=>h.Name == name))
            {
                throw new InvalidOperationException(String.Format(OutputMessages.HeroAlreadyExist, name));
            }

            if(!existingHeroTypes.Contains(type))
            {
                throw new InvalidOperationException(OutputMessages.HeroTypeIsInvalid);
            }

            IHero hero = null!;
            string result = string.Empty;
            switch(type)
            {
                case nameof(Knight):
                    hero = new Knight(name, health, armour);
                    result = string.Format(OutputMessages.SuccessfullyAddedKnight, name);
                    break;
                case nameof(Barbarian):
                    hero = new Barbarian(name, health, armour);
                    result = string.Format(OutputMessages.SuccessfullyAddedBarbarian, name);
                    break;
            }

            heroes.Add(hero);
            return result;
        }

        public string CreateWeapon(string type, string name, int durability)
        {
            if(weapons.Models.Any(h=>h.Name == name))
            {
                throw new InvalidOperationException(String.Format(OutputMessages.WeaponAlreadyExists, name));
            }

            if(!existingWeaponTypes.Contains(type))
            {
                throw new InvalidOperationException(OutputMessages.WeaponTypeIsInvalid);
            }

            IWeapon weapon = null!;
            switch(type)
            {
                case nameof(Mace):
                    weapon = new Mace(name, durability);
                    break;
                case nameof(Claymore):
                    weapon = new Claymore(name, durability);
                    break;
            }

            weapons.Add(weapon);
            return String.Format(OutputMessages.WeaponAddedSuccessfully, type.ToLower(), name);
        }

        public string HeroReport()
        {
            StringBuilder sb = new StringBuilder();
            foreach(var hero in heroes.Models
                .OrderBy(h=>h.GetType().Name)
                .ThenByDescending(h=>h.Health)
                .ThenBy(h=>h.Name))
            {
                sb.AppendLine(hero.ToString());
            }

            return sb.ToString().TrimEnd();
        }

        public string StartBattle()
        {
            IMap map = new Map();
            ICollection<IHero> fightParticipants = heroes.Models
                .Where(h => h.Weapon != null && h.IsAlive)
                .ToList();
             return  map.Fight(heroes.Models.Where(h => h.Weapon != null && h.IsAlive).ToList());
        }
    }
}
