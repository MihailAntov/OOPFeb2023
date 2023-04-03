using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BookingApp.Utilities.Messages;
using BookingApp.Repositories;

namespace BookingApp.Models.Hotels
{
    public class Hotel : IHotel
    {
        private string fullname;
        private int category;
        private IRepository<IRoom> rooms;
        private IRepository<IBooking> bookings;

        public Hotel(string fullName, int category)
        {
            FullName = fullName;
            Category = category;
            Rooms = new RoomRepository();
            Bookings = new BookingRepository();
        }
        
        public string FullName
        {
            get { return fullname; }
            private set
            {
                if(string.IsNullOrWhiteSpace(value))
                {
                    throw new ArgumentException(ExceptionMessages.HotelNameNullOrEmpty);
                }
                fullname = value;
            }
        }

        public int Category
        {
            get { return category; }
            private set
            {
                if(value < 1 || value > 5)
                {
                    throw new ArgumentException(ExceptionMessages.InvalidCategory);
                }
                category = value;
            }
        }

        public double Turnover
        {
            get => Math.Round(bookings.All().Sum(b => b.ResidenceDuration * b.Room.PricePerNight),2);
        }

        public IRepository<IRoom> Rooms
        {
            get { return rooms; }
            set
            {
                rooms = value;
            }
        }

        public IRepository<IBooking> Bookings
        {
            get { return bookings; }
            set
            {
                bookings = value;
            }
        }
    }
}
