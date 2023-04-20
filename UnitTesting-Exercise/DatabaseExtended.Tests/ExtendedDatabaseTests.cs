using ExtendedDatabase;
namespace DatabaseExtended.Tests
{
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        Database database;
        Person person;
        const int InitialCount = 0;

        [SetUp]
        public void SeedDatabase()
        {
            person = new Person(1, "PersonOne");
            database = new Database(new Person[] { person });
        }

        [TearDown]
        public void TearDown()
        {
            person = null;
            database = null;
        }

        [Test]
        public void AddingUserWithExistingNameThrowsException()
        {

            Person personTwo = new Person(2, "PersonOne");

            Assert.Throws<InvalidOperationException>(() => database.Add(personTwo));
        }



        [Test]
        public void AddingUserWithExistingIdThrowsException()
        {
            Person personTwo = new Person(1, "PersonTwo");

            Assert.Throws<InvalidOperationException>(() => database.Add(personTwo));
        }

        [Test]
        public void AddingUserAddsItToCollection()
        {

            Person personTwo = new Person(2, "TestTest");
            database.Add(personTwo);
            Assert.AreSame(personTwo, database.FindById(2));
        }
        [Test]
        public void InitialCountIsZero()
        {

            Database database = new Database();
            Assert.That(database.Count, Is.EqualTo(InitialCount));
        }

        [Test]
        public void AddingUserIncreasesCount()
        {
            Person personOne = new Person(2, "Test");
            database.Add(personOne);
            Assert.That(database.Count, Is.EqualTo(InitialCount + 2));
        }

        [Test]
        public void Adding17ThUserThrowsException()
        {
            Database database = new Database();
            for (int i = 1; i <= 16; i++)
            {
                database.Add(new Person(i, $"Name{i}"));
            }


            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => database.Add(new Person(16, "Name16")));
            //Assert.That(ex.Message, Is.EqualTo("Array's capacity must be exactly 16 integers!"));
        }




        [Test]
        public void RemoveUserRemovesLastItemInCollection()
        {
            database.Add(new Person(2, "PersonTwo"));
            database.Remove();

            Assert.That(database.FindById(1), !Is.Null);


        }

        [Test]
        public void RemovingUserLowersCount()
        {
            database.Remove();
            Assert.That(database.Count, Is.EqualTo(InitialCount));
        }

        [Test]
        public void RemoveUserThrowsExceptionIfDatabaseIsEmpty()
        {
            database.Remove();

            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }

        [Test]
        public void FindUserByUsernameReturnsUser()
        {
            Assert.That(database.FindByUsername("PersonOne"), Is.SameAs(person));
        }

        [Test]
        public void FindUserByUsernameThrowsExceptionIfNoUserFound()
        {
            Assert.Throws<InvalidOperationException>(() => database.FindByUsername("UserTwo"));
        }
        [Test]
        public void FindUserByUserameThrowsExceptionIfInputIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => database.FindByUsername(null));

        }
        [Test]
        public void FindUserByUsernameArgumentsAreCaseSensitive()
        {
            Assert.Throws<InvalidOperationException>(() => database.FindByUsername("userone"));

        }

        [Test]
        public void FindUserByIdReturnsUser()
        {
            Assert.That(database.FindById(1), Is.SameAs(person));

        }

        [Test]
        public void FindUserByIdThrowsExceptionIfNoUserFound()
        {
            Assert.Throws<InvalidOperationException>(() => database.FindById(2));
        }
        [Test]
        public void FindUserByIdThrowsExceptionIfInputIsNegativeOne()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => database.FindById(-1));

        }
        [Test]
        public void FindUserByIdThrowsExceptionIfInputIsNegative()
        {
            Assert.Throws<ArgumentOutOfRangeException>(() => database.FindById(-123456));

        }



        [Test]
        public void DatabaseConstructorAddsPersonToCollection()
        {

            Person[] people = new Person[2];
            people[0] = new Person(1, "One");
            people[1] = new Person(2, "Two");
            database = new Database(people);

            Person personOne = database.FindById(1);
            Person personTwo = database.FindById(2);
            Assert.That(personOne, Is.SameAs(people[0]));
            Assert.That(personTwo, Is.SameAs(people[1]));
        }
        [Test]
        public void DatabaseConstructorThrowsIfPassed17Arguments()
        {
            Person[] people = new Person[17];
            for (int i = 0; i < 17; i++)
            {
                people[i] = new Person(i, i.ToString());
            }


            Assert.Throws<ArgumentException>(() => database = new Database(people));

        }



        [Test]
        public void PersonConstructorSetsId()
        {
            person = new Person(1, "PersonOne");
            Assert.That(person.Id, Is.EqualTo(1));

        }
        [Test]
        public void PersonConstructorSetsName()
        {
            person = new Person(1, "PersonOne");
            Assert.That(person.UserName, Is.EqualTo("PersonOne"));
        }
    }
}