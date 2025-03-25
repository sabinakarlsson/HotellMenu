using HotellMenu.Contexts;
using HotellMenu.Entities;
using Microsoft.EntityFrameworkCore;
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

        public bool IsRoomAvailable(int roomId, DateTime checkInDate, DateTime totalStay)
        {
            return !_dbContext.Bookings.Any(b => b.HotelRooms.HotelRoomsId == roomId && b.CheckInDate < totalStay && checkInDate < b.TotalStay);
        }

        public void AddBooking(Bookings booking)
        {
            _dbContext.Bookings.Add(booking);
            _dbContext.SaveChanges();
        }

        public List<Bookings> ShowAllTheBookings()
        {
            return _dbContext.Bookings.Include(h=>h.HotelRooms).Include(c=>c.Customers).ToList();
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
