namespace Book.Tests
{
    using System;

    using NUnit.Framework;

    public class Tests
    {
        private Book book;
        private string footnoteOneText;
        private int footnoteOneNumber;
        
        [SetUp]
        public void StartUp()
        {
            book = new Book("name1", "author1");
            footnoteOneText = "footnote1text";
            footnoteOneNumber = 1;
        }
        
        [Test]
        public void BookConstructorCreatesObjectProperly()
        {
            Book book2 = new Book("name2", "author2");
            Assert.That(book2, Is.Not.Null);
            Assert.That(book2.Author, Is.EqualTo("author2"));
            Assert.That(book2.BookName, Is.EqualTo("name2"));
            
        }

        [Test]
        public void BookCountReturnsCorrectValue()
        {
            Assert.That(book.FootnoteCount, Is.EqualTo(0));
            book.AddFootnote(footnoteOneNumber, footnoteOneText);
            Assert.That(book.FootnoteCount, Is.EqualTo(1));
        }

        [Test]
        public void AuthorValidationThrowsIfInvalid()
        {
           ArgumentException ex = Assert.Throws<ArgumentException>(()=> book = new Book("test", null));
            Assert.That(ex.Message, Is.EqualTo("Invalid Author!"));

            ArgumentException ex2 = Assert.Throws<ArgumentException>(() => book = new Book("test", String.Empty));
            Assert.That(ex2.Message, Is.EqualTo("Invalid Author!"));


        }

        [Test]
        public void BookNameValidationThrowsIfInvalid()
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() => book = new Book(null, "test"));
            Assert.That(ex.Message, Is.EqualTo("Invalid BookName!"));

            ArgumentException ex2 = Assert.Throws<ArgumentException>(() => book = new Book(String.Empty, "test"));
            Assert.That(ex2.Message, Is.EqualTo("Invalid BookName!"));
        }

        [Test]
        public void BookNameValidationThrowsIfBothNameAndAuthorAreNull()
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() => book = new Book(null, null));
            Assert.That(ex.Message, Is.EqualTo("Invalid BookName!"));

            ArgumentException ex2 = Assert.Throws<ArgumentException>(() => book = new Book(String.Empty, String.Empty));
            Assert.That(ex2.Message, Is.EqualTo("Invalid BookName!"));
        }

        [Test]
        public void AddFootnoteAddsCorrectly()
        {
            book.AddFootnote(footnoteOneNumber, footnoteOneText);
            Assert.That(book.FootnoteCount, Is.EqualTo(1));
        }

        [Test]
        public void AddFootnoteThrowsIfExistingNumber()
        {
            book.AddFootnote(footnoteOneNumber, footnoteOneText);
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => book.AddFootnote(footnoteOneNumber, footnoteOneText));
            Assert.That(ex.Message, Is.EqualTo("Footnote already exists!"));
        }

        [Test]
        public void FindFootnoteReturnsCorrectValue()
        {
            book.AddFootnote(footnoteOneNumber, footnoteOneText);
            Assert.That(book.FindFootnote(footnoteOneNumber), Is.EqualTo($"Footnote #{footnoteOneNumber}: {footnoteOneText}"));

        }

        [Test]
        public void FindFootnoteThrowsIfFootnoteDoesntExist()
        {
            string result;
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => result =
            book.FindFootnote(footnoteOneNumber));
            Assert.That(ex.Message, Is.EqualTo("Footnote doesn't exists!"));

        }

        [Test]
        public void AlterFootnoteChangesFootnoteCorrectly()
        {
            book.AddFootnote(footnoteOneNumber, footnoteOneText);
            Assert.That(book.FindFootnote(footnoteOneNumber), Is.EqualTo($"Footnote #{footnoteOneNumber}: {footnoteOneText}"));
            string newText = "testAlter";
            book.AlterFootnote(footnoteOneNumber, newText);
            Assert.That(book.FindFootnote(footnoteOneNumber), Is.EqualTo($"Footnote #{footnoteOneNumber}: {newText}"));

        }

        [Test]
        public void AlterFootnoteThrowsIfFootnoteDoesntExist()
        {
            string result;
            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() => 
            book.AlterFootnote(footnoteOneNumber,"sampleText"));
            Assert.That(ex.Message, Is.EqualTo("Footnote does not exists!"));
        }
    }
}