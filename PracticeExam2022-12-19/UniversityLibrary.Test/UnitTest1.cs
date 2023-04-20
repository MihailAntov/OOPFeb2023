namespace UniversityLibrary.Test
{
    using NUnit.Framework;
    using System;

    public class Tests
    {
        private UniversityLibrary library;
        private TextBook bookOne;
        private TextBook bookTwo;
        
        [SetUp]
        public void Setup()
        {
            library = new UniversityLibrary();
            bookOne = new TextBook("book1", "author1", "category1");
            bookTwo = new TextBook("book2", "author1", "category1");
        }

        [Test]
        public void TextBookConstructorWorksProperly()
        {
            TextBook testBook = new TextBook("testTitle", "testAuthor", "testCategory");
            Assert.That(testBook, Is.Not.Null);
            Assert.That(testBook.Title, Is.EqualTo("testTitle"));
            Assert.That(testBook.Author, Is.EqualTo("testAuthor"));
            Assert.That(testBook.Category, Is.EqualTo("testCategory"));
            Assert.That(testBook.InventoryNumber, Is.EqualTo(0));
            Assert.That(testBook.Holder, Is.Null);
        }
        [Test]
        public void TextBookToStringWorksProperly()
        {
            TextBook testBook = new TextBook("testTitle", "testAuthor", "testCategory");
            string expectedResult = $"Book: testTitle - 0{Environment.NewLine}Category: testCategory{Environment.NewLine}Author: testAuthor";
            

            Assert.That(testBook.ToString(), Is.EqualTo(expectedResult));
        }

        [Test]
        public void UniversityLibraryConstructorWorksProperly()
        {
            UniversityLibrary testLibrary = new UniversityLibrary();

            Assert.That(testLibrary, Is.Not.Null);
            Assert.That(testLibrary.Catalogue, Is.Not.Null);
            Assert.That(testLibrary.Catalogue.Count == 0);
        }

        [Test]
        public void AddingTextBookToLibraryAddsItToCatalogue()
        {
            library.AddTextBookToLibrary(bookOne);
            Assert.That(library.Catalogue.Count, Is.EqualTo(1));
            Assert.That(library.Catalogue[0], Is.EqualTo(bookOne));

            library.AddTextBookToLibrary(bookTwo);
            Assert.That(library.Catalogue.Count, Is.EqualTo(2));
            Assert.That(library.Catalogue[0], Is.EqualTo(bookOne));
            Assert.That(library.Catalogue[1], Is.EqualTo(bookTwo));
        }

        [Test]
        public void AddingTextBookToLibraryAssignsItANumber()
        {
            Assert.That(bookTwo.InventoryNumber, Is.EqualTo(0));
            Assert.That(bookOne.InventoryNumber, Is.EqualTo(0));
            library.AddTextBookToLibrary(bookOne);
            Assert.That(bookOne.InventoryNumber, Is.EqualTo(1));
            Assert.That(bookTwo.InventoryNumber, Is.EqualTo(0));
            library.AddTextBookToLibrary(bookTwo);
            Assert.That(bookOne.InventoryNumber, Is.EqualTo(1));
            Assert.That(bookTwo.InventoryNumber, Is.EqualTo(2));


        }

        [Test]
        public void AddingTextBookToLibraryReturnsItAsString()
        {
            Assert.That(library.AddTextBookToLibrary(bookOne), Is.EqualTo(bookOne.ToString()));
        }

        

        [Test]
        public void LoaningTextBookToCurrentHolderFailsAndReturnsAppropriateMessage()
        {
            const string exptectedResult = "greedyHolder still hasn't returned book1!";
            library.AddTextBookToLibrary(bookOne);
            library.LoanTextBook(1, "greedyHolder");
            Assert.That(library.LoanTextBook(1, "greedyHolder"), Is.EqualTo(exptectedResult));
        }

        [Test]
        public void LoaningTextBookTransfersHolderPropertyToNewHolder()
        {
            Assert.That(bookOne.Holder, Is.Null);
            library.AddTextBookToLibrary(bookOne);
            Assert.That(bookOne.Holder, Is.Null);
            library.LoanTextBook(1, "holder");
            Assert.That(bookOne.Holder, Is.Not.Null);
            Assert.That(bookOne.Holder, Is.EqualTo("holder"));


        }

        [Test]
        public void LonaingTextBookReturnsCorrectMessage()
        {
            library.AddTextBookToLibrary(bookOne);
            string expectedResult = $"book1 loaned to student1.";
            Assert.That(library.LoanTextBook(1, "student1"), Is.EqualTo(expectedResult));
        }
        

        [Test]
        public void ReturningTextBookClearsItsHolderProperty()
        {
            library.AddTextBookToLibrary(bookOne);
            Assert.That(bookOne.Holder, Is.Null);

            library.LoanTextBook(1, "someone");
            Assert.That(bookOne.Holder, Is.Not.Null);

            library.ReturnTextBook(1);
            Assert.That(bookOne.Holder, Is.EqualTo(string.Empty));


        }

        [Test]
        public void ReturingTextBookReturnsCorrectMessage()
        {
            library.AddTextBookToLibrary(bookOne);
            library.LoanTextBook(1, "someone");
            string result = library.ReturnTextBook(1);
            string expectedResult = "book1 is returned to the library.";
            Assert.That(result, Is.EqualTo(expectedResult));

        }
    }
}