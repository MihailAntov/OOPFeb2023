using NUnit.Framework;
using System;

namespace TheRace.Tests
{
    public class Tests
    {
        UnitCar car;
        UnitDriver driver;
        RaceEntry race;
        
        [SetUp]
        public void Setup()
        {
            car = new UnitCar("opel", 10, 30);
            driver = new UnitDriver("ivan", car);
            race = new RaceEntry();
        }

        [Test]
        public void UnitCarConstructorSetsProperties()
        {
            car = new UnitCar("testModel", 10, 50);
            Assert.That(car, Is.Not.Null);
            Assert.That(car.CubicCentimeters, Is.EqualTo(50));
            Assert.That(car.HorsePower, Is.EqualTo(10));
            Assert.That(car.Model, Is.EqualTo("testModel"));
        }

        [Test]
        public void UnitDriverConstructorSetsProperties()
        {
            driver = new UnitDriver("testDriver", car);
            Assert.That(driver, Is.Not.Null);
            Assert.That(driver.Name, Is.EqualTo("testDriver"));
            Assert.That(driver.Car, Is.SameAs(car));
        }

        [Test]
        public void UnitDriverConstructorWorksWithNullCar()
        {
            driver = new UnitDriver("testDriver", null);
            Assert.That(driver, Is.Not.Null);
            Assert.That(driver.Name, Is.EqualTo("testDriver"));
            Assert.That(driver.Car, Is.Null);
        }

        [Test]
        public void UnitDriverConstructorThrowsIfNameNull()
        {
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
            driver = new UnitDriver(null, car));
            Assert.That(ex.Message, Is.EqualTo("Name cannot be null! (Parameter 'Name')"));
        
            ex = Assert.Throws<ArgumentNullException>(() =>
            driver = new UnitDriver(null, null));
            Assert.That(ex.Message, Is.EqualTo("Name cannot be null! (Parameter 'Name')"));

        }

        [Test]
        public void RaceEntryConstructorWorksProperly()
        {
            race = new RaceEntry();
            Assert.That(race, Is.Not.Null);
        }

        [Test]
        public void CountReturnsZeroWhenEmpty()
        {
            Assert.That(race.Counter, Is.EqualTo(0));
        }

        [Test]
        public void AddDriverAddsDriverToCollectionAndReturnsCorrectValue()
        {
            string result = race.AddDriver(driver);
            string expectedResult = "Driver ivan added in race.";
            
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void AddDriverThrowsIfDriverIsNull()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            race.AddDriver(null));
            Assert.That(ex.Message, Is.EqualTo("Driver cannot be null."));
        }

        [Test]
        public void AddDriverThrowsIfDriverAlreadyAdded()
        {
            race.AddDriver(driver);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            race.AddDriver(new UnitDriver("ivan", null)));
            Assert.That(ex.Message, Is.EqualTo("Driver ivan is already added."));
        }

        [TestCase(5,10,15,10)]
        [TestCase(10,10,10,10)]
        [TestCase(4,4,16,8)]
        public void CalculateAverageHorsePowerReturnsCorrectValue(int hp1, int hp2, int hp3, double avg)
        {
            UnitCar car1 = new UnitCar("model1", hp1,20);
            UnitCar car2 = new UnitCar("model2", hp2,20);
            UnitCar car3 = new UnitCar("model3", hp3,20);


            
            UnitDriver driver1 = new UnitDriver("driver1", car1);
            UnitDriver driver2 = new UnitDriver("driver2", car2);
            UnitDriver driver3 = new UnitDriver("driver3", car3);

            race.AddDriver(driver1);
            race.AddDriver(driver2);
            race.AddDriver(driver3);

            double expectedAverage = avg;
            double average = race.CalculateAverageHorsePower();
            Assert.That(average, Is.EqualTo(expectedAverage));
        }

        [Test]
        public void CalculateAverageHorsePowerThrowsIfNotEnoughParticipants()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()=>
            race.CalculateAverageHorsePower());
            Assert.That(ex.Message, Is.EqualTo("The race cannot start with less than 2 participants."));
        }
    }
}