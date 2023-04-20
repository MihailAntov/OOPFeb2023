using NUnit.Framework;
using System;
namespace BankSafe.Tests
{
    public class Tests
    {
        BankVault vault;
        Item item;
        
        [SetUp]
        public void Setup()
        {
            vault = new BankVault();
            item = new Item("urza", "papers");
        }

        [Test]
        public void ItemConstructorSetsProperties()
        {
            item = new Item("urza", "papers");
            Assert.That(item, Is.Not.Null);
            Assert.That(item.Owner, Is.EqualTo("urza"));
            Assert.That(item.ItemId, Is.EqualTo("papers"));
        }

        [Test]
        public void BankVaultConstructorWorksCorrectly()
        {
            vault = new BankVault();
            Assert.That(vault, Is.Not.Null);
        }

        [Test]
        public void AddItemAddsItemToCollectionAndReturnsMessage()
        {
            string result = vault.AddItem("A1", item);
            Assert.That(vault.VaultCells["A1"], Is.Not.Null);
            Assert.That(result, Is.EqualTo("Item:papers saved successfully!"));
        }

        [Test]
        public void AddItemThrowsIfInvalidKey()
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
            vault.AddItem("D1", item));
            Assert.That(ex.Message, Is.EqualTo("Cell doesn't exists!"));
        }

        [Test]
        public void AddItemThrowsIfCellNotEmpty()
        {
            vault.AddItem("B1", new Item("joro", "documents"));
            
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
            vault.AddItem("B1", item));
            Assert.That(ex.Message, Is.EqualTo("Cell is already taken!"));
        }

        [Test]
        public void AddItemThrowsIfItemAlreadyAdded()
        {
            vault.AddItem("B1", item);

            InvalidOperationException ex = Assert.Throws<InvalidOperationException>(() =>
            vault.AddItem("C1", new Item("ivan", "papers")));

            Assert.That(ex.Message, Is.EqualTo("Item is already in cell!"));
        }

        [Test]
        public void RemoveItemRemovesFromCollectionAndReturnsMessage()
        {
            vault.AddItem("A1", item);
            Assert.That(vault.VaultCells["A1"], Is.Not.Null);
            string result = vault.RemoveItem("A1", item);
            Assert.That(vault.VaultCells["A1"], Is.Null);
            Assert.That(result, Is.EqualTo("Remove item:papers successfully!"));
        }

        [Test]
        public void RemoveItemThrowsIfInvalidCell()
        {
            vault.AddItem("A1", item);
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
            vault.RemoveItem("D5", item));
            Assert.That(ex.Message, Is.EqualTo("Cell doesn't exists!"));
        }

        [Test]
        public void RemoveItemThrowsIfIncorrectItem()
        {
            vault.AddItem("A1", item);
            Item item2 = new Item("urza", "morePapers");
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
            vault.RemoveItem("A1", item2));
            Assert.That(ex.Message, Is.EqualTo("Item in that cell doesn't exists!"));
        }


    }
}