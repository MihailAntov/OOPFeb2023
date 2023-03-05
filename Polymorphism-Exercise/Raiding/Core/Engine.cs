using System;
using System.Collections.Generic;
using System.Linq;
using Raiding.Models;
using Raiding.Models.Interfaces;

namespace Raiding.Core
{
    public class Engine
    {
        private List<IBaseHero> heroes;

        public Engine()
        {
            heroes = new List<IBaseHero>();
        }

        public void Run()
        {
            CreateHeroes();
            HandleFight();
        }


        public void CreateHeroes()
        {
            int numberOfHeroes = int.Parse(Console.ReadLine());

            while (heroes.Count < numberOfHeroes)
            {
                CreateHero();
            }

        }

        public void CreateHero()
        {
            string heroName = Console.ReadLine();
            string heroClass = Console.ReadLine();
            IBaseHero hero = null;
            switch (heroClass)
            {
                case "Paladin":
                    hero = new Paladin(heroName);
                    break;
                case "Druid":
                    hero = new Druid(heroName);
                    break;
                case "Rogue":
                    hero = new Rogue(heroName);
                    break;
                case "Warrior":
                    hero = new Warrior(heroName);
                    break;
            }

            if (hero == null)
            {
                Console.WriteLine("Invalid hero!");
                return;
            }

            heroes.Add(hero);
        }
        private void HandleFight()
        {
            int bossPower = int.Parse(Console.ReadLine());

            foreach (IBaseHero hero in heroes)
            {
                Console.WriteLine(hero.CastAbility());
            }

            int raidPower = heroes.Select(h => h.Power).Sum();

            if (raidPower >= bossPower)
            {
                Console.WriteLine("Victory!");
                return;
            }

            Console.WriteLine("Defeat...");
        }



    }
}
