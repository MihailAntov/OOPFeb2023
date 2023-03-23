namespace FightingArena.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;

    [TestFixture]
    public class ArenaTests
    {
        Arena arena;
        Warrior attacker;
        Warrior defender;
        [SetUp]
        public void SetUp()
        {
            arena = new Arena();
            attacker = new Warrior("Attacker", 40, 100);
            defender = new Warrior("Defender", 35, 120);
        }
        
        [Test]
        public void TestConstructor()
        {
            arena = new Arena();
            Assert.That(arena, Is.Not.Null);
            Assert.That(arena.Warriors.Count, Is.EqualTo(0));
            Assert.That(arena.Count, Is.EqualTo(0));
        }

        [Test]
        public void EnrollAddsWarriorToCollection()
        {
            arena.Enroll(attacker);
            Assert.That(arena.Count, Is.EqualTo(1));
            Assert.That(arena.Warriors.Contains(attacker), Is.True);
        }

        [Test]
        public void EnrollThrowsIfNameAlreadyExistsInCollection()
        {
            arena.Enroll(attacker);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => arena
            .Enroll(new Warrior("Attacker", 50, 150)));
            Assert.That(ex.Message, Is.EqualTo("Warrior is already enrolled for the fights!"));
        }

        [Test]
        public void FightBeginsFightWithBothWarriors()
        {
            arena.Enroll(attacker);
            arena.Enroll(defender);
            arena.Fight("Attacker", "Defender");
            Assert.That(attacker.HP, Is.EqualTo(65));
            Assert.That(defender.HP, Is.EqualTo(80));
        }

        [Test]
        public void FightThrowsIfAttackerIsMissing()
        {
            arena.Enroll(defender);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => arena
            .Fight("Attacker", "Defender"));

            Assert.That(ex.Message, Is.EqualTo("There is no fighter with name Attacker enrolled for the fights!"));
        }

        [Test]
        [TestCase("Attacker","Missing")]
        [TestCase("AlsoMissing","Missing")]
        public void FightThrowsIfDefenderOrBothAreMissing(string attackerName, string defenderName)
        {
            arena.Enroll(attacker);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => arena
            .Fight(attackerName, defenderName));
            Assert.That(ex.Message, Is.EqualTo("There is no fighter with name Missing enrolled for the fights!"));
        }
    }
}
