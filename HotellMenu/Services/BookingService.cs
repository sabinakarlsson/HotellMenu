using HotellMenu.Contexts;
using HotellMenu.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotellMenu.Services
{
    public class BookingService
    {
        ApplicationDbContext _dbContext;

        public BookingService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

        }


        public void AddBooking(Bookings booking)
        {
            _dbContext.Bookings.Add(booking);
            _dbContext.SaveChanges();
        }

        public void UpdateBooking(Bookings booking)
        {
            _dbContext.Bookings.Update(booking);
            _dbContext.SaveChanges();
        }

        public void DeleteBooking(int bookingId)
        {
            var booking = _dbContext.Bookings.FirstOrDefault(c => c.BookingsId == bookingId);
            if (booking != null)
            {
                _dbContext.Bookings.Remove(booking);
                _dbContext.SaveChanges();
            }
        }

        public List<Bookings> ShowAllBookings()
        {
            return _dbContext.Bookings.ToList();
        }


        public Bookings ShowBookingById(int bookingId)
        {
            return _dbContext.Bookings.FirstOrDefault(r => r.BookingsId == bookingId);
        }

    }

}
