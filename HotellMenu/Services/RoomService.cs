using HotellMenu.Contexts;
using HotellMenu.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotellMenu.Services
{
    public class RoomService
    {
        ApplicationDbContext _dbContext;

        public RoomService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddHotelRoom(HotelRooms hotelRoom)
        {
            _dbContext.HotelRooms.Add(hotelRoom);
            _dbContext.SaveChanges();
        }

        public void UpdateHotelRoom(HotelRooms hotelRoom)
        {
            _dbContext.HotelRooms.Update(hotelRoom);
            _dbContext.SaveChanges();
        }


        public void DeleteHotelRoom(int hotelRoomId)
        {
            var hotelRoom = _dbContext.HotelRooms.FirstOrDefault(r => r.HotelRoomsId == hotelRoomId);
            if (hotelRoom != null)
            {
                _dbContext.HotelRooms.Remove(hotelRoom);
                _dbContext.SaveChanges();
            }
        }
        
        public List<HotelRooms> ShowAllHotelRoom()
        {
            return _dbContext.HotelRooms.ToList();
        }


        public HotelRooms GetHotelRoomById(int hotelRoomId)
        {
            return _dbContext.HotelRooms.FirstOrDefault(r => r.HotelRoomsId == hotelRoomId);
        }

        public bool IsRoomNumberTaken(int roomNumber)
        {
            return _dbContext.HotelRooms.Any(r => r.RoomNumber == roomNumber);
        }

        public bool IsRoomAvailable(int roomNumber)
        {
            return _dbContext.HotelRooms.Any(r => r.RoomNumber == roomNumber && r.RoomAvailability == true);
        }

        public List<HotelRooms> AllAvailableRooms()
        {
            return _dbContext.HotelRooms.Where(r => r.RoomAvailability == true).ToList();
        }



    }
}
