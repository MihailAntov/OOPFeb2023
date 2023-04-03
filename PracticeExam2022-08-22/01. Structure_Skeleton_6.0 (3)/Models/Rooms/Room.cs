using BookingApp.Models.Rooms.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using BookingApp.Utilities.Messages;

namespace BookingApp.Models.Rooms
{
    public abstract class Room : IRoom
    {
        protected int bedCapacity;
        private double pricePerNight;

        protected Room(int bedCapacity)
        {
            this.bedCapacity = bedCapacity;
            PricePerNight = 0;
        }
        public int BedCapacity { get { return bedCapacity; }   }

        public double PricePerNight
        {
            get {  return pricePerNight; }
            private set
            {
                if(value < 0)
                {
                    throw new ArgumentException(ExceptionMessages.PricePerNightNegative);
                }
                pricePerNight = value;
            }
        }

        public void SetPrice(double price)
        {
            PricePerNight = price;
        }
    }
}
