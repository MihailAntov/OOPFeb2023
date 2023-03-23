using Collections;

namespace Collections.Tests
{
    public class CollectionTests
    {
        Collection<int> collection;
        
        [SetUp]
        public void SetUp()
        {
            collection = new Collection<int>();
        }
        
        [Test]

        public void ConstructorEmptyTest()
        {
            collection = new Collection<int>();
            Assert.That(collection.Count, Is.EqualTo(0));
            Assert.That(collection.Capacity, Is.EqualTo(16));
        }

        [Test]
        public void ConstructorWithOneArgTest()
        {
            Collection<string> collectionStrings = new Collection<string>("First");
            Assert.That(collectionStrings.Count, Is.EqualTo(1));
            Assert.That(collectionStrings.Capacity, Is.EqualTo(16));
            Assert.That(collectionStrings[0], Is.EqualTo("First"));
        }
        [Test]
        public void ConstructorWithMultipleArgsTest()
        {
            Collection<string> collectionStrings = new Collection<string>("First","Second","Third");
            Assert.That(collectionStrings.Count, Is.EqualTo(3));
            Assert.That(collectionStrings.Capacity, Is.EqualTo(16));
            Assert.That(collectionStrings[0], Is.EqualTo("First"));
            Assert.That(collectionStrings[1], Is.EqualTo("Second"));
            Assert.That(collectionStrings[2], Is.EqualTo("Third"));
        }

        


        [Test]
        public void AddTest()
        {
            collection.Add(1);
            Assert.That(collection.Count, Is.EqualTo(1));
            Assert.That(collection.Capacity, Is.EqualTo(16));
        }
        [Test]
        public void AddWithGrowTest()
        {
            for(int i = 0; i < 20; i++)
            {
                if(i == 16)
                {
                    Assert.That(collection.Count, Is.EqualTo(16));
                    Assert.That(collection.Capacity, Is.EqualTo(16));
                    collection.Add(i);
                    Assert.That(collection.Count, Is.EqualTo(17));
                    Assert.That(collection.Capacity, Is.EqualTo(32));
                }
                else
                {
                    collection.Add(i);
                }

                

            }
        }
        [Test]
        public void AddRangeTest()
        {
            collection.AddRange(new int[] { 1, 2, 3, 4 });
            Assert.That(collection.Count, Is.EqualTo(4));
        }


        [Test]
        public void AddRangeWithGrowTest()
        {
            int[] ints = new int[20];
            for (int i = 0; i < ints.Length; i++)
            {
                ints[i] = i;
            }
            Assert.That(collection.Count, Is.EqualTo(0));
            Assert.That(collection.Capacity, Is.EqualTo(16));
            collection.AddRange(ints);
            Assert.That(collection.Count, Is.EqualTo(20));
            Assert.That(collection.Capacity, Is.EqualTo(32));
        }

        [Test]
        public void AddOneMillionItems()
        {
            Collection<int> collection = new Collection<int>();
            for(int i = 0; i < 1_000_000; i ++)
            {
                collection.Add(i);
            }
            Assert.That(collection.Count == 1_000_000);
            Assert.That(collection.Capacity > 1_000_000);
        }
        [Test]
        public void CountTest()
        {
            Assert.That(collection.Count, Is.EqualTo(0));
            collection.AddRange(new int[] { 1, 2 });
            Assert.That(collection.Count, Is.EqualTo(2));
        }
        [Test]
        public void CapacityTest()
        {
            Assert.That(collection.Capacity, Is.EqualTo(16));
            collection.AddRange(new int[] { 1, 2 });
            Assert.That(collection.Capacity, Is.EqualTo(16));
            collection.AddRange(new int[] { 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17 });
            Assert.That(collection.Capacity, Is.EqualTo(32));

        }
        [Test]
        public void IndexGetTest()
        {
            collection.Add(1);
            collection.Add(2);
            collection.Add(3);
            collection.Add(4);
            Assert.That(collection[2], Is.EqualTo(3));
        }
        [Test]
        public void InvalidIndexGetThrows()
        {
            int attempt = 0;
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            attempt = collection[1]);

            

        }
        [Test]
        public void IndexSetTest()
        {
            collection.Add(5);
            Assert.That(collection[0], Is.EqualTo(5));
            collection[0] = 4;
            Assert.That(collection[0], Is.EqualTo(4));
        }
        [Test]
        public void InvalidIndexSetTest()
        {
            collection.Add(5);
            collection.Add(6);
            Assert.Throws<ArgumentOutOfRangeException>(() => collection[2] = 5);
        }

