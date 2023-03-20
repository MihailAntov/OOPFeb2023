namespace DatabaseExtended.Tests
{
    using ExtendedDatabase;
    using NUnit.Framework;
    using System;

    [TestFixture]
    public class ExtendedDatabaseTests
    {
        [TestFixture]
        public class AddingUserTests : ExtendedDatabaseTests
        {
            [Test]
            public void AddingUserWithExistingNameThrowsException()
            {
                Person personOne = new Person(1, "Test");
                Person personTwo = new Person(2, "Test");
                Database database = new Database(personOne);
                Assert.Throws<InvalidOperationException>(()=>database.Add(personTwo));
            }

            [Test]
            public void AddingUserWithExistingIdThrowsException()
            {
                Person personOne = new Person(1, "Test");
                Person personTwo = new Person(1, "TestTest");
                Database database = new Database(personOne);
                Assert.Throws<InvalidOperationException>(() => database.Add(personTwo));
            }

            [Test]
            public void AddingUserAddsItToCollection()
            {
                Person personOne = new Person(1, "Test");
                Person personTwo = new Person(2, "TestTest");
                Database database = new Database(personOne);
                database.Add(personTwo);
                Assert.AreSame(personTwo, database.FindById(2));
            }
            [Test]
            public void InitialCountIsZero()
            {
                const int initialCount = 0;

                Database database = new Database();
                Assert.That(database.Count, Is.EqualTo(initialCount));
            }

            [Test]
            public void AddingUserIncreasesCount()
            {
                const int initialCount = 0;
                
                Person personOne = new Person(1, "Test");
                Database database = new Database();
                database.Add(personOne);
                Assert.That(database.Count, Is.EqualTo(initialCount + 1));
            }

            [Test]
            public void Adding17ThUserThrowsException()
            {
                Database database = new Database();
                for(int i = 0; i < 16; i++)
                {
                    database.Add(new Person(i, $"Name{i + 1}"));
                }

                Assert.Throws<InvalidOperationException>(() => database.Add(new Person(16, "Name15")));
            }
        }

        [TestFixture]
        public class RemovingUserTests : ExtendedDatabaseTests
        {
            private Database database;
            private const int InitialCount = 3;
            [SetUp]
            public void SeedDatabase()
            {
                Person personOne = new Person(1, "PersonOne");
                Person personTwo = new Person(2, "PersonTwo");
                Person personThree = new Person(3, "PersonThree");

                database = new Database(new Person[] { personOne, personTwo, personThree });
            }
            
            [Test]
            public void RemoveUserRemovesLastItemInCollection()
            {
                database.Remove();
                database.Remove();
                Assert.That(database.FindById(1), !Is.Null);


            }

            [Test]
            public void RemovingUserLowersCount()
            {
                database.Remove();
                Assert.That(database.Count, Is.EqualTo(InitialCount - 1));
            }

            [Test]
            public void RemoveUserThrowsExceptionIfDatabaseIsEmpty()
            {
                for(int i = 0; i < 3; i++)
                {
                    database.Remove();
                }

                Assert.Throws<InvalidOperationException>(() => database.Remove());
            }

            
        }
        [TestFixture]
        public class FindByUsernameTests: ExtendedDatabaseTests
        {
            Database database;
            Person person;
            const string UserName = "PersonOne";
            [SetUp]
            public void InitializeDb()
            {
                
                person = new Person(1, UserName);
                database = new Database(person);
            }
            
            [Test]
            public void FindUserByUsernameReturnsUser()
            {
                Assert.That(database.FindByUsername(UserName), Is.SameAs(person));
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
        }
        [TestFixture]
        public class FindByIdTests : ExtendedDatabaseTests
        {
            private Database database;
            private Person person;
            private const int personId = 1;
            
            
            [SetUp]
            public void InitializeDb()
            {
                person = new Person(personId, "UserOne");
                database = new Database(person);
            }
            
            [Test]
            public void FindUserByIdReturnsUser()
            {
                Assert.That(database.FindById(personId), Is.SameAs(person));

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
            
        }

        [TestFixture]
        public class ConstructorTests : ExtendedDatabaseTests
        {

            const string PersonName = "Five";
            const int PersonId = 5;
            Person person;

            [SetUp]
            public void InitializePerson()
            {
                person = new Person(PersonId, PersonName);
            }
            
            [Test]
            public void DatabaseConstructorAddsPersonToCollection()
            {
                
                
                Database database = new Database(person);

                Person foundPerson = database.FindById(PersonId);
                Assert.That(person, Is.SameAs(foundPerson));
            }
            [Test]
            public void PersonConstructorSetsId()
            {
                Assert.That(person.Id, Is.EqualTo(PersonId));
            }
            [Test]
            public void PersonConstructorSetsName()
            {
                Assert.That(person.UserName, Is.EqualTo(PersonName));
            }

            
        }
        

        

        
    }
}