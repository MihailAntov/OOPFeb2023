using BookingApp.Models.Rooms.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookingApp.Models.Rooms
{
    public class Studio : Room , IRoom
    {
        private const int StudioCapacity = 4;
        public Studio() : base(StudioCapacity)
        {
        }
    }
}
