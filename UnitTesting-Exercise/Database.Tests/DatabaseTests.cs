namespace Database.Tests
{
    using NUnit.Framework;
    using System;
    using System.Linq;
    using System.Reflection;

    [TestFixture]
    public class DatabaseTests
    {
        Database database;

        [SetUp]
        public void InitializeDatabase()
        {
            database = new Database();
        }

        [Test]
        public void Adding17thElementThrowsException()
        {

            FillDatabase();
            Assert.Throws<InvalidOperationException>(() => database.Add(0));
        }

        [Test]
        public void DatabaseCapacityIsSixteen()
        {
            const int dbCapacity = 16;
            FillDatabase();
            Assert.That(database.Count, Is.EqualTo(dbCapacity));
        }

        [Test]
        public void AddsToNextEmptyIndex()
        {
            database.Add(0);
            database.Add(0);
            database.Add(50);

            Assert.That(database.Fetch()[2] == 50);
        }

        [Test]
        public void RemoveRemovesLastIndex()
        {
            const int TargetElement = 3;
            const int OtherElements = 1;
            
            database.Add(OtherElements);
            database.Add(TargetElement);
            database.Add(OtherElements);
            database.Remove();

            int lastElement = database.Fetch()[database.Count-1];
            Assert.That(lastElement, Is.EqualTo(TargetElement));
        }

        [Test]
        public void RemoveThrowsExceptionIfDatabaseIsEmpty()
        {
            Database database = new Database();
            Assert.Throws<InvalidOperationException>(() => database.Remove());
        }

        [Test]
        public void ConstructorOnlyTakesIntegers()
        {
            Type dbType = typeof(Database);
            ConstructorInfo[] constructors = dbType.GetConstructors();

            bool onlyTakesInts = true;

            foreach(ConstructorInfo constructor in constructors)
            {
                ParameterInfo[] paramTypes = constructor.GetParameters();

                foreach(ParameterInfo parameterInfo in paramTypes)
                {
                    if (parameterInfo.ParameterType != typeof(int[]))
                    {
                        onlyTakesInts = false;
                    }
                }
                
            }

            Assert.That(onlyTakesInts, Is.True);
        }

        [Test]
        public void ConstructorStoresIntegers()
        {
            int[] InitialDatabaseSeed =  { 3 };
            Database database = new Database(InitialDatabaseSeed);
            bool seedEqualToDataContents = true;
            int[] databaseContents = (int[])database.GetType()
                .GetFields(BindingFlags.NonPublic | BindingFlags.Instance)
                .FirstOrDefault(p => p.Name == "data")
                .GetValue(database);
            for(int i = 0; i < InitialDatabaseSeed.Length; i++)
            {
                if (InitialDatabaseSeed[i] != databaseContents[i])
                {
                    seedEqualToDataContents = false;
                }
            }

            Assert.That(seedEqualToDataContents, Is.True);
        }

        [Test]
        public void FetchReturnsArray()
        {
            int[] InitialDatabaseSeed = { 3,5,7 };
            Database database = new Database(InitialDatabaseSeed);

            bool seedEqualToDataContents = true;
            for (int i = 0; i < InitialDatabaseSeed.Length; i++)
            {
                if (InitialDatabaseSeed[i] != database.Fetch()[i])
                {
                    seedEqualToDataContents = false;
                }
            }

            Assert.That(seedEqualToDataContents, Is.True);
        }

        


        public void FillDatabase()
        {
            for (int i = 0; i < 16; i++)
            {
                database.Add(i);
            }
        }
    }
}
