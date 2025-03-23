using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotellMenu.Entities
{
    public class Bookings
    {
        public int BookingsId { get; set; }

        public DateTime CheckInDate { get; set; }

        public DateTime TotalStay { get; set; }

        public int NbrOfGuests { get; set; }

        public Customers Customers { get; set; }

        public HotelRooms HotelRooms { get; set; }

    }
}
