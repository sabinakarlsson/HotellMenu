using HotellMenu.Contexts;
using HotellMenu.Entities;
using HotellMenu.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotellMenu.Controllers
{
    public class RoomController
    {
        RoomService _roomService;

        public RoomController(RoomService roomService)
        {
            _roomService = roomService;
        }


        public void RegisterNewRoom()
        {
            var avaliableRoomNumbers = _roomService.ShowAllHotelRoom();

            Console.WriteLine("Ange rummets rumsnummer: ");
            int roomNumber = int.Parse(Console.ReadLine());
            Console.WriteLine("Ange om rummet är dubbelrum (true/false): ");
            bool isDouble = bool.Parse(Console.ReadLine());
            Console.WriteLine("Ange om rummet är ledigt (true/false): ");
            bool isAvaliable = bool.Parse(Console.ReadLine());
            Console.WriteLine("Ange om storlek på rummet: (20/30/40) kvm ");
            int roomSize = int.Parse(Console.ReadLine());
            Console.WriteLine("Ange antal extra sängar: (0/1/2) ");
            int nbrExtraBeds = int.Parse(Console.ReadLine());

            var room = new HotelRooms
            {
                RoomNumber = roomNumber,
                IsDouble = isDouble,
                RoomAvaliability = isAvaliable,
                RoomSize = roomSize,
                NbrExtraBeds = nbrExtraBeds
            };
            _roomService.AddHotelRoom(room);

            Console.WriteLine("Rummet har registrerats!");
        }



        /*

        public void UpdateRoom()
        {
            var room = _dbContext.HotelRooms.FirstOrDefault(c => c.HotelRoomsId == hotelRoomsId);
            if (room != null)
            {
                room.IsDouble = newRoomType;
                room.RoomNumber = newRoomNumber;
                room.RoomAvaliability = newRoomAvaliability;
                room.NbrExtraBeds = newNbrExtraBeds;
                _dbContext.SaveChanges();
                Console.WriteLine("Rummets egenskaper har uppdaterats.");
            }
            else
            {
                Console.WriteLine("Rum inte hittat.");
            }
        }*/


    }
}
