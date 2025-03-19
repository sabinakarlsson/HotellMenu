using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotellMenu.Entities
{
    public class Customers
    {
        public int CustomersId { get; set; }

        public string CustomerName { get; set; }

        public string Email { get; set; }

        public List<Bookings> Booking { get; set; } = null!;
    }
}
