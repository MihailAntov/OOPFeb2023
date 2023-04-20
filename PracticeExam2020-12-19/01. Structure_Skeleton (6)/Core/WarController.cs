using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WarCroft.Constants;
using WarCroft.Entities.Characters;
using WarCroft.Entities.Characters.Contracts;
using WarCroft.Entities.Items;

namespace WarCroft.Core
{
	public class WarController
	{
		private List<Character> party;
		private List<Item> itemPool;

		private string[] allowedCharacterTypes =
		{
			nameof(Warrior),
			nameof(Priest)
		};

		private string[] allowedItemTypes =
		{
			nameof(HealthPotion),
			nameof(FirePotion)
		};

		public WarController()
		{
			party = new List<Character>();
			itemPool = new List<Item>();
		}

		public string JoinParty(string[] args)
		{
			string characterType = args[0];
			string name = args[1];
			if(!allowedCharacterTypes.Contains(characterType))
            {
				throw new ArgumentException(string.Format(ExceptionMessages.InvalidCharacterType,characterType));
            }
			Character character = null!;

			
			switch(characterType)
            {
				case nameof(Warrior):
					character = new Warrior(name);
					break;

				case nameof(Priest):
					character = new Priest(name);
					break;
					
			}
			party.Add(character);
			return string.Format(SuccessMessages.JoinParty, name);
		}

		public string AddItemToPool(string[] args)
		{
			string itemName = args[0];
			if (!allowedItemTypes.Contains(itemName))
            {
				throw new ArgumentException(string.Format(ExceptionMessages.InvalidItem, itemName));
            }

			Item item = null!;

            switch (itemName)
            {
				case nameof(HealthPotion):
					item = new HealthPotion();
					break;
				case nameof(FirePotion):
					item = new FirePotion();
					break;
            }

			itemPool.Add(item);
			return String.Format(SuccessMessages.AddItemToPool, itemName);
        }

        public string PickUpItem(string[] args)
		{
			string characterName = args[0];

			Character character = party.FirstOrDefault(c => c.Name == characterName);
			if(character == null)
            {
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, characterName));
            }

			if(!itemPool.Any())
            {
				throw new InvalidOperationException(ExceptionMessages.ItemPoolEmpty);
            }

			Item item = itemPool[itemPool.Count-1];
			character.Bag.AddItem(item);
			itemPool.Remove(item);

			return String.Format(SuccessMessages.PickUpItem, characterName, item.GetType().Name);
		}

		public string UseItem(string[] args)
		{
			string characterName = args[0];
			string itemName = args[1];
			Character character = party.FirstOrDefault(c => c.Name == characterName);
			if(character == null)
            {
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, characterName));
            }



			Item item = character.Bag.GetItem(itemName);
			

			character.UseItem(item);

			return string.Format(SuccessMessages.UsedItem, characterName, itemName);
		}

		public string GetStats()
		{
			StringBuilder sb = new StringBuilder();
			
			foreach(var character in party
				.OrderByDescending(c=>c.IsAlive)
				.ThenByDescending(c=>c.Health))
            {
				sb.AppendLine(character.ToString());
            }

			return sb.ToString().TrimEnd();
		}

		public string Attack(string[] args)
		{
			string attackerName = args[0];
			string receiverName = args[1];

			Character attacker = party.FirstOrDefault(c => c.Name == attackerName);
			if (attacker == null)
			{
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, attackerName));
			}

			Character receiver = party.FirstOrDefault(c => c.Name == receiverName);
			if (receiver == null)
			{
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, receiverName));
			}

			IAttacker actualAttacker = attacker as IAttacker;
			if(actualAttacker == null)
            {
				throw new ArgumentException(string.Format(ExceptionMessages.AttackFail, attackerName));
            }

			actualAttacker.Attack(receiver);
			StringBuilder sb = new StringBuilder();
			sb.AppendLine($"{attackerName} attacks {receiverName} for {attacker.AbilityPoints} hit points! {receiverName} has {receiver.Health}/{receiver.BaseHealth} HP and {receiver.Armor}/{receiver.BaseArmor} AP left!");
			if(!receiver.IsAlive)
            {
				sb.AppendLine($"{receiver.Name} is dead!");
            }

			return sb.ToString().TrimEnd();
		}

		public string Heal(string[] args)
		{
			string healerName = args[0];
			string receiverName = args[1];

			Character healer = party.FirstOrDefault(c => c.Name == healerName);
			if (healer == null)
			{
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, healerName));
			}

			Character receiver = party.FirstOrDefault(c => c.Name == receiverName);
			if (receiver == null)
			{
				throw new ArgumentException(string.Format(ExceptionMessages.CharacterNotInParty, receiverName));
			}

			IHealer actualHealer = healer as IHealer;
			if (actualHealer == null)
			{
				throw new ArgumentException(string.Format(ExceptionMessages.HealerCannotHeal, healerName));
			}

			actualHealer.Heal(receiver);
			return $"{healer.Name} heals {receiver.Name} for {healer.AbilityPoints}! {receiver.Name} has {receiver.Health} health now!";

		}
	}
}
