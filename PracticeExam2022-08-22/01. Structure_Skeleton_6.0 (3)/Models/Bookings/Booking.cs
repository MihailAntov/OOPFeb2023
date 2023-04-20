using BookingApp.Models.Bookings.Contracts;
using BookingApp.Models.Rooms.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BookingApp.Utilities.Messages;

namespace BookingApp.Models.Bookings
{
    public class Booking : IBooking
    {
        private IRoom room;
        private int residenceDuration;
        private int adultsCount;
        private int childrenCount;
        private int bookingNumber;


        public Booking(IRoom room, int residenceDuration, int adultsCount, int childrenCount, int bookingNumber)
        {
            Room = room;
            ResidenceDuration = residenceDuration;
            AdultsCount = adultsCount;
            ChildrenCount = childrenCount;
            this.bookingNumber = bookingNumber;
        }
        public IRoom Room
        {
            get { return room; }
            private set
            {
                room = value; 
            }
        }

        public int ResidenceDuration
        {
            get { return residenceDuration; }
            private set
            {
                if(value <= 0)
                {
                    throw new ArgumentException(ExceptionMessages.DurationZeroOrLess);
                }
                residenceDuration = value;
            }
        }

        public int AdultsCount
        {
            get { return adultsCount; }
            private set
            {
                if(value < 1)
                {
                    throw new ArgumentException(ExceptionMessages.AdultsZeroOrLess);
                }

                adultsCount = value;
            }
        }

        public int ChildrenCount
        {
            get { return childrenCount; }
            private set
            {
                if(value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.ChildrenNegative);
                }

                childrenCount = value;
            }
        }

        public int BookingNumber
        {
            get { return bookingNumber; }
            
        }



        public string BookingSummary()
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine($"Booking number: {BookingNumber}");
            sb.AppendLine($"Room type: {this.GetType().Name}");
            sb.AppendLine($"Adults: {AdultsCount} Children: {ChildrenCount}");
            double totalPaid = Math.Round(ResidenceDuration * Room.PricePerNight, 2);
            sb.AppendLine($"Total amount paid: {totalPaid:F2} $");

            return sb.ToString().TrimEnd();
        }
    }
}
