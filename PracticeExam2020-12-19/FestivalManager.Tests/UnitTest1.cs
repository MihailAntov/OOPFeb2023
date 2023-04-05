using FestivalManager.Entities;
using NUnit.Framework;
using System;

namespace FestivalManager.Tests
{
    public class Tests
    {
        Song song;
        Stage stage;
        Performer performer;

        [SetUp]
        public void Setup()
        {
            song = new Song("sweet", new TimeSpan(0, 3, 25));
            performer = new Performer("test", "testov", 20);
            stage = new Stage();
        }

        //[Test]
        //public void SongConstructorSetsProperties()
        //{
        //    song = new Song("caroline",new TimeSpan(0, 2, 15));
        //    Assert.That(song, Is.Not.Null);
        //    Assert.That(song.Name, Is.EqualTo("caroline"));
        //    Assert.That(song.Duration, Is.EqualTo(new TimeSpan(0, 2, 15)));
        //    Assert.That(song.Duration.TotalSeconds, Is.EqualTo(135));
        //}

        //[Test]
        //public void SongToStringReturnsCorrectValue()
        //{
        //    Assert.That(song.ToString(), Is.EqualTo("sweet (03:25)"));
        //}

        //[Test]
        //public void PerformerConstructorSetsProperties()
        //{
        //    performer = new Performer("john", "smith", 25);
        //    Assert.That(performer, Is.Not.Null);
        //    Assert.That(performer.FullName, Is.EqualTo("john smith"));
        //    Assert.That(performer.SongList, Is.Not.Null);
        //    Assert.That(performer.Age, Is.EqualTo(25));

        //}

        //[Test]
        //public void PerformerToStringReturnsCorrectValue()
        //{
        //    Assert.That(performer.ToString(), Is.EqualTo("test testov"));
        //}

        [Test]
        public void StageConstructorSetsProperties()
        {
            stage = new Stage();
            Assert.That(stage, Is.Not.Null);
            Assert.That(stage.Performers, Is.Not.Null);
        }

        [Test]
        public void AddPerformerAddsPerformer()
        {
            Assert.That(stage.Performers.Count, Is.EqualTo(0));
            stage.AddPerformer(performer);
            Assert.That(stage.Performers.Count, Is.EqualTo(1));
        }

        [Test]
        public void AddPerformerThrowsIfAgeBelow18()
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
            stage.AddPerformer(new Performer("willy", "wonka", 15)));
            Assert.That(ex.Message, Is.EqualTo("You can only add performers that are at least 18."));
        }

        [Test]
        public void AddPerformerThrowsIfNull()
        {
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
            stage.AddPerformer(null));
            Assert.That(ex.Message, Is.EqualTo("Can not be null! (Parameter 'performer')"));
        }

        [Test]
        public void AddSongAddPerformerPlayWorkProperly()
        {
            Assert.That(stage.Play(), Is.EqualTo("0 performers played 0 songs"));
            stage.AddSong(song);
            stage.AddPerformer(performer);
            stage.AddSongToPerformer("sweet", "test testov");
            Assert.That(stage.Play(), Is.EqualTo("1 performers played 1 songs"));

        }

        [Test]
        public void AddSongThrowsIfNull()
        {
            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
            stage.AddSong(null));
            Assert.That(ex.Message, Is.EqualTo("Can not be null! (Parameter 'song')"));
        }

        [Test]
        public void AddSongThrowsIfSongTooShort()
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() =>
            stage.AddSong(new Song("sweet", new TimeSpan(0, 0, 59))));
            Assert.That(ex.Message, Is.EqualTo("You can only add songs that are longer than 1 minute."));
        }

        

        [Test]
        public void AddSongToPerformerThrowsIfSongNameNull()
        {
            

            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
            stage.AddSongToPerformer(null,"barry"));
            Assert.That(ex.Message, Is.EqualTo("Can not be null! (Parameter 'songName')"));

        }

        [Test]
        public void ADdSongToPerformerThrowsIfPerformerNameNull()
        {
            

            ArgumentNullException ex = Assert.Throws<ArgumentNullException>(() =>
            stage.AddSongToPerformer("sweet",null));
            Assert.That(ex.Message, Is.EqualTo("Can not be null! (Parameter 'performerName')"));
        }

        [Test]
        public void AddSongToPerformerThrowsIfSongNotFound()
        {
            stage.AddPerformer(performer);
            ArgumentException ex = Assert.Throws<ArgumentException>(() => stage.AddSongToPerformer("sweet", "test testov"));
            Assert.That(ex.Message, Is.EqualTo("There is no song with this name."));        
        } 

        [Test]
        public void AddSongToPerformerThrowsIfPerformerNotFound()
        {
            stage.AddSong(song);
            ArgumentException ex = Assert.Throws<ArgumentException>(() => stage.AddSongToPerformer("sweet", "test testov"));
            Assert.That(ex.Message, Is.EqualTo("There is no performer with this name."));
        }

        
    }
}