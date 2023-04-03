using System;

namespace Computers.Tests2
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ComputerConstructorWorksProperly()
        {
            Computer computer = new Computer("manufacturer1", "model1", 20.10M);
            Assert.That(computer, Is.Not.Null);
            Assert.That(computer.Manufacturer, Is.EqualTo("manufacturer1"));
            Assert.That(computer.Model, Is.EqualTo("model1"));
            Assert.That(computer.Price, Is.EqualTo(20.10M));
        }

        [Test]
        public void ComputerManagerCreatesEmptyCollection()
        {
            ComputerManager manager = new ComputerManager();
            Assert.That(manager.Computers.Count, Is.EqualTo(0));
            Assert.That(manager, Is.Not.Null);
            Assert.That(manager.Computers, Is.Not.Null);
            Assert.That(manager.Count, Is.EqualTo(0));

        }


        [Test]
        public void AddComputerThrowsIfNullValue()
        {
            Computer computer = null;

            ComputerManager manager = new ComputerManager();
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() => manager.AddComputer(computer));
            Assert.That(ex.Message, Is.EqualTo("Can not be null! (Parameter 'computer')"));

            ArgumentNullException ex2 = Assert.Throws<ArgumentNullException>(() => manager.GetComputersByManufacturer(null));
            Assert.That(ex2.Message, Is.EqualTo("Can not be null! (Parameter 'manufacturer')"));

            ArgumentNullException ex3 = Assert.Throws<ArgumentNullException>(() => manager.GetComputer(null,"Model"));
            Assert.That(ex3.Message, Is.EqualTo("Can not be null! (Parameter 'manufacturer')"));

            ArgumentNullException ex4 = Assert.Throws<ArgumentNullException>(() => manager.GetComputer("Manufacturer", null));
            Assert.That(ex4.Message, Is.EqualTo("Can not be null! (Parameter 'model')"));
        }

        [Test]
        public void AddComputerThrowsIfComputerAlreadyAdded()
        {
            Computer computer = new Computer("manufacturerOne", "modelOne", 100);

            Computer sameComputer = new Computer("manufacturerOne", "modelOne", 200);

            ComputerManager manager = new ComputerManager();
            manager.AddComputer(computer);

            Exception ex = Assert.Throws<ArgumentException>(() => manager.AddComputer(computer));
            Assert.That(ex.Message, Is.EqualTo("This computer already exists."));
            Exception ex2 = Assert.Throws<ArgumentException>(() => manager.AddComputer(sameComputer));
            Assert.That(ex2.Message, Is.EqualTo("This computer already exists."));

        }

        [Test]
        public void AddComputerAddsComputerToCollection()
        {
            ComputerManager manager = new ComputerManager();
            Computer computer = new Computer("Company", "2.0", 100);
            manager.AddComputer(computer);

            Assert.That(manager.Count, Is.EqualTo(1));
            Assert.That(manager.GetComputersByManufacturer("Company").Contains(computer));
            Assert.That(manager.GetComputer("Company", "2.0"), Is.SameAs(computer));
        }

       
        [TestCase("Wrong1","Right2")]
        [TestCase("Right1","Wrong2")]
        [TestCase("Wrong1","Wrong2")]
        public void GetComputerThrowsIfNotFound(string manufacturer, string model)
        {
            Computer computer = new Computer("Right1", "Right2", 200);
            ComputerManager manager = new ComputerManager();
            manager.AddComputer(computer);

            ArgumentException ex = Assert.Throws<ArgumentException>(() => manager.GetComputer(manufacturer, model));
        }

        [Test]
        public void GetComputerByManufacturerReturnsCollectionOfComputers()
        {
            Computer computer1 = new Computer("One", "MOne", 100);
            Computer computer2 = new Computer("One", "MTwo", 100);
            Computer computer3 = new Computer("Two", "MOne", 100);
            ComputerManager manager = new ComputerManager();
            manager.AddComputer(computer1);
            manager.AddComputer(computer2);
            manager.AddComputer(computer3);

            Assert.That(manager.GetComputersByManufacturer("One"), Is.Not.Null);
            Assert.That(manager.GetComputersByManufacturer("Two"), Is.Not.Null);
            Assert.That(manager.GetComputersByManufacturer("Three"), Is.Not.Null);

            Assert.That(manager.GetComputersByManufacturer("One").Count, Is.EqualTo(2));
            Assert.That(manager.GetComputersByManufacturer("Two").Count, Is.EqualTo(1));
            Assert.That(manager.GetComputersByManufacturer("Three").Count, Is.EqualTo(0));

            Assert.That(manager.GetComputersByManufacturer("One").Contains(computer1));
            Assert.That(manager.GetComputersByManufacturer("One").Contains(computer2));
        }

        [Test]
        public void RemoveComputerRemovesFromCollection()
        {
            Computer computer = new Computer("Right1", "Right2", 200);
            ComputerManager manager = new ComputerManager();
            manager.AddComputer(computer);


            Assert.That(manager.RemoveComputer("Right1", "Right2"), Is.SameAs(computer));
            Assert.That(manager.Count, Is.EqualTo(0));
        }

        [TestCase("Right1","Wrong2")]
        [TestCase("Wrong1","Right2")]
        [TestCase("Wrong1","Wrong2")]
        public void RemoveComputerReturnsFalseIfNoSuchComputer(string manufacturer, string model)
        {
            Computer computer = new Computer("Right1", "Right2", 200);
            ComputerManager manager = new ComputerManager();
            manager.AddComputer(computer);

            Exception ex = Assert.Throws<ArgumentException>(() => manager.RemoveComputer(manufacturer, model));
            Assert.That(ex.Message, Is.EqualTo("There is no computer with this manufacturer and model."));
        }

        [Test]
        public void GetComputerByManufactuerReturnsEmptyCollectionIfNoSuchManufactuer()
        {

        }

        [Test]
        public void GetComputerFindsComputer()
        {
            Computer computer = new Computer("Guy", "Model1", 200);
            ComputerManager manager = new ComputerManager();
            manager.AddComputer(computer);
            Assert.That(manager.GetComputer("Guy","Model1"),Is.Not.Null);
            Assert.That(manager.GetComputer("Guy","Model1"),Is.SameAs(computer));
        }

    }
}