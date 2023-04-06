using NUnit.Framework;
using System;

namespace Aquariums.Tests
{
    public class Tests
    {
        private Fish fish;
        private Aquarium aquarium;
        
        [SetUp]
        public void Setup()
        {
            fish = new Fish("nemo");
            aquarium = new Aquarium("ocean", 10);
        }

        [Test]
        public void FishConstructorSetsProperties()
        {
            fish = new Fish("joro");
            Assert.That(fish, Is.Not.Null);
            Assert.That(fish.Name, Is.EqualTo("joro"));
            Assert.That(fish.Available, Is.True);
        }

        [Test]
        public void AquariumConstructorSetsProperties()
        {
            aquarium = new Aquarium("lake", 5);
            Assert.That(aquarium, Is.Not.Null);
            Assert.That(aquarium.Name, Is.EqualTo("lake"));
            Assert.That(aquarium.Capacity, Is.EqualTo(5));
            Assert.That(aquarium.Count, Is.EqualTo(0));
        }

        
        [TestCase(-1)]
        [TestCase(-300)]
        public void AquariumConstructorThrowsIfCapacityLessThanZero(int capacity)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
            aquarium = new Aquarium("lake", capacity));
            Assert.That(ex.Message, Is.EqualTo("Invalid aquarium capacity!"));
        }

        [TestCase("")]
        [TestCase(null)]
        public void AquariumConstructorThrowsIfNameNull(string name)
        {
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
            aquarium = new Aquarium(name, 10));
            Assert.That(ex.Message, Is.EqualTo("Invalid aquarium name! (Parameter 'value')"));
        }

        [Test]
        public void CountIncreasesOnAdd()
        {
            Assert.That(aquarium.Count, Is.EqualTo(0));
            aquarium.Add(fish);
            Assert.That(aquarium.Count, Is.EqualTo(1));
        }


        [Test]
        public void AddThrowsIfAtCapacity()
        {
            for(int i = 0; i < aquarium.Capacity; i++)
            {
                aquarium.Add(new Fish($"fish{i + 1}"));
            }
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            aquarium.Add(fish));
            Assert.That(ex.Message, Is.EqualTo("Aquarium is full!"));
        }

        [Test]
        public void RemoveRemovesFish()
        {
            aquarium.Add(fish);
            Assert.That(aquarium.Count, Is.EqualTo(1));
            aquarium.RemoveFish("nemo");
            Assert.That(aquarium.Count, Is.EqualTo(0));
        }

        [Test]
        public void RemoveThrowsIfFishNotFound()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            aquarium.RemoveFish("shark"));
            Assert.That(ex.Message, Is.EqualTo("Fish with the name shark doesn't exist!"));
        }

        [Test]
        public void SellSetsAvailableToFalseAndReturnsFish()
        {
            aquarium.Add(fish);
            Fish result = aquarium.SellFish("nemo");
            Assert.That(aquarium.Count, Is.EqualTo(1));
            Assert.That(fish.Available, Is.False);
            Assert.That(result, Is.SameAs(fish));
        }

        [Test]
        public void SellThrowsIfFishNotFound()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            aquarium.SellFish("nemo"));
            Assert.That(ex.Message, Is.EqualTo("Fish with the name nemo doesn't exist!"));
        }

        [Test]
        public void ReportReturnsCorrectValue()
        {
            Fish fish2 = new Fish("shark");
            Fish fish3 = new Fish("fin");
            aquarium.Add(fish);
            aquarium.Add(fish2);
            aquarium.Add(fish3);
            string expectedResult = "Fish available at ocean: nemo, shark, fin";
            Assert.That(aquarium.Report(), Is.EqualTo(expectedResult));
        }
    }
}