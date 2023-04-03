using BookingApp.Core.Contracts;
using BookingApp.Models.Hotels.Contacts;
using BookingApp.Repositories;
using BookingApp.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BookingApp.Utilities.Messages;
using BookingApp.Models.Hotels;
using BookingApp.Models.Rooms.Contracts;
using BookingApp.Models.Rooms;
using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Bookings;

namespace BookingApp.Core
{
    public class Controller : IController
    {
        private IRepository<IHotel> hotels;
        private string[] allowedRoomTypes = { "Apartment", "DoubleBed", "Studio" };
        public Controller()
        {
            hotels = new HotelRepository();
        }
        public string AddHotel(string hotelName, int category)
        {
            if (hotels.All().Any(h=>h.FullName == hotelName))
            {
                return string.Format(OutputMessages.HotelAlreadyRegistered,hotelName);
            }

            hotels.AddNew(new Hotel(hotelName, category));
            return String.Format(OutputMessages.HotelSuccessfullyRegistered, category, hotelName);
                
        }

        public string BookAvailableRoom(int adults, int children, int duration, int category)
        {
            
            if(!hotels.All().Any(h=>h.Category == category))
            {
                return String.Format(OutputMessages.CategoryInvalid, category);
            }


            foreach(var hotel in hotels.All()
                .Where(h=>h.Category == category)
                .OrderBy(h=>h.FullName))
            {
                if(!hotel.Rooms.All()
                    .Where(r=>r.PricePerNight > 0)
                    .Any(r=>r.BedCapacity >= adults + children))
                {
                    continue;
                }

                IRoom room = hotel.Rooms.All()
                    .Where(r => r.PricePerNight > 0)
                    .OrderBy(r => r.BedCapacity)
                    .FirstOrDefault(r => r.BedCapacity >= adults + children);
                IBooking booking = new Booking(room, duration, adults, children, hotel.Bookings.All().Count + 1);
                hotel.Bookings.AddNew(booking);

                return String.Format(OutputMessages.BookingSuccessful, booking.BookingNumber, hotel.FullName);
            }

            return String.Format(OutputMessages.RoomNotAppropriate, category);

        }

        public string HotelReport(string hotelName)
        {
            IHotel hotel = hotels.All().FirstOrDefault(h => h.FullName == hotelName);

            if(hotel == null)
            {
                return String.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            StringBuilder sb = new StringBuilder();

            sb.AppendLine($"Hotel name: {hotel.FullName}");
            sb.AppendLine($"--{hotel.Category} star hotel");
            sb.AppendLine($"--Turnover: {hotel.Turnover:f2} $");
            sb.AppendLine($"--Bookings:");

            if(!hotel.Bookings.All().Any())
            {
                sb.AppendLine();
                sb.AppendLine("none");
            }

            foreach(var booking in hotel.Bookings.All())
            {
                sb.AppendLine();
                sb.AppendLine($"Booking number: {booking.BookingNumber}");
                sb.AppendLine($"Room type: {booking.Room.GetType().Name}");
                sb.AppendLine($"Adults: {booking.AdultsCount} Children: {booking.ChildrenCount}");
                sb.AppendLine($"Total amount paid: {booking.Room.PricePerNight * booking.ResidenceDuration:f2} $");
            }

            return sb.ToString().TrimEnd();

            
        }

        public string SetRoomPrices(string hotelName, string roomTypeName, double price)
        {
            if (!hotels.All().Any(h => h.FullName == hotelName))
            {
                return String.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            IHotel hotel = hotels.All().First(h => h.FullName == hotelName);

            if (!allowedRoomTypes.Contains(roomTypeName))
            {
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
            }

            if (!hotel.Rooms.All().Any(r => r.GetType().Name == roomTypeName))
            {
                return String.Format(OutputMessages.RoomTypeNotCreated);
            }

            IRoom room = hotel.Rooms.All().First(r => r.GetType().Name == roomTypeName);
            if(room.PricePerNight != 0)
            {
                throw new InvalidOperationException(ExceptionMessages.PriceAlreadySet);
            }

            room.SetPrice(price);

            return String.Format(OutputMessages.PriceSetSuccessfully, roomTypeName, hotelName);

        }

        public string UploadRoomTypes(string hotelName, string roomTypeName)
        {
            if(!hotels.All().Any(h=>h.FullName == hotelName))
            {
                return String.Format(OutputMessages.HotelNameInvalid, hotelName);
            }

            IHotel hotel = hotels.All().First(h => h.FullName == hotelName);
            if(hotel.Rooms.All().Any(r=>r.GetType().Name == roomTypeName))
            {
                return String.Format(OutputMessages.RoomTypeAlreadyCreated);
            }

            if (!allowedRoomTypes.Contains(roomTypeName))
            {
                throw new ArgumentException(ExceptionMessages.RoomTypeIncorrect);
            }


            IRoom room = null!;
            switch(roomTypeName)
            {
                case nameof(Studio):
                    room = new Studio();
                    break;
                case nameof(Apartment):
                    room = new Apartment();
                    break;
                case nameof(DoubleBed):
                    room = new DoubleBed();
                    break;
            }

            hotel.Rooms.AddNew(room);

            return String.Format(OutputMessages.RoomTypeAdded, roomTypeName, hotelName);
        }

        
    }
}
