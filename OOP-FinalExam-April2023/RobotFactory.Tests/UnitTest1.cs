using NUnit.Framework;
using System.Linq;

namespace RobotFactory.Tests
{
    public class Tests
    {
        Robot robot;
        Supplement supplement;
        Factory factory;
        
        
        [SetUp]
        public void Setup()
        {
            robot = new Robot("nemo", 20, 256); 
            supplement  = new Supplement("arm", 256);
            factory = new Factory("scifi", 5);
        }

        [Test]
        public void SupplementConstructorSetsProperties()
        {
            supplement = new Supplement("testName", 100);
            Assert.That(supplement, Is.Not.Null);
            Assert.That(supplement.Name, Is.EqualTo("testName"));
            Assert.That(supplement.InterfaceStandard, Is.EqualTo(100));
        }

        [Test]
        public void SupplementToStringReturnsCorrectValue()
        {
            Assert.That(supplement.ToString(), Is.EqualTo("Supplement: arm IS: 256"));
        }

        [Test]
        public void RobotConstructorSetsProperties()
        {
            robot = new Robot("nemo", 100, 256);
            Assert.That(robot, Is.Not.Null);
            Assert.That(robot.Model, Is.EqualTo("nemo"));
            Assert.That(robot.InterfaceStandard, Is.EqualTo(256));
            Assert.That(robot.Price, Is.EqualTo(100));
            Assert.That(robot.Supplements, Is.Not.Null);
            Assert.That(robot.Supplements.Count, Is.EqualTo(0));
        }

        [Test]
        public void RobotToStringREturnsCorrectValue()
        {
            Assert.That(robot.ToString(), Is.EqualTo("Robot model: nemo IS: 256, Price: 20.00"));
        }

        [Test]
        public void FactoryConstructorSetsProperties()
        {
            factory = new Factory("factoryName", 10);
            Assert.That(factory, Is.Not.Null);
            Assert.That(factory.Name, Is.EqualTo("factoryName"));
            Assert.That(factory.Capacity, Is.EqualTo(10));
            Assert.That(factory.Robots, Is.Not.Null);
            Assert.That(factory.Robots.Count, Is.EqualTo(0));
            Assert.That(factory.Supplements, Is.Not.Null);
            Assert.That(factory.Supplements.Count, Is.EqualTo(0));
        }

        [Test]
        public void ProduceRobotAddsRobotToCollectionAndReturnsCorrectMessage()
        {
            string result = factory.ProduceRobot("nemo",100,256);
            Assert.That(factory.Robots.Count, Is.EqualTo(1));
            string expectedOutput = "Produced --> Robot model: nemo IS: 256, Price: 100.00";
            Assert.That(result, Is.EqualTo(expectedOutput));
        }

        [Test]
        public void ProduceRobotReturnsCorrectMessageIfAtCapacity()
        {
            for(int i = 0; i < factory.Capacity; i++)
            {
                factory.ProduceRobot($"model{i + 1}", 100, 100);
            }

            string result = factory.ProduceRobot("nemo", 120, 150);
            string expectedResult = "The factory is unable to produce more robots for this production day!";
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void ProduceSupplementAddsItToCollectionAndReturnsCorrectMessage()
        {
            string result = factory.ProduceSupplement("arm", 100);
            Assert.That(factory.Supplements.Count, Is.EqualTo(1));
            Assert.That(factory.Supplements.Find(s => s.Name == "arm") != null);
            string expectedResult = "Supplement: arm IS: 100";
            Assert.That(result, Is.EqualTo(expectedResult));
        }

        [Test]
        public void UpgradeRobotAddsSupplementToRobotAndReturnsTrue()
        {
            factory.ProduceSupplement(supplement.Name, supplement.InterfaceStandard);
            factory.ProduceRobot(robot.Model, robot.Price, robot.InterfaceStandard);
            Supplement supplementAdded = factory.Supplements.FirstOrDefault(s => s.Name == supplement.Name);
            Robot robotAdded = factory.Robots.FirstOrDefault(r => r.Model == robot.Model);
            bool result = factory.UpgradeRobot(robotAdded, supplementAdded);
            Assert.That(robotAdded.Supplements.Count, Is.EqualTo(1));
            Assert.That(robotAdded.Supplements.Any(s => s.Name == "arm"));
            Assert.That(result, Is.True);
        }

        [Test]
        public void UpgradeFailsIfInterfaceAlreadyAdded()
        {
            factory.UpgradeRobot(robot, supplement);
            bool result = factory.UpgradeRobot(robot, supplement);
            Assert.That(robot.Supplements.Count, Is.EqualTo(1));
            Assert.That(result, Is.False);
        }

        [Test]
        public void UpgradeFailsIfRobotAndSupplementDontMatch()
        {
            Supplement notMatchingSupplement = new Supplement("leg", 337);
            bool result = factory.UpgradeRobot(robot, notMatchingSupplement);
            Assert.That(robot.Supplements.Count, Is.EqualTo(0));
            Assert.That(result, Is.False);
        }

        [TestCase(145)]
        [TestCase(150)]
        public void SellRobotReturnsFirstRobotToMeetBudget(int price)
        {
            factory.ProduceRobot("cheap", 20, 100);
            factory.ProduceRobot("right", price, 100);
            factory.ProduceRobot("expensive", 160, 100);
            Robot rightRobot = factory.Robots.FirstOrDefault(r => r.Model == "right");

            Robot result = factory.SellRobot(150);

            Assert.That(result, Is.SameAs(rightRobot));
        }
    }
}