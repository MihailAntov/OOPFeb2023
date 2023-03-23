using Collections;

namespace Collections.Tests
{
    public class CircularQueueTests
    {
        CircularQueue<int> queue;

        [SetUp]
        public void SetUp()
        {
            queue = new CircularQueue<int>();
        }
        
        
        [Test]
        public void ConstructorDefault()
        {
            queue = new CircularQueue<int>();
            Assert.That(queue.Capacity, Is.EqualTo(8));
            Assert.That(queue.Count, Is.EqualTo(0));
            Assert.That(queue.StartIndex, Is.EqualTo(0));
            Assert.That(queue.EndIndex, Is.EqualTo(0));
        }
        [Test]
        public void ConstructorWithCapacity()
        {
            queue = new CircularQueue<int>(5);
            Assert.That(queue.Capacity, Is.EqualTo(5));
            Assert.That(queue.Count, Is.EqualTo(0));
            Assert.That(queue.StartIndex, Is.EqualTo(0));
            Assert.That(queue.EndIndex, Is.EqualTo(0));
        }
        [Test]
        public void Enqueue()
        {
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            Assert.That(queue.Count, Is.EqualTo(3));
            Assert.That(queue.Capacity >= queue.Count);
            Assert.That(queue.Peek() == 1);
            Assert.That(queue.ToString() == "[1, 2, 3]");
        }
        [Test]
        public void EnqueueWithGrow()
        {
            queue = new CircularQueue<int>(4);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            Assert.That(queue.Capacity == 4);
            queue.Enqueue(5);
            Assert.That(queue.Capacity == 8);
            Assert.That(queue.ToString() == "[1, 2, 3, 4, 5]");
        }

        [Test]
        public void EndIndexIsZeroWhenFull()
        {
            queue = new CircularQueue<int>(4);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            Assert.That(queue.EndIndex, Is.EqualTo(0));
        }
        [Test]
        public void Deqeue()
        {
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            Assert.That(queue.Count, Is.EqualTo(3));
            queue.Dequeue();
            queue.Dequeue();
            Assert.That(queue.ToString(), Is.EqualTo("[3]"));
            Assert.That(queue.Count, Is.EqualTo(1));
        }
        [Test]
        public void DequeueWhenEmtpy()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            queue.Dequeue());
            Assert.That(ex.Message, Is.EqualTo("The queue is empty!"));
        }
        [Test]
        public void EnqueueDequeueWithRangeCross()
        {
            for (int i = 1; i <= 8; i++)
            {
                queue.Enqueue(i);
            }

            int number = queue.Dequeue();
            Assert.That(number, Is.EqualTo(1));
            number = queue.Dequeue();
            Assert.That(number, Is.EqualTo(2));
            Assert.That(queue.StartIndex, Is.EqualTo(2));
            Assert.That(queue.EndIndex, Is.EqualTo(0));
            queue.Enqueue(9);
            Assert.That(queue.Capacity, Is.EqualTo(8));
            Assert.That(queue.Count, Is.EqualTo(7));
            Assert.That(queue.EndIndex, Is.EqualTo(1));
            Assert.That(queue.StartIndex, Is.EqualTo(2));
            Assert.That(queue.ToString(), Is.EqualTo("[3, 4, 5, 6, 7, 8, 9]"));
            queue.Dequeue();
            queue.Dequeue();
            queue.Dequeue();
            queue.Dequeue();
            queue.Dequeue();
            queue.Dequeue();

            Assert.That(queue.StartIndex, Is.EqualTo(0));
            Assert.That(queue.EndIndex, Is.EqualTo(1));
            Assert.That(queue.Count, Is.EqualTo(1));
        }
        [Test]
        public void Peek()
        {
            queue.Enqueue(1);
            Assert.That(queue.Peek, Is.EqualTo(1));
            queue.Enqueue(2);
            queue.Enqueue(3);
            Assert.That(queue.Peek, Is.EqualTo(1));

        }
        [Test]
        public void PeekEmpty()
        {
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            queue.Peek());

            Assert.That(ex.Message, Is.EqualTo("The queue is empty!"));
        }
        [Test]
        public void ToArray()
        {
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            int[] intArray = new int[] { 1,2,3 };
            Assert.That(queue.ToArray(), Is.EquivalentTo(intArray));
        }
        [Test]
        public void ToArrayWithCrossRange()
        {
            queue = new CircularQueue<int>(4);
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            queue.Enqueue(4);
            queue.Dequeue();
            queue.Dequeue();
            queue.Enqueue(5);
            queue.Enqueue(6);
            int[] intArray = new int[] { 3, 4, 5, 6 };
            Assert.That(queue.ToArray(), Is.EquivalentTo(intArray));
        }
        [Test]
        public void TestToString()
        {
            queue.Enqueue(1);
            queue.Enqueue(2);
            queue.Enqueue(3);
            Assert.That(queue.ToString(), Is.EqualTo("[1, 2, 3]"));
        }
        [Test]
        public void MultipleOperations()
        {
            
            for (int i = 1; i <= 300; i++)
            {
                queue.Enqueue(10);
                queue.Enqueue(10);
                Assert.That(queue.Peek(), Is.EqualTo(10));
                queue.Dequeue();
                Assert.That(queue.Count, Is.EqualTo(i));

                int[] ints = new int[i];
                for (int j = 0; j < i; j++)
                {
                    ints[j] = 10;
                }
                Assert.That(queue.ToArray(), Is.EquivalentTo(ints));

            }

            

            
        }
        [Test]
        public void OneMillionItems()
        {
            int countToBeAdded = 1_000_000;
            int addedCount = 0;
            int removedCount = 0;
            int counter = 0;
            for(int i = 0; i < countToBeAdded / 2; i++)
            {
                queue.Enqueue(++counter);
                addedCount++;

                queue.Enqueue(++counter);
                addedCount++;

                queue.Dequeue();
                removedCount++;
            }

            Assert.That(queue.Count, Is.EqualTo(addedCount - removedCount));
        }
    }
}
