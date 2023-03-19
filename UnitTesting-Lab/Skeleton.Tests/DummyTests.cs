using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class DummyTests
    {
        [Test]
        public void DummyLosesHealthIfAttacked()
        {
            const int StartingHealth = 10;
            const int Attack = 5;
            Dummy dummy = new Dummy(StartingHealth, 10);
            dummy.TakeAttack(Attack);

            Assert.That(dummy.Health, Is.EqualTo(StartingHealth - Attack),"Dummy is not losing health properly.");
        }

        [Test]
        public void DeadDummyThrowsExceptionIfAttacked()
        {
            const int StartingHealth = 0;
            const int Attack = 5;
            Dummy dummy = new Dummy(StartingHealth, 10);

            Assert.Throws<InvalidOperationException>(() => dummy.TakeAttack(Attack),"Dead dummy can't be attacked.");
        }

        [Test]
        public void DeadDummyCanGiveXp()
        {
            const int StartingHealth = 0;
            const int DummyXpReward = 10;
            Dummy dummy = new Dummy(StartingHealth, DummyXpReward);
            int xp = dummy.GiveExperience();

            Assert.That(xp, Is.EqualTo(DummyXpReward),"A dead dummy can give xp");
        }

        [Test]
        public void AliveDUmmyCantGiveXp()
        {
            const int StartingHealth = 5;
            const int DummyXpReward = 10;
            Dummy dummy = new Dummy(StartingHealth, DummyXpReward);

            Assert.Throws<InvalidOperationException>(() => dummy.GiveExperience(),"An alive dummy can't give xp");
        }
    }
}