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
            Console.Clear();
            ShowAllRooms();
            Console.WriteLine("Ange rummets rumsnummer: ");
            if (_roomService.IsRoomNumberTaken(int.Parse(Console.ReadLine())))
            {
                Console.WriteLine("Rumsnumret är upptaget");
                return;
            }
            if (!int.TryParse(Console.ReadLine(), out int roomNumber))
            {
                Console.WriteLine("Inte ett giltigt rumsnummer");
                return;
            }

            Console.WriteLine("Ange om rummet är dubbelrum (true/false): ");

            if (!bool.TryParse(Console.ReadLine(), out bool isDouble))
            {
                Console.WriteLine("Inte en giltig inmatning, ange true eller false");
                return;
            }

            Console.WriteLine("Ange om rummet är ledigt (true/false): ");
            if (!bool.TryParse(Console.ReadLine(), out bool isAvaliable))
            {
                Console.WriteLine("Inte en giltig inmatning, ange true eller false");
                return;
            }

            Console.WriteLine("Ange om storlek på rummet: (20/30/40) kvm ");
            if (!int.TryParse(Console.ReadLine(), out int roomSize) || (roomSize != 20 && roomSize != 30 && roomSize != 40))
            {
                Console.WriteLine("Inte giltig storlek. Vänligen ange 20, 30 eller 40 kvm.");
                return;
            }

            if (isDouble)
            {
                if (roomSize == 30 || roomSize == 40)
                {
                    
                }
                else
                {
                    Console.WriteLine("Ett dubbelrum kan endast vara 30 eller 40 kvm");
                    return;
                }
            }
            else
            {
                if (roomSize != 20)
                {
                    Console.WriteLine("Ett singelrum kan endast vara 20 kvm.");
                    return;
                }
            }


            Console.WriteLine("Ange antal extra sängar: (0/1/2) ");
            if (!int.TryParse(Console.ReadLine(), out int nbrExtraBeds))
            {
                Console.WriteLine("Inte giltigt antal extra sängar, du behöver välja 0, 1 eller 2");
                return;
            }

            if (nbrExtraBeds < 2)
            {
                Console.WriteLine("För många extrasängar");
            }

            if (roomSize == 20 && nbrExtraBeds > 0)
            {
                Console.WriteLine("Ett rum på 20 kvm kan inte ha några extrasängar.");
                return;
            }

            if (roomSize == 30 && nbrExtraBeds > 1)
            {
                Console.WriteLine("Ett rum på 30 kvm kan max ha 1 extrasäng");
                return;
            }

            else if (roomSize == 40 && nbrExtraBeds > 2)
            {
                Console.WriteLine("Ett rum på 40 kvm kan ha max 2 extrasängar");
                return;
            }

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
            Console.Clear();
            ShowAllRooms();
            Console.WriteLine("Ange rumsID på rummet du vill redigera: ");

            if (!int.TryParse(Console.ReadLine(), out int roomId))
            {
                Console.WriteLine("Inte ett giltigt rumsID");
                return;
            }

            var room = _roomService.ShowHotelRoomById(roomId);

            if (room == null)
            {
                Console.WriteLine("Rummet finns inte");
                return;
            }

            Console.WriteLine("Ange nytt rumsnummer: ");
            if (!int.TryParse(Console.ReadLine(), out int newRoomNumber))
            {
                Console.WriteLine("Inte ett giltigt rumsnummer");
                return;
            }
            room.RoomNumber = newRoomNumber;

            Console.WriteLine("Ange om rummet är dubbelrum (true/false): ");
            if (!bool.TryParse(Console.ReadLine(), out bool isDouble))
            {
                Console.WriteLine("Inte en giltig inmatning, ange true eller false");
                return;
            }
            room.IsDouble = isDouble;

            Console.WriteLine("Ange om rummet är ledigt (true/false): ");
            if (!bool.TryParse(Console.ReadLine(), out bool isAvaliable))
            {
                Console.WriteLine("Inte en giltig inmatning, ange true eller false");
                return;
            }
            room.RoomAvaliability = isAvaliable;

            Console.WriteLine("Ange storlek på rummet: (20/30/40) kvm ");
            if (!int.TryParse(Console.ReadLine(), out int roomSize) || (roomSize != 20 && roomSize != 30 && roomSize != 40))
            {
                Console.WriteLine("Inte giltig storlek. Vänligen ange 20, 30 eller 40 kvm.");
                return;
            }

            if (room.IsDouble)
            {
                if (roomSize == 30 || roomSize == 40)
                {
                    room.RoomSize = roomSize;
                }
                else
                {
                    Console.WriteLine("Ett dubbelrum kan endast vara 30 eller 40 kvm");
                    return;
                }
            }

            else
            {
                if (roomSize != 20)
                {
                    Console.WriteLine("Ett singelrum kan endast vara 20 kvm.");
                    return;
                }

                room.RoomSize = roomSize;
            }


            
            Console.WriteLine("Ange antal extra sängar: (0/1/2) ");
            if (!int.TryParse(Console.ReadLine(), out int nbrExtraBeds))
            {
                Console.WriteLine("Inte giltigt antal extra sängar, du behöver välja 0, 1 eller 2");
                return;
            }

            if (nbrExtraBeds < 2)
            {
                Console.WriteLine("För många extrasängar");
            }

            if (room.RoomSize == 20 && nbrExtraBeds > 0)
            {
                    Console.WriteLine("Ett rum på 20 kvm kan inte ha några extrasängar.");
                    return;
            }

            if (room.RoomSize == 30 && nbrExtraBeds > 1)
            {
                    Console.WriteLine("Ett rum på 30 kvm kan max ha 1 extrasäng");
                    return;
            }

            else if (room.RoomSize == 40 && nbrExtraBeds > 2)
            {
                    Console.WriteLine("Ett rum på 40 kvm kan ha max 2 extrasängar");
                    return;
            }

            room.NbrExtraBeds = nbrExtraBeds;

            _roomService.UpdateHotelRoom(room);
            Console.WriteLine("Rummet har uppdaterats!");
        }

        public void ShowAllRooms()
        {
            Console.Clear();
            var rooms = _roomService.ShowAllHotelRoom();
            foreach (var room in rooms)
            {
                Console.WriteLine($"RumsID: {room.HotelRoomsId}, Rumsnummer: {room.RoomNumber}, Dubbelrum: {room.IsDouble}, Ledigt: {room.RoomAvaliability}, Storlek: {room.RoomSize}, Antal extra sängar: {room.NbrExtraBeds}");
            }
        }

        public void DeleteRoom()
        {
            Console.Clear();
            ShowAllRooms();

            Console.WriteLine("Ange rumsId på rummet du vill radera: ");
            int roomId = int.Parse(Console.ReadLine());
            _roomService.DeleteHotelRoom(roomId);
            Console.WriteLine("Rummet har raderats!");
            Console.ReadLine();



        }
    }
}
