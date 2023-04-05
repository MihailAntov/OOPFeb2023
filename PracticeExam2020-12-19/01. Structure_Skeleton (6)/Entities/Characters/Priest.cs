using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WarCroft.Constants;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Inventory;

namespace WarCroft.Entities.Characters
{
    internal class Priest : Character, IHealer
    {
        private const double PriestHealth = 50;
        private const double PriestArmor = 25;
        private const double PriestAbilityPoints = 40;
        public Priest(string name) 
            : base(name, PriestHealth, PriestArmor, PriestAbilityPoints, new Backpack())
        {

        }

        public void Heal(Character character)
        {
            EnsureAlive();
            if(!character.IsAlive)
            {
                throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
            }

            character.Health += AbilityPoints;
        }
    }
}
