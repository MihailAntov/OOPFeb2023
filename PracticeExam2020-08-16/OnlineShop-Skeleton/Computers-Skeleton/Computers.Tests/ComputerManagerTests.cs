using NUnit.Framework;

namespace Computers.Tests
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
    }
}