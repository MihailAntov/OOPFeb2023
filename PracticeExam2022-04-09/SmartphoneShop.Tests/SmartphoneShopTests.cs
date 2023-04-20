using NUnit.Framework;
using System;

namespace SmartphoneShop.Tests
{
    [TestFixture]
    public class SmartphoneShopTests
    {
        Smartphone smartphone;
        Shop shop;
        [SetUp]
        public void SetUp()
        {
            smartphone = new Smartphone("model1", 10);
            shop = new Shop(3);

        }

        [Test]
        public void SmarthPhoneConstructorSetsProperties()
        {
            Smartphone smartphoneTest = new Smartphone("testName", 10);
            Assert.That(smartphoneTest.ModelName, Is.EqualTo("testName"));
            Assert.That(smartphoneTest.MaximumBatteryCharge, Is.EqualTo(10));
            Assert.That(smartphoneTest.CurrentBateryCharge, Is.EqualTo(10));
        }

        [Test]
        public void ShopConstructorSetsProperties()
        {
            Shop shopTest = new Shop(5);
            Assert.That(shopTest, Is.Not.Null);
            Assert.That(shopTest.Capacity, Is.EqualTo(5));
            Assert.That(shopTest.Count, Is.EqualTo(0));
        }

        [TestCase(-1)]
        [TestCase(-25)]
        public void ShopConstructorThrowsIfCapacityLessThanZero(int capacity)
        {
            Shop shopTest;
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
            shopTest = new Shop(capacity));
            Assert.That(ex.Message, Is.EqualTo("Invalid capacity."));
        }

        [Test]
        public void AddPhoneAddsCorrectly()
        {
            shop.Add(smartphone);
            Assert.That(shop.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddPhoneThrowsIfAtCapacity()
        {
            shop.Add(smartphone);
            shop.Add(new Smartphone("model2", 10));
            shop.Add(new Smartphone("model3", 10));
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            shop.Add(new Smartphone("model4", 10)));
            Assert.That(ex.Message, Is.EqualTo("The shop is full."));
        }

        [Test]
        public void AddPhoneThrowsIfPhoneModelAlreadyExists()
        {
            shop.Add(smartphone);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            shop.Add(new Smartphone("model1", 15)));
            Assert.That(ex.Message, Is.EqualTo("The phone model model1 already exist."));
        }

        [Test]
        public void RemovePhoneRemovesItemFromCollection()
        {
            Assert.That(shop.Count, Is.EqualTo(0));
            shop.Add(smartphone);
            Assert.That(shop.Count, Is.EqualTo(1));
            shop.Remove("model1");
            Assert.That(shop.Count, Is.EqualTo(0));

        }

        [Test]
        public void RemovePhoneThrowsIfPhoneDoesntExist()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            shop.Remove("model1"));
            Assert.That(ex.Message, Is.EqualTo("The phone model model1 doesn't exist."));
        }

        [Test]
        public void TestPhoneThrowsIfPhoneNotFound()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException> (()=> shop.TestPhone("model1", 10));
            Assert.That(ex.Message, Is.EqualTo("The phone model model1 doesn't exist."));
        }

        [Test]
        public void TestPhoneThrowsIfChargeInsufficient()
        {
            shop.Add(smartphone);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            shop.TestPhone("model1", 11));
            Assert.That(ex.Message, Is.EqualTo("The phone model model1 is low on batery."));
        }

        [Test]
        public void TestPhoneCorrectlyLowersCharge()
        {
            shop.Add(smartphone);
            shop.TestPhone("model1", 5);
            Assert.That(smartphone.CurrentBateryCharge, Is.EqualTo(5));
        }

        [Test]
        public void ChargePhoneThrowsIfPhoneNotFound()
        {
            shop.Add(smartphone);
            shop.TestPhone("model1", 3);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            shop.ChargePhone("model2"));
            Assert.That(ex.Message, Is.EqualTo("The phone model model2 doesn't exist."));
        }

        [Test]
        public void ChargePhoneCorrectlyIncreasesCharge()
        {
            shop.Add(smartphone);
            Assert.That(smartphone.CurrentBateryCharge, Is.EqualTo(smartphone.MaximumBatteryCharge));

            shop.TestPhone("model1", 5);
            Assert.That(smartphone.CurrentBateryCharge, Is.EqualTo(smartphone.MaximumBatteryCharge-5));

            shop.ChargePhone("model1");
            Assert.That(smartphone.CurrentBateryCharge, Is.EqualTo(smartphone.MaximumBatteryCharge));
        }
    }
}