        [TestCase(0,0)]
        [TestCase(6,0)]
        [TestCase(3,0)]
        public void InsertAtTest(int index,int value)
        {
            collection.AddRange(new int[] { 1, 2, 3, 4 , 5 , 6 });
            collection.InsertAt(index, value);
            Assert.That(collection.Count, Is.EqualTo(7));
            Assert.That(collection[index], Is.EqualTo(value));

            for(int i = 0; i < index; i++)
            {
                Assert.That(collection[i], Is.EqualTo(i+1));
            }

            for (int i = index+1; i < 7; i++)
            {
                Assert.That(collection[i], Is.EqualTo(i));
            }
        }

        [Test]
        public void InsertWithGrow()
        {
            collection.AddRange(new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16 });
            collection.InsertAt(4, 5);
            Assert.That(collection.Count, Is.EqualTo(17));
            Assert.That(collection.Capacity, Is.EqualTo(32));
        }
        [Test]
        public void InsertThrowsIfIndexInvalid()
        {
            collection.AddRange(new int[] { 1, 2, 3 });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            collection.InsertAt(4, 5));
        }
        [Test]
        public void InsertToEmptyCollection()
        {
            collection.InsertAt(0, 5);
            Assert.That(collection.Count, Is.EqualTo(1));
            Assert.That(collection[0], Is.EqualTo(5));
        }
        [Test]
        public void ExchangeTest()
        {
            collection.AddRange(new int[] { 1, 2, 3 });
            collection.Exchange(0, 2);
            Assert.That(collection.Count, Is.EqualTo(3));
            Assert.That(collection[0], Is.EqualTo(3));
            Assert.That(collection[2], Is.EqualTo(1));
        }
        [TestCase(-1,2)]
        [TestCase(1,5)]
        [TestCase(-1,5)]
        [Test]
        public void ExchangeThrowsIfInvalidIndex(int index1, int index2)
        {
            collection.AddRange(new int[] { 1, 2, 3 });
            Assert.Throws<ArgumentOutOfRangeException>(() =>
            collection.Exchange(index1, index2));
        }
        [Test]
        public void RemoveAtTest()
        {
            collection.AddRange(new int[] { 1, 2, 3 , 4, 5});
            collection.RemoveAt(2);
            Assert.That(collection[2], Is.EqualTo(4));
            Assert.That(collection.Count, Is.EqualTo(4));
        }
        [Test]
        public void RemoveAtStart()
        {
            collection.AddRange(new int[] { 1, 2, 3, 4, 5 });
            collection.RemoveAt(0);
            Assert.That(collection[0], Is.EqualTo(2));
            Assert.That(collection.Count, Is.EqualTo(4));

        }
        [Test]
        public void RemoveAtEnd()
        {
            collection.AddRange(new int[] { 1, 2, 3, 4, 5 });
            collection.RemoveAt(collection.Count-1);
            Assert.That(collection[3], Is.EqualTo(4));
            Assert.That(collection.Count, Is.EqualTo(4));

        }

        [TestCase(-1)]
        [TestCase(5)]
        [TestCase(100)]
        [Test]  
        public void RemoveThrowsIfInvalidIndex(int index)
        {
            collection.Add(1);
            collection.Add(2);
            collection.Add(3);

            Assert.Throws<ArgumentOutOfRangeException>(() =>
            collection.RemoveAt(index));

            
        }

        [Test]
        public void ClearTest()
        {
            collection.Add(1);
            collection.Add(2);
            collection.Add(3);
            collection.Clear();
            Assert.That(collection.Count, Is.EqualTo(0));
            collection.Clear();
            Assert.That(collection.Count, Is.EqualTo(0));
        }
        [Test]
        public void ToStringTestMultipleItems()
        {
            collection.AddRange(new int[] { 1, 2, 3 });
            Assert.That(collection.ToString(), Is.EqualTo("[1, 2, 3]"));
        }
        [Test]
        public void ToStringTestSingleItem()
        {
            Collection<string> stringCollection = new Collection<string>("single");
            Assert.That(stringCollection.ToString(), Is.EqualTo("[single]"));
        }
        [Test]
        public void ToStringTestEmpty()
        {
            Assert.That(collection.ToString(), Is.EqualTo("[]"));
        }
        [Test]
        public void ToStringTestNestedCollections()
        {
            Collection<Collection<string>> nestedCollection = new Collection<Collection<string>>();

            nestedCollection.Add(new Collection<string>("1.1","1.2"));
            nestedCollection.Add(new Collection<string>("2.1","2.2","2.3"));

            Assert.That(nestedCollection.ToString(), Is.EqualTo("[[1.1, 1.2], [2.1, 2.2, 2.3]]"));
        }

    }
}
