using System;

using WarCroft.Constants;
using WarCroft.Entities.Inventory;
using WarCroft.Entities.Items;

namespace WarCroft.Entities.Characters.Contracts
{
    public abstract class Character
    {
		// TODO: Implement the rest of the class.
		private string name;
		private double health;
		private double armor;
        public Character(string name, double health, double armor, double abilityPoints, Bag bag)
        {
			Name = name;
			BaseHealth = health;
			Health = health;
			BaseArmor = armor;
			Armor = armor;
			AbilityPoints = abilityPoints;
			Bag = bag;
        }
		public string Name
        {
			get { return name; }
			private set
            {
				if(string.IsNullOrWhiteSpace(value))
                {
					throw new ArgumentException(ExceptionMessages.CharacterNameInvalid);
                }
				name = value;
            }
        }
		public double BaseHealth { get; private set; }
		public double Health
        {
			get { return health; }
			set
            {
				health = value;

				if(health > BaseHealth)
                {
					health = BaseHealth;
                }

				if(health <= 0)
                {
					health = 0;
					IsAlive = false;
                }

            }
        }
		public double BaseArmor { get; private set; }
		public double Armor
        {
			get { return armor; }
			private set
            {
				armor = value;
				if(armor > BaseArmor)
                {
					armor = BaseArmor;
                }
            }
        }

		public double AbilityPoints { get; private set; }
		public IBag Bag { get; private set; }

		public bool IsAlive { get; set; } = true;

		protected void EnsureAlive()
		{
			if (!this.IsAlive)
			{
				throw new InvalidOperationException(ExceptionMessages.AffectedCharacterDead);
			}
		}

		public void TakeDamage(double hitPoints)
        {
			EnsureAlive();
			double currentArmor = Armor;
			double currentHp = Health;
			while(hitPoints > 0)
            {
				if(currentArmor > 0)
                {
					currentArmor--;
					hitPoints--;
					continue;
                }
				currentHp--;
				hitPoints--;
            }

			Armor = currentArmor;
			Health = currentHp;
        }

		public void UseItem(Item item)
        {
			EnsureAlive();
			item.AffectCharacter(this);
        }

        public override string ToString()
        {
			string status = IsAlive ? "Alive" : "Dead";
			return $"{Name} - HP: {Health}/{BaseHealth}, AP: {Armor}/{BaseArmor}, Status: {status}";

		}
    }
}