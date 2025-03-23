using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotellMenu.Entities
{
    public class HotelRooms
    {

        public int HotelRoomsId { get; set; }

        public int RoomNumber { get; set; }

        public bool IsDouble { get; set; }

        public bool RoomAvaliability { get; set; }

        public int RoomSize { get; set; }

        public int NbrExtraBeds { get; set; }

        public List<Bookings> Bookings { get; set; } = null!;

    }
}
