using NUnit.Framework;
using System;

namespace Gyms.Tests
{
    public class Tests
    {
        Athlete athlete;
        Gym gym;

        
        [SetUp]
        public void Setup()
        {
            gym = new Gym("globo", 5);
            athlete = new Athlete("blazer");
        }

        [Test]
        public void AthleteConstructorSetsProperties()
        {
            athlete = new Athlete("test");
            Assert.That(athlete, Is.Not.Null);
            Assert.That(athlete.FullName, Is.EqualTo("test"));
            Assert.That(athlete.IsInjured, Is.False);
        }

        [Test]
        public void GymConstructorSetsProperties()
        {
            gym = new Gym("globo", 5);
            Assert.That(gym, Is.Not.Null);
            Assert.That(gym.Capacity, Is.EqualTo(5));
            Assert.That(gym.Name, Is.EqualTo("globo"));
        }

        [TestCase(null)]
        [TestCase("")]
        public void GymConstructorThrowsIfNameIsNull(string name)
        {
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
            gym = new Gym(name, 5));
            Assert.That(ex.Message, Is.EqualTo("Invalid gym name. (Parameter 'value')"));
        }

        [Test]
        public void GymConstructorThrowsIfCapacityLessThanZero()
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
            gym = new Gym("globo", -1));
            Assert.That(ex.Message, Is.EqualTo("Invalid gym capacity."));
        }

        [Test]
        public void GymCountReturnsZeroOnEmptyCollection()
        {
            Assert.That(gym.Count, Is.EqualTo(0));
        }

        [Test]
        public void AddAthleteIncreasesCount()
        {
            Assert.That(gym.Count, Is.EqualTo(0));
            gym.AddAthlete(athlete);
            Assert.That(gym.Count, Is.EqualTo(1));

        }

        [Test]
        public void AddAthleteThrowsIfAtCapacity()
        {
            for (int i = 0; i < 5; i++)
            {
                gym.AddAthlete(new Athlete($"blazer{i}"));
            }

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            gym.AddAthlete(athlete));
            Assert.That(ex.Message, Is.EqualTo("The gym is full."));

        }

        [Test]
        public void RemoveAthleteRemovesAthleteFromCollection()
        {
            gym.AddAthlete(athlete);
            Assert.That(gym.Count, Is.EqualTo(1));
            gym.RemoveAthlete("blazer");
            Assert.That(gym.Count, Is.EqualTo(0));
        }

        [Test]
        public void RemoveAthleteThrowsIfNotFound()
        {
            gym.AddAthlete(athlete);
            Assert.That(gym.Count, Is.EqualTo(1));
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            gym.RemoveAthlete("test"));
            Assert.That(ex.Message, Is.EqualTo("The athlete test doesn't exist."));
            Assert.That(gym.Count, Is.EqualTo(1));
        }

        [Test]
        public void InjureAthleteSetsInjuredProperty()
        {
            gym.AddAthlete(athlete);
            Assert.That(athlete.IsInjured, Is.False);
            Athlete result = gym.InjureAthlete("blazer");
            Assert.That(athlete.IsInjured, Is.True);
            Assert.That(result, Is.EqualTo(athlete));
        }

        [Test]
        public void InjureAthleteThrowsIfNotFound()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            gym.InjureAthlete("test"));
            Assert.That(ex.Message, Is.EqualTo("The athlete test doesn't exist."));
        }

        [Test]
        public void ReportReturnsCorrectValue()
        {
            string expectedResult = "Active athletes at globo: blazer, blazer2, blazer4";
            Athlete athlete2 = new Athlete("blazer2");
            Athlete athlete3 = new Athlete("blazer3");
            Athlete athlete4 = new Athlete("blazer4");
            gym.AddAthlete(athlete);
            gym.AddAthlete(athlete2);
            gym.AddAthlete(athlete3);
            gym.AddAthlete(athlete4);
            gym.InjureAthlete("blazer3");
            Assert.That(gym.Report(), Is.EqualTo(expectedResult));
        }




    }
}