using NUnit.Framework;
using System;
using System.Linq;

namespace PlanetWars.TestsNew
{
    public class Tests
    {
        Weapon weapon;
        Planet planet;
        
        [SetUp]
        public void Setup()
        {
            weapon = new Weapon("Laser", 100, 5);
            planet = new Planet("Earth", 1000);
        }

        [Test]
        public void WeaponConstructorSetsProperties()
        {
            weapon = new Weapon("Laser", 200, 5);
            Assert.That(weapon, Is.Not.Null);
            Assert.That(weapon.Name, Is.EqualTo("Laser"));
            Assert.That(weapon.Price, Is.EqualTo(200));
            Assert.That(weapon.DestructionLevel, Is.EqualTo(5));
        }
        [Test]
        public void WeaponConstructorThrowsIfPriceNegative()
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
            weapon = new Weapon("Laser", -5, 5));
            Assert.That(ex.Message, Is.EqualTo("Price cannot be negative."));
        }
        [Test]
        public void WeaponIncreaseDestructionLevelWorksProperly()
        {
            weapon = new Weapon("Nuke", 100, 10);
            weapon.IncreaseDestructionLevel();
            Assert.That(weapon.DestructionLevel, Is.EqualTo(11));
        }
        [TestCase(5,false)]
        [TestCase(10,true)]
        [TestCase(15,true)]
        public void WeaponIsNuclearReturnsCorrectValue(int level, bool value)
        {
            weapon = new Weapon("test", 100, level);
            Assert.That(weapon.IsNuclear, Is.EqualTo(value));
        }

        [Test]
        public void PlanetConstructorSetsProperties()
        {
            planet = new Planet("Altera", 1000);
            Assert.That(planet, Is.Not.Null);
            Assert.That(planet.Name, Is.EqualTo("Altera"));
            Assert.That(planet.Budget, Is.EqualTo(1000));
            Assert.That(planet.Weapons, Is.Not.Null);
            Assert.That(planet.Weapons.Count, Is.EqualTo(0));
        }

        [TestCase("",100, "Invalid planet Name")]
        [TestCase(null,100, "Invalid planet Name")]
        [TestCase("name",-1, "Budget cannot drop below Zero!")]
        [TestCase("name",-5000.25, "Budget cannot drop below Zero!")]
        public void PlanetConstructorThrowsCorrectly(string name, double budget, string errorMessage)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
            planet = new Planet(name, budget));
            Assert.That(ex.Message, Is.EqualTo(errorMessage));
        }

        [Test]
        public void PlanetMilitaryPowerRatioReturnsCorrectValue()
        {
            Assert.That(planet.MilitaryPowerRatio, Is.EqualTo(0));
            planet.AddWeapon(weapon);
            Assert.That(planet.MilitaryPowerRatio, Is.EqualTo(5));
            planet.AddWeapon(new Weapon("nuke", 250, 40));
            Assert.That(planet.MilitaryPowerRatio, Is.EqualTo(45));

        }

        [TestCase(5)]
        [TestCase(5.5)]
        [TestCase(0)]
        [TestCase(-5)]
        public void ProfitIncreasesBudget(double amount)
        {
            double startingBudget = planet.Budget;
            planet.Profit(amount);
            Assert.That(planet.Budget, Is.EqualTo(startingBudget + amount));
        }

        [TestCase(5)]
        [TestCase(5.5)]
        [TestCase(0)]
        [TestCase(-5)]
        public void SpendingDecreasesBudget(double amount)
        {
            double startingBudget = planet.Budget;
            
            
            planet.SpendFunds(amount);
            Assert.That(planet.Budget, Is.EqualTo(startingBudget - amount));
        }

        [Test]
        public void SpendingThrowsIfInsufficientFunds()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            planet.SpendFunds(1500));
            Assert.That(ex.Message, Is.EqualTo("Not enough funds to finalize the deal."));
        }

        [Test]
        public void AddWeaponAddsWeaponToCollection()
        {
            planet.AddWeapon(weapon);
            Assert.That(planet.Weapons.FirstOrDefault(), Is.SameAs(weapon));
        }

        [Test]
        public void AddWeaponThrowsIfAlreadyAdded()
        {
            planet.AddWeapon(weapon);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            planet.AddWeapon(new Weapon("Laser", 200, 6)));
            Assert.That(ex.Message, Is.EqualTo($"There is already a Laser weapon."));
        }

        [Test]
        public void RemoveWeaponRemovesFromCollection()
        {
            planet.AddWeapon(weapon);
            Assert.That(planet.Weapons.Count, Is.EqualTo(1));
            planet.RemoveWeapon("Laser");
            Assert.That(planet.Weapons.Count, Is.EqualTo(0));
        }

        [Test]
        public void RemoveWeaponThrowsIfNoWeaponMatchesName()
        {
            Assert.That(planet.Weapons.Count, Is.EqualTo(0));
            planet.AddWeapon(weapon);
            Assert.That(planet.Weapons.Count, Is.EqualTo(1));
            planet.RemoveWeapon("WMD");
            Assert.That(planet.Weapons.Count, Is.EqualTo(1));
        }

        [Test]
        public void UpgradeWeaponIncreasesDestructionLevel()
        {
            planet.AddWeapon(weapon);
            Assert.That(weapon.DestructionLevel, Is.EqualTo(5));
            planet.UpgradeWeapon("Laser");
            Assert.That(weapon.DestructionLevel, Is.EqualTo(6));
        }

        [Test]
        public void UpgradeWeaponThrowsIfNoSuchWeaponFound()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            planet.UpgradeWeapon("Laser"));
            Assert.That(ex.Message, Is.EqualTo("Laser does not exist in the weapon repository of Earth"));
        }

        [Test]
        public void DestructPlanetReturnsCorrectMessage()
        {
            Planet attacker = new Planet("Republic", 1000);
            attacker.AddWeapon(new Weapon("BiggerLaser", 150, 9));
           
            Planet defender = new Planet("Empire", 1000);
            defender.AddWeapon(new Weapon("Laser", 100, 5));

            string result = attacker.DestructOpponent(defender);
            string expectedResult = "Empire is destructed!";
            Assert.That(result, Is.EqualTo(expectedResult));

        }

        [Test]
        public void DestructPlanetThrowsIfAttackingStrongerPlanet()
        {
            Planet attacker = new Planet("Empire", 1000);
            attacker.AddWeapon(new Weapon("Laser", 100, 5));

            Planet defender = new Planet("Republic", 1000);
            defender.AddWeapon(new Weapon("BiggerLaser", 150, 9));

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            attacker.DestructOpponent(defender));

            Assert.That(ex.Message, Is.EqualTo("Republic is too strong to declare war to!"));

            
        }


    }
}