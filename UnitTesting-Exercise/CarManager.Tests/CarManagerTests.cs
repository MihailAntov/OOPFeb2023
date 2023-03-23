namespace CarManager.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class CarManagerTests
    {
        Car car;
        
        [SetUp]
        public void SetUp()
        {
            car = new Car("make", "model", 5, 50);
        }

        [TearDown]
        public void TearDown()
        {
            car = null;
        }

        [Test]
        public void ConstructorsSetProperties()
        {
            Car car = new Car("make", "model", 10, 15);
            Assert.That(car.Make, Is.EqualTo("make"));
            Assert.That(car.Model, Is.EqualTo("model"));
            Assert.That(car.FuelConsumption, Is.EqualTo(10));
            Assert.That(car.FuelCapacity, Is.EqualTo(15));
            Assert.That(car.FuelAmount, Is.EqualTo(0));
        }

        [Test]
        public void ConstructorExceptions()
        {
            Assert.Throws<ArgumentException>(() => new Car(null, "model", 10, 15));
            Assert.Throws<ArgumentException>(() => new Car(string.Empty, "model", 10, 15));
            Assert.Throws<ArgumentException>(() => new Car("make", null, 10, 15));
            Assert.Throws<ArgumentException>(() => new Car("make", string.Empty, 10, 15));
            Assert.Throws<ArgumentException>(() => new Car("make", "model", 0, 15));
            Assert.Throws<ArgumentException>(() => new Car("make", "model", -15, 15));
            Assert.Throws<ArgumentException>(() => new Car("make", "model", 2, 0));
            Assert.Throws<ArgumentException>(() => new Car("make", "model", 2, -10));

        }

        [Test]
        public void RefuelThrowsIfNegativeAmountOrZero()
        {
            Assert.Throws<ArgumentException>(() => car.Refuel(0));
            Assert.Throws<ArgumentException>(() => car.Refuel(-5));
        }

        [Test]
        public void RefuelAddsFuelAmount()
        {
            car.Refuel(10);
            Assert.That(car.FuelAmount, Is.EqualTo(10));
        }

        [Test]
        public void RefuelCantExceedCapacity()
        {
            car.Refuel(70);
            Assert.That(car.FuelAmount, Is.EqualTo(car.FuelCapacity));
        }

        [Test]
        public void DriveReducesFuelAmount()
        {
            car.Refuel(40);
            Assert.That(car.FuelAmount, Is.EqualTo(40));
            car.Drive(100);
            Assert.That(car.FuelAmount, Is.EqualTo(35));
        }

        [Test]
        public void CantDriveWithoutSufficientFuel()
        {
            car = new Car("make", "model", 1, 20);
            car.Refuel(5);
            Assert.Throws<InvalidOperationException>(() => car.Drive(501));
        }
    }
}