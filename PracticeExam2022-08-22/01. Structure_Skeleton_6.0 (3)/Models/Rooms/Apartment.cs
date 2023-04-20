using BookingApp.Models.Rooms.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Models.Rooms
{
    public class Apartment : Room, IRoom
    {
        private const int ApartmentCapacity = 6;
        public Apartment() : base(ApartmentCapacity)
        {
        }
    }
}
