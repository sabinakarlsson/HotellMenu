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


        public void AddNewRoom()
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


        public void EditRoom()
        {
            var avaliableRoomNumbers = _roomService.ShowAllHotelRoom();

            Console.WriteLine("Ange rumsnummer på rummet du vill redigera: ");
            int roomNumber = int.Parse(Console.ReadLine());
            var room = _roomService.ShowHotelRoomById(roomNumber);
            if (room == null)
            {
                Console.WriteLine("Rummet finns inte!");
                return;
            }
            Console.WriteLine("Ange nytt rumsnummer: ");
            room.RoomNumber = int.Parse(Console.ReadLine());
            Console.WriteLine("Ange om rummet är dubbelrum (true/false): ");
            room.IsDouble = bool.Parse(Console.ReadLine());
            Console.WriteLine("Ange om rummet är ledigt (true/false): ");
            room.RoomAvaliability = bool.Parse(Console.ReadLine());
            Console.WriteLine("Ange om storlek på rummet: (20/30/40) kvm ");
            room.RoomSize = int.Parse(Console.ReadLine());
            Console.WriteLine("Ange antal extra sängar: (0/1/2) ");
            room.NbrExtraBeds = int.Parse(Console.ReadLine());
            _roomService.UpdateHotelRoom(room);
            Console.WriteLine("Rummet har uppdaterats!");
        }

        public void ShowAllRooms()
        {
            var rooms = _roomService.ShowAllHotelRoom();
            foreach (var room in rooms)
            {
                Console.WriteLine($"Rumsnummer: {room.RoomNumber}, Dubbelrum: {room.IsDouble}, Ledigt: {room.RoomAvaliability}, Storlek: {room.RoomSize}, Antal extra sängar: {room.NbrExtraBeds}");
                
            }
            Console.ReadLine();
        }

        public void DeleteRoom()
        {
            Console.WriteLine("Ange rumsnummer på rummet du vill radera: ");
            int roomNumber = int.Parse(Console.ReadLine());
            _roomService.DeleteHotelRoom(roomNumber);
            Console.WriteLine("Rummet har raderats!");
        }


    }
}
