using FootballTeam;
using NUnit.Framework;
using System;

namespace Football.NewTests
{
    public class Tests
    {
        FootballPlayer player;
        FootballTeam.FootballTeam team;

        [SetUp]
        public void Setup()
        {
            player = new FootballPlayer("test", 1, "Forward");
            team = new FootballTeam.FootballTeam("teamName", 15);
        }

        [Test]
        public void FootballPlayerConstructorSetsPropertiesCorrectly()
        {
            player = new FootballPlayer("test1", 5, "Midfielder");
            Assert.That(player.Position, Is.EqualTo("Midfielder"));
            Assert.That(player.Name, Is.EqualTo("test1"));
            Assert.That(player.PlayerNumber, Is.EqualTo(5));
        }

        [TestCase("",1,"Forward", "Name cannot be null or empty!")]
        [TestCase(null,1, "Forward", "Name cannot be null or empty!")]
        [TestCase("test",0, "Forward", "Player number must be in range [1,21]")]
        [TestCase("test", 22, "Forward", "Player number must be in range [1,21]")]
        [TestCase("test", 1,"", "Invalid Position")]
        [TestCase("test", 1,null, "Invalid Position")]
        public void FootballPlayerConstructorThrowsCorrectly(string name, int number, string position, string errormessage)
        {
            
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
            player = new FootballPlayer(name, number, position));

            Assert.That(ex.Message, Is.EqualTo(errormessage));
        }

        [Test]
        public void FootballTeamConstructorSetsProperties()
        {
            team = new FootballTeam.FootballTeam("testName", 20);
            Assert.That(team, Is.Not.Null);
            Assert.That(team.Players, Is.Not.Null);
            Assert.That(team.Name, Is.EqualTo("testName"));
            Assert.That(team.Capacity, Is.EqualTo(20));
        }

        [TestCase("",15, "Name cannot be null or empty!")]
        [TestCase(null,15, "Name cannot be null or empty!")]
        [TestCase("name",14, "Capacity min value = 15")]
        public void FootballTeamThrowsCorrectly(string name, int capacity, string errorMessage)
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(()=>
            team = new FootballTeam.FootballTeam(name, capacity));
            Assert.That(ex.Message, Is.EqualTo(errorMessage));
        }


        [Test]
        public void ScoredGoalsTrackedCorrectly()
        {
            Assert.That(player.ScoredGoals, Is.EqualTo(0));
            player.Score();
            Assert.That(player.ScoredGoals, Is.EqualTo(1));
        }

        [Test]
        public void AddNewPlayerAddsPlayerToCollectionAndReturnsCorrectMessage()
        {
            string result = team.AddNewPlayer(player);
            Assert.That(team.Players.Count, Is.EqualTo(1));
            Assert.That(result, Is.EqualTo($"Added player {player.Name} in position {player.Position} with number {player.PlayerNumber}"));
        }

        [Test]
        public void AddNewPlayerThrowsIfAtCapacity()
        {
            for (int i = 1; i <= 15; i++)
            {
                team.AddNewPlayer(new FootballPlayer($"name{i}", i, "Forward"));
            }
            Assert.That(team.Players.Count, Is.EqualTo(15));

            string result = team.AddNewPlayer(new FootballPlayer("name23", 20, "Midfielder"));
            Assert.That(team.Players.Count, Is.EqualTo(15));
            Assert.That(result, Is.EqualTo("No more positions available!"));
        }

        [Test]
        public void PickPlayerReturnsCorrectPlayer()
        {
            team.AddNewPlayer(player);
            FootballPlayer foundPlayer = team.PickPlayer("test");
            Assert.That(foundPlayer, Is.Not.Null);
            Assert.That(foundPlayer, Is.SameAs(player));
        }

        [Test]
        public void PickPlayerReturnsNullIfNoPlayerFound()
        {
            FootballPlayer foundPlayer = team.PickPlayer("test");
            Assert.That(foundPlayer, Is.Null);
            
        }


        [Test]
        public void PlayerScoreIncreasesScoreCountAndReturnsCorrectMessage()
        {
            team.AddNewPlayer(player);
            Assert.That(player.ScoredGoals, Is.EqualTo(0));
            string result = team.PlayerScore(1);
            Assert.That(player.ScoredGoals, Is.EqualTo(1));
            Assert.That(result, Is.EqualTo($"{player.Name} scored and now has {player.ScoredGoals} for this season!"));
        }
    }
}