using NUnit.Framework;
using System;

namespace RepairShop.Tests
{
    public class Tests
    {
        Car car;
        Garage garage;
        
        [SetUp]
        public void Setup()
        {
            car = new Car("test", 5);
            garage = new Garage("talos", 10);
        }

        [Test]
        public void CarConstructorSetsValues()
        {
            car = new Car("test1", 5);
            Assert.That(car, Is.Not.Null);
            Assert.That(car.CarModel, Is.EqualTo("test1"));
            Assert.That(car.NumberOfIssues, Is.EqualTo(5));
        }

        [Test]
        public void CarIsFixedReturnsCorrectValue()
        {
            Assert.That(car.IsFixed, Is.False);
            Car car2 = new Car("opel", 0);
            Assert.That(car2.IsFixed, Is.True);
        }

        [Test]
        public void GarageConstructorSetsProperties()
        {
            garage = new Garage("testGarage", 5);
            Assert.That(garage, Is.Not.Null);
            Assert.That(garage.Name, Is.EqualTo("testGarage"));
            Assert.That(garage.MechanicsAvailable, Is.EqualTo(5));
        }

        [TestCase("")]
        [TestCase(null)]
        public void GarageConstructorNullExceptionIfNameNullOrEmpty(string name)
        {
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
            garage = new Garage(name, 10));
            Assert.That(ex.Message, Is.EqualTo("Invalid garage name. (Parameter 'value')"));
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-5)]
        public void GarageConstructorThrowsIfMechanicsZeroOrLess(int mechanics)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
            garage = new Garage("test", mechanics));
            Assert.That(ex.Message, Is.EqualTo("At least one mechanic must work in the garage."));
        }

        [Test]
        public void CountReturnsZeroWhenEmpty()
        {
            Assert.That(garage.CarsInGarage, Is.EqualTo(0));
            
        }

        [Test]
        public void AddIncreasesCarsInGarage()
        {
            garage.AddCar(car);
            Assert.That(garage.CarsInGarage, Is.EqualTo(1));
        }

        [Test]
        public void AddThrowsIfAtCapacity()
        {
            for (int i = 0; i < 10; i++)
            {
                garage.AddCar(new Car($"car{i}", 5));
            }
            
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            garage.AddCar(new Car("testThrow", 5)));
        }

        [Test]
        public void FixCarSetsIssuesTo0AndReturnsCar()
        {
            garage.AddCar(car);
            Car result = garage.FixCar("test");
            Assert.That(car.NumberOfIssues, Is.EqualTo(0));
            Assert.That(result, Is.SameAs(car));
        }

        [Test]
        public void FixCarThrowsIfCarNotFound()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            garage.FixCar("lambo"));
            Assert.That(ex.Message, Is.EqualTo("The car lambo doesn't exist."));
        }

        [Test]
        public void RemoveFixedCarsReturnsCorrectValueAndRemovesFixedCarsFromCollection()
        {
            Car car2 = new Car("opel", 0);
            Car car3 = new Car("VW", 4);
            garage.AddCar(car2);
            garage.AddCar(car3);
            garage.AddCar(car);

            Assert.That(garage.CarsInGarage, Is.EqualTo(3));
            garage.FixCar("VW");
            Assert.That(garage.CarsInGarage, Is.EqualTo(3));
            int result = garage.RemoveFixedCar();
            Assert.That(garage.CarsInGarage, Is.EqualTo(1));
            Assert.That(result, Is.EqualTo(2));





        }

        [Test]
        public void RemoveFixedCarsThrowsIfNoFixedCarsAvailable()
        {
            garage.AddCar(car);
            
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            garage.RemoveFixedCar());
            Assert.That(ex.Message, Is.EqualTo("No fixed cars available."));
        }

        [Test]
        public void ReportReturnsCorrectValue()
        {
            Car car2 = new Car("opel", 0);
            Car car3 = new Car("VW", 4);
            Car car4 = new Car("lada", 2);
            garage.AddCar(car2);
            garage.AddCar(car3);
            garage.AddCar(car);
            garage.AddCar(car4);
            garage.FixCar("lada");

            string expectedResult = "There are 2 which are not fixed: VW, test.";
        
            Assert.That(garage.Report, Is.EqualTo(expectedResult));
        }
    }
}