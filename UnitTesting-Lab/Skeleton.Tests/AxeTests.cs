using NUnit.Framework;
using System;

namespace Skeleton.Tests
{
    [TestFixture]
    public class AxeTests
    {
        [Test]
        public void AxeLosesDurabilityAfterAttack()
        {
            const int StartingAxeDurability = 10;
            const int DurabilityLostPerAttack = 1;

            Axe axe = new Axe(10, StartingAxeDurability);
            Dummy dummy = new Dummy(10, 10);

            axe.Attack(dummy);

            Assert.That(axe.DurabilityPoints, Is.EqualTo(StartingAxeDurability - DurabilityLostPerAttack),"Axe is not losing durability properly.");
        }
        [Test]
        public void AxeCantAttackWithoutDurability()
        {
            const int  StartingAxeDurability = 0;

            Axe axe = new Axe(10, StartingAxeDurability);
            Dummy dummy = new Dummy(10, 10);

            Assert.Throws<InvalidOperationException>(() => axe.Attack(dummy),"Axe is not throwing an exception properly when attacking with broken weapon.");


        }
    }
}