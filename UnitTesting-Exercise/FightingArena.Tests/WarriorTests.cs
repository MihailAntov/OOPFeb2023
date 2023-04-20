namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class WarriorTests
    {
        Warrior warrior;
        
        [Test]
        public void WarriorConstructorTest()
        {
            warrior = new Warrior("Conan",45, 50);
            Assert.That(warrior, Is.Not.Null);
            Assert.That(warrior.Name, Is.EqualTo("Conan"));
            Assert.That(warrior.HP, Is.EqualTo(50));
            Assert.That(warrior.Damage, Is.EqualTo(45));
            
        }

        [Test]
        [TestCase(" ")]
        [TestCase(null)]
        public void NameThrowsIfNullOrWhitespace(string name)
        {

            Assert.Throws<ArgumentException>(() => warrior = new Warrior(name, 40, 50));
        }

        [Test]
        [TestCase(-5)]
        public void HpThrowsIfLessThanZero(int hp)
        {
            Assert.Throws<ArgumentException>(() => warrior = new Warrior("Conan", 40, hp));

        }

        [Test]
        [TestCase(-1)]
        [TestCase(0)]
        [TestCase(-5)]
        public void DamageThrowsIfZeroOrLess(int damage)
        {
            Assert.Throws<ArgumentException>(() => warrior = new Warrior("Conan", damage, 50));

        }

        [Test]
        public void FightLowersHpOfBothPlayers()
        {
            Warrior attacker = new Warrior("Attacker", 50, 60);
            Warrior defender = new Warrior("Defender", 60, 250);
            attacker.Attack(defender);
            Assert.That(attacker.HP, Is.EqualTo(0));
            Assert.That(defender.HP, Is.EqualTo(200));
        }

        [Test]
        public void FightCantLowerHpBelowZero()
        {
            Warrior attacker = new Warrior("Attacker", 50, 300);
            Warrior defender = new Warrior("Defender", 60, 40);
            attacker.Attack(defender);
            Assert.That(attacker.HP, Is.EqualTo(240));
            Assert.That(defender.HP, Is.EqualTo(0));
        }

        [Test]
        public void FightThrowsIfHpLowerThanMinimum()
        {
            Warrior attacker = new Warrior("Attacker", 45, 25);
            Warrior defender = new Warrior("Defender", 50, 100);
            
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => attacker.Attack(defender));
            Assert.That(ex.Message, Is.EqualTo("Your HP is too low in order to attack other warriors!"));
        }

        [Test]
        public void FightThrowsIfEnemyHpLowerThanMinimum()
        {
            Warrior attacker = new Warrior("Attacker", 45, 50);
            Warrior defender = new Warrior("Defender", 50, 25);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => attacker.Attack(defender));
            Assert.That(ex.Message, Is.EqualTo($"Enemy HP must be greater than 30 in order to attack him!"));
        }
        [Test]
        public void FightThrowsIfAttackingEnemyWithDamageHigherThanYourHp()
        {
            Warrior attacker = new Warrior("Attacker", 45, 50);
            Warrior defender = new Warrior("Defender", 55, 100);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => attacker.Attack(defender));
            Assert.That(ex.Message, Is.EqualTo($"You are trying to attack too strong enemy"));
        }
    }
}