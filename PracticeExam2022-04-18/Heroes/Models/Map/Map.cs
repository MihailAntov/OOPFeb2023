using Heroes.Models.Contracts;
using Heroes.Models.Heroes;
using Heroes.Utilities.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heroes.Models.Map
{
    public class Map : IMap
    {
        public string Fight(ICollection<IHero> players)
        {
            List<IHero> barbarianTeam = new List<IHero>();
            List<IHero> knightTeam = new List<IHero>();

            foreach(IHero hero in players)
            {
                if(hero.GetType() == typeof(Barbarian))
                {
                    barbarianTeam.Add(hero);
                }

                if(hero.GetType() == typeof(Knight))
                {
                    knightTeam.Add(hero);
                }
            }

            while(barbarianTeam.Any(b=>b.IsAlive) && knightTeam.Any(k=>k.IsAlive))
            {
                foreach(IHero livingKnight in knightTeam.Where(k=>k.IsAlive))
                {
                        int knightDamage = livingKnight.Weapon.DoDamage();
                    foreach(IHero livingBarbarian in barbarianTeam.Where(b => b.IsAlive))
                    {
                        livingBarbarian.TakeDamage(knightDamage);

                        //livingBarbarian.TakeDamage(livingKnight.Weapon.DoDamage());


                    }
                }


                foreach (IHero livingBarbarian in barbarianTeam.Where(b => b.IsAlive))
                {
                        int barbarianDamage = livingBarbarian.Weapon.DoDamage();
                    foreach(IHero livingKnight in knightTeam.Where(k => k.IsAlive))
                    {
                        //livingKnight.TakeDamage(livingBarbarian.Weapon.DoDamage());
                        livingKnight.TakeDamage(barbarianDamage);
                    }
                }
            }

            int remainingKnights = knightTeam.Where(k => k.IsAlive).Count();
            int remainingBarbarians = barbarianTeam.Where(b => b.IsAlive).Count();

            if(remainingKnights > remainingBarbarians)
            {
                return String.Format(OutputMessages.MapFightKnightsWin,knightTeam.Count - remainingKnights);
            }
            else
            {
                return String.Format(OutputMessages.MapFigthBarbariansWin, barbarianTeam.Count - remainingBarbarians);
            }
        }
    }
}
