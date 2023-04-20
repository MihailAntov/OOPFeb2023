using FrontDeskApp;
using NUnit.Framework;
using System;
using System.Linq;

namespace BookigApp.Tests
{
    public class Tests
    {
        Room room;
        Hotel hotel;
        
        [SetUp]
        public void Setup()
        {
            room = new Room(2, 10);
            hotel = new Hotel("hotelName", 3);
        }

        [Test]
        public void RoomConstructorSetsProperties()
        {
            room = new Room(4, 25.25);
            Assert.That(room, Is.Not.Null);
            Assert.That(room.BedCapacity, Is.EqualTo(4));
            Assert.That(room.PricePerNight, Is.EqualTo(25.25));
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-15)]
        public void RoomConstructorThrowsIfBedCapacityZeroOrNegative(int bedCapacity)
        {
            Assert.Throws<ArgumentException>(() => room = new Room(bedCapacity, 10));
        }
        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-10.37)]
        public void RoomConstructorThrowsIfPricePerNightZeroOrNegative(double pricePerNight)
        {
            Assert.Throws<ArgumentException>(() => room = new Room(2, pricePerNight));
        }
        [Test]
        public void HotelConstructorSetsProperties()
        {
            hotel = new Hotel("testName", 1);
            Assert.That(hotel, Is.Not.Null);
            Assert.That(hotel.FullName, Is.EqualTo("testName"));
            Assert.That(hotel.Category, Is.EqualTo(1));
        }
        [TestCase("")]
        [TestCase(null)]
        [TestCase(" ")]
        [TestCase("     ")]
        public void HotelConstructorThrowsIfNameNullOrWhitespace(string hotelName)
        {
            Assert.Throws<ArgumentNullException>(() => hotel = new Hotel(hotelName, 3));
        }

        [TestCase(0)]
        [TestCase(6)]
        [TestCase(-25)]
        [TestCase(1400)]
        public void HotelConstructorThrowsIfCategoryNotBetween1And5(int category)
        {
            Assert.Throws<ArgumentException>(() => hotel = new Hotel("testName", category));
        }

        [Test]
        public void AddRoomWorksProperly()
        {
            Assert.That(hotel.Rooms, Is.Not.Null);
            Assert.That(hotel.Rooms.Count, Is.EqualTo(0));
            hotel.AddRoom(room);
            Assert.That(hotel.Rooms, Is.Not.Null);
            Assert.That(hotel.Rooms.Count, Is.EqualTo(1));
            
        }

        [Test]
        public void RoomsReturnsCorrectItems()
        {
            Assert.That(hotel.Rooms.FirstOrDefault(), Is.Null);
            hotel.AddRoom(room);
            Assert.That(hotel.Rooms.FirstOrDefault(), Is.SameAs(room));
        }

        [Test]
        public void BookingsReturnsCorrectItems()
        {
            Assert.That(hotel.Bookings.FirstOrDefault(), Is.Null);
            hotel.AddRoom(room);
            Assert.That(hotel.Bookings.FirstOrDefault(), Is.Null);
            hotel.BookRoom(1, 1, 1, 100);
            Assert.That(hotel.Bookings.FirstOrDefault(), Is.Not.Null);
        }

        [Test]
        public void TurnoverReturnsZeroWithEmptyHotel()
        {
            Assert.That(hotel.Turnover, Is.EqualTo(0));
        }

        [Test]
        public void TurnoverReturnsCorrectValue()
        {
            hotel.AddRoom(room);
            hotel.BookRoom(1, 1, 2, 100);
            Assert.That(hotel.Turnover, Is.EqualTo(20));
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-239)]
        public void BookRoomThrowsIfAdultsZeroOrLess(int adults)
        {
            hotel.AddRoom(room);
            Assert.Throws<ArgumentException>(() => hotel.BookRoom(adults,0,2,100));
        }

        
        [TestCase(-1)]
        [TestCase(-239)]
        public void BookRoomThrowsIfChildrenLessThanZero(int children)
        {
            hotel.AddRoom(room);
            Assert.Throws<ArgumentException>(() => hotel.BookRoom(1, children, 2, 100));
        }

        [TestCase(0)]
        [TestCase(-1)]
        [TestCase(-1153)]
        public void BookRoomThrowsIfDurationZeroOrLess(int nights)
        {
            hotel.AddRoom(room);
            Assert.Throws<ArgumentException>(() => hotel.BookRoom(1, 0, nights, 100));
        }

        [Test]
        public void BookRoomFailsIfNotEnoughBeds()
        {
            Assert.That(hotel.Bookings.Count, Is.EqualTo(0));
            hotel.BookRoom(4, 4, 2, 100);
            Assert.That(hotel.Bookings.Count, Is.EqualTo(0));

        }

        [Test]
        public void BookRoomFailsIfPriceHigherThanBudger()
        {
            Assert.That(hotel.Bookings.Count, Is.EqualTo(0));
            hotel.BookRoom(1, 1, 2, 10);
            Assert.That(hotel.Bookings.Count, Is.EqualTo(0));
        }

        [Test]
        public void BookRoomAddsBookingCorrectly()
        {
            Assert.That(hotel.Bookings.Count, Is.EqualTo(0));
            hotel.AddRoom(room);
            hotel.BookRoom(1, 1, 2, 100);
            Assert.That(hotel.Bookings.Count, Is.EqualTo(1));
            Booking booking = hotel.Bookings.First();
            Assert.That(booking.ResidenceDuration, Is.EqualTo(2));
            Assert.That(booking.BookingNumber, Is.EqualTo(1));

            Assert.That(booking.Room, Is.SameAs(room));
        }

        [Test]
        public void BookingConstructorSetsProperties()
        {
            Booking booking = new Booking(1, room, 3);
            Assert.That(booking.Room, Is.SameAs(room));
            Assert.That(booking.ResidenceDuration, Is.EqualTo(3));
            Assert.That(booking.BookingNumber, Is.EqualTo(1));  
        }

   
    }
}