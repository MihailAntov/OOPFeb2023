using NUnit.Framework;
using System;
using System.Linq;

namespace Presents.Tests
{
    public class Tests
    {
        Present present;
        Bag bag;
        
        [SetUp]
        public void Setup()
        {
            present = new Present("horse", 10);
            bag = new Bag();
        }

        [Test]
        public void PresentConstructorSetsProperties()
        {
            present = new Present("toy", 3);
            Assert.That(present, Is.Not.Null);
            Assert.That(present.Magic, Is.EqualTo(3));
            Assert.That(present.Name, Is.EqualTo("toy"));
            
        }

        [Test]
        public void BagConstructorCreatesBagWithCount0()
        {
            bag = new Bag();
            Assert.That(bag, Is.Not.Null);
            Assert.That(bag.GetPresents(), Is.Not.Null);
            Assert.That(bag.GetPresents().Count, Is.EqualTo(0));
        }

        [Test]
        public void CreateAddsPresentToBagAndReturnsCorrectValue()
        {
            string actualResult = bag.Create(present);
            
            string expectedResult = "Successfully added present horse.";
            Assert.That(actualResult, Is.EqualTo(expectedResult));
        }

        [Test]
        public void CreateThrowsIfPresentIsNull()
        {
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
            bag.Create(null));
            Assert.That(ex.Message, Is.EqualTo("Value cannot be null. (Parameter 'Present is null')"));
        }

        [Test]
        public void CreateThrowsIfPresentAlreadyAdded()
        {
            bag.Create(present);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            bag.Create(present));
            Assert.That(ex.Message, Is.EqualTo("This present already exists!"));
        }

        [Test]
        public void RemoveRemovesPresentFromBagAndReturnsTrue()
        {
            bag.Create(present);
            Assert.That(bag.GetPresents().Count, Is.EqualTo(1));
            bool result = bag.Remove(present);
            Assert.That(bag.GetPresents().Count, Is.EqualTo(0));
            Assert.That(result, Is.True);
        }

        [Test]
        public void RemoveReturnsFalseIfPresentNotFound()
        {
            bool result = bag.Remove(present);
            Assert.That(result, Is.False);
        }

        [Test]
        public void GetPresentReturnsCorrectPresent()
        {
            bag.Create(present);
            Present result = bag.GetPresent("horse");
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.SameAs(present));
        }

        [Test]
        public void GetPresentReturnsNullIfNotFOund()
        {
            Present result = bag.GetPresent("horse");
            Assert.That(result, Is.Null);
        }

        [Test]
        public void GetPresentWithLeastMagicReturnsCorrectPresent()
        {
            Present present2 = new Present("doll", 20);
            Present present3 = new Present("coal", 1);
            bag.Create(present);
            bag.Create(present2);
            bag.Create(present3);
            Present result = bag.GetPresentWithLeastMagic();
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.SameAs(present3));
        }

        [Test]
        public void GetPresentWithLeastMagicReturnsNullIfEmptyBag()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => bag.GetPresentWithLeastMagic());

        }

        [Test]
        public void GetPresentsReturnsWholeCollection()
        {
            Present present2 = new Present("doll", 20);
            Present present3 = new Present("coal", 1);
            bag.Create(present);
            bag.Create(present2);
            bag.Create(present3);

            var presents = bag.GetPresents();
            Assert.That(presents.Count, Is.EqualTo(3));
            Assert.That(presents.Contains(present));
            Assert.That(presents.Contains(present2));
            Assert.That(presents.Contains(present3));
        }
    }
}