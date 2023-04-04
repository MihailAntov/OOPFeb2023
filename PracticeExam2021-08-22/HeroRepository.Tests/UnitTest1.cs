using NUnit.Framework;

using System;

namespace HeroRepository1.Tests
{
    public class Tests
    {

        Hero hero;
        HeroRepository repo;

        [SetUp]
        public void Setup()
        {
            hero = new Hero("talos", 5);
            repo = new HeroRepository();
        }

        [Test]
        public void HeroConstructorSetsProperties()
        {
            hero = new Hero("testName", 5);
            Assert.That(hero, Is.Not.Null);
            Assert.That(hero.Name, Is.EqualTo("testName"));
            Assert.That(hero.Level, Is.EqualTo(5));
        }

        [Test]
        public void HeroRepositoryConstructorSetsProperties()
        {
            repo = new HeroRepository();
            Assert.That(repo.Heroes, Is.Not.Null);
        }

        [Test]
        public void HeroRepositoryCountReturnsCorrectValue()
        {
            Assert.That(repo.Heroes.Count, Is.EqualTo(0));
            repo.Create(hero);
            Assert.That(repo.Heroes.Count, Is.EqualTo(1));

        }

        [Test]
        public void CreateHeroAddsToCollection()
        {
            string result = repo.Create(hero);
            Assert.That(repo.GetHeroWithHighestLevel(), Is.SameAs(hero));

            Assert.That(result, Is.EqualTo("Successfully added hero talos with level 5"));
        }

        [Test]
        public void CreateHeroThrowsIfHeroIsNull()
        {
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
            repo.Create(null));
            Assert.That(ex.Message, Is.EqualTo("Hero is null (Parameter 'hero')"));
            
        }

        [Test]
        public void CreateHeroThrowsIfHeroAlreadyAdded()
        {
            repo.Create(hero);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            repo.Create(new Hero("talos", 4)));
            Assert.That(ex.Message, Is.EqualTo("Hero with name talos already exists"));
        }

        [Test]
        public void RemoveRemovesHeroFromCollection()
        {
            repo.Create(hero);
            bool result = repo.Remove("talos");
            Assert.That(result, Is.True);
            Assert.That(repo.Heroes.Count, Is.EqualTo(0));
        }

        [Test]
        public void RemoveReturnsFalseIfHeroNotFound()
        {
            repo.Create(hero);
            bool result = repo.Remove("talos2");
            Assert.That(result, Is.False);
            Assert.That(repo.Heroes.Count, Is.EqualTo(1));
        }

        [TestCase("")]
        [TestCase(null)]
        public void RemoveThrowsIfHeroNotFound(string input)
        {
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
            repo.Remove(input));
            Assert.That(ex.Message, Is.EqualTo("Name cannot be null (Parameter 'name')"));
        }

        [Test]
        public void GetHeroWithHighestLevelReturnsCorrectHero()
        {
            repo.Create(hero);
            Hero hero2 = new Hero("Ivan", 25);
            Hero hero3 = new Hero("Jorge", 1);
                
            repo.Create(hero2);
            repo.Create(hero3);

            Hero result = repo.GetHeroWithHighestLevel();
            Assert.That(result, Is.SameAs(hero2));
        }

        [Test]
        public void GetHeroByNameReturnsCorrectHero()
        {
            repo.Create(hero);

            Hero hero2 = new Hero("Ivan", 25);
            repo.Create(hero2);
            Assert.That(repo.GetHero("Ivan"), Is.SameAs(hero2));
            Assert.That(repo.GetHero("talos"), Is.SameAs(hero));
        }

        [Test]
        public void GetHeroByNameReturnsNullIfNotFound()
        {
            Assert.That(repo.GetHero("test"), Is.Null);
        }
    }
}