using NUnit.Framework;
using System;

namespace Robots.Tests
{
    public class Tests
    {
        Robot robot;
        RobotManager manager;
        
        [SetUp]
        public void Setup()
        {
            robot = new Robot("one", 10);
            manager = new RobotManager(5);
        }

        [Test]
        public void RobotConstructorSetsProperties()
        {
            robot = new Robot("testName", 10);
            Assert.That(robot, Is.Not.Null);
            Assert.That(robot.Name, Is.EqualTo("testName"));
            Assert.That(robot.MaximumBattery, Is.EqualTo(10));
            Assert.That(robot.Battery, Is.EqualTo(10));
        }

        [Test]
        public void RobotManagerConstructorSetsProperties()
        {
            manager = new RobotManager(5);
            Assert.That(manager, Is.Not.Null);
            Assert.That(manager.Count, Is.EqualTo(0));
            Assert.That(manager.Capacity, Is.EqualTo(5));
        }

        [Test]
        public void RobotManagerConstructorThrowsIfNegativeCapacity()
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
            manager = new RobotManager(-1));
            Assert.That(ex.Message, Is.EqualTo("Invalid capacity!"));
        }

        

        [Test]
        public void AddAddsRobotToCollection()
        {
            Assert.That(manager.Count, Is.EqualTo(0));
            manager.Add(robot);
            Assert.That(manager.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddThrowsIfAtCapacity()
        {
            manager = new RobotManager(1);
            manager.Add(robot);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            manager.Add(new Robot("test2", 12)));
            Assert.That(ex.Message, Is.EqualTo("Not enough capacity!"));
        }

        [Test]
        public void AddThrowsIfRobotAlreadyExists()
        {
            manager.Add(robot);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(()=>
                manager.Add(new Robot("one", 12)));
            Assert.That(ex.Message, Is.EqualTo("There is already a robot with name one!"));
        }

        [Test]
        public void RemoveRemovesRobotFromCollection()
        {
            manager.Add(robot);
            Assert.That(manager.Count, Is.EqualTo(1));
            manager.Remove("one");
            Assert.That(manager.Count, Is.EqualTo(0));

        }

        [Test]
        public void RemoveThrowsIfRobotNotFound()
        {
            manager.Add(robot);
            Assert.That(manager.Count, Is.EqualTo(1));
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            manager.Remove("two"));
            Assert.That(ex.Message, Is.EqualTo("Robot with the name two doesn't exist!"));
        }

        [Test]
        public void WorkLowersBatteryCharge()
        {
            int startingBattery = robot.Battery;
            int energyCost = 2;
            manager.Add(robot);
            manager.Work("one", "dig", energyCost);
            Assert.That(robot.Battery, Is.EqualTo(startingBattery - energyCost));
        }

        [Test]
        public void WorkThrowsIfRobotNotFound()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            manager.Work("one","dig",5));
            Assert.That(ex.Message, Is.EqualTo("Robot with the name one doesn't exist!"));
        }

        [Test]
        public void WorkThrowsIfChargeTooLow()
        {
            manager.Add(robot);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            manager.Work("one", "dig", 15));
            Assert.That(ex.Message, Is.EqualTo("one doesn't have enough battery!"));
        }

        [Test]
        public void ChargeRefillsBattery()
        {
            int startingEnergy = robot.Battery;
            Assert.That(robot.Battery, Is.EqualTo(startingEnergy));
            manager.Add(robot);
            manager.Work("one", "dig", 5);
            Assert.That(robot.Battery, Is.EqualTo(5));
            manager.Charge("one");
            Assert.That(robot.Battery, Is.EqualTo(startingEnergy));
        }

        [Test]
        public void ChargeThrowsIfRobotNotFound()
        {
            manager.Add(robot);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            manager.Charge("two"));

            Assert.That(ex.Message, Is.EqualTo("Robot with the name two doesn't exist!"));
        }



    }
}