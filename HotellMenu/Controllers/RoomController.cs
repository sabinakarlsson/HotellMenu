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
            
            int roomNumber;
            while (true)
            {
                Console.WriteLine("Ange rummets rumsnummer: ");
                if (int.TryParse(Console.ReadLine(), out roomNumber) && !_roomService.IsRoomNumberTaken(roomNumber))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Inte ett giltigt rumsnummer eller så är rumsnumret redan upptaget. Försök igen");
                }
            }

            bool isDouble;
            while (true)
            {
                Console.WriteLine("Ange om rummet är dubbelrum (true/false): ");
                if (bool.TryParse(Console.ReadLine(), out isDouble))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Inte en giltig inmatning, ange true eller false");
                }
            }

            bool isAvaliable;
            while (true)
            {
                Console.WriteLine("Ange om rummet är ledigt (true/false): ");
                if (bool.TryParse(Console.ReadLine(), out isAvaliable))
                {
                    break;
                }
                else
                {
                    Console.WriteLine("Inte en giltig inmatning, ange true eller false");
                }

            }

            int roomSize;
            int[] validRoomSizes = { 20, 30, 40 };
            while (true)
            {
                Console.WriteLine("Ange om storlek på rummet: (20/30/40) kvm ");
                if (int.TryParse(Console.ReadLine(), out roomSize) || validRoomSizes.Contains(roomSize))
                {
                    var validRoomSize = isDouble ? new int[] { 30, 40 }.Contains(roomSize) : roomSize == 20;

                    if (!validRoomSize)
                    {
                        if (isDouble)
                        {
                            Console.WriteLine("Ett dubbelrum kan endast vara 30 eller 40 kvm");
                        }
                        else
                        {
                            Console.WriteLine("Ett singelrum kan endast vara 20 kvm.");
                        }
                    }
                    else
                    {
                        break;
                    }

                }
                else
                {
                    Console.WriteLine("Inte giltig storlek. Vänligen ange 20, 30 eller 40 kvm.");
                }
            }


            int[] validNbrExtraBeds = { 0, 1, 2 };
            int nbrExtraBeds;
            while (true)
            {
                Console.WriteLine("Ange antal extra sängar: (0/1/2) ");
                Console.WriteLine("OBS! Singelrum kan inte ha några extrasängar, dubbelrum 30 kvm max 1 extrasäng, dubbelrum 40 kvm max 2 ");
                if (int.TryParse(Console.ReadLine(), out nbrExtraBeds) && validNbrExtraBeds.Contains(nbrExtraBeds))
                {
                    var validNbrExtraBedPerRoomSize = new[]
                    {
                        new { RoomSize = 20, MaxExtraBeds = 0 },
                        new { RoomSize = 30, MaxExtraBeds = 1 },
                        new { RoomSize = 40, MaxExtraBeds = 2 }
                    };

                    var validNbrExtraBed = validNbrExtraBedPerRoomSize.FirstOrDefault(r => r.RoomSize == roomSize)?.MaxExtraBeds;

                    if (nbrExtraBeds > validNbrExtraBed)
                    {
                        Console.WriteLine($"För många extrasängar, {roomSize} kan max ha {validNbrExtraBed} extrasängar");
                    }
                    else
                    {
                        break;
                    }
                }

                else
                {
                    Console.WriteLine("Inte giltigt antal extra sängar, du behöver välja 0, 1 eller 2");
                }
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
            Console.ReadLine();
        }


        public void EditRoom()
        {
            Console.Clear();
            ShowAllRooms();

            int hotelRoomId;
            HotelRooms room = null;
            while (true)
            {
                Console.WriteLine("Ange rumsID på rummet du vill redigera: ");

                if (int.TryParse(Console.ReadLine(), out hotelRoomId))
                {
                    room = _roomService.ShowHotelRoomById(hotelRoomId);

                    if (room != null)
                    {
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Inte ett giltigt rumsID eller så finns inte rummet. Försök igen");
                }

                Console.WriteLine("Du redigerar nu: " + room.HotelRoomsId);
            }
            
            int newRoomNumber;
            while (true)
            {
                Console.WriteLine("Ange nytt rumsnummer: ");
                if (int.TryParse(Console.ReadLine(), out newRoomNumber) && !_roomService.IsRoomNumberTaken(newRoomNumber))
                {
                    room.RoomNumber = newRoomNumber;
                    break;
                }
                else
                {
                    Console.WriteLine("Inte ett giltigt rumsnummer eller så är rumsnumret redan upptaget. Försök igen");
                }
            }

            bool isDouble;
            while (true)
            {
                Console.WriteLine("Ange om rummet är dubbelrum (true/false): ");
                if (bool.TryParse(Console.ReadLine(), out isDouble))
                {
                    room.IsDouble = isDouble;
                    break;
                }
                else
                {
                    Console.WriteLine("Inte en giltig inmatning, ange true eller false");
                }
            }

            bool isAvaliable;
            while (true)
            {
                Console.WriteLine("Ange om rummet är ledigt (true/false): ");
                if (bool.TryParse(Console.ReadLine(), out isAvaliable))
                {
                    room.RoomAvaliability = isAvaliable;
                    break;
                }
                else
                {
                    Console.WriteLine("Inte en giltig inmatning, ange true eller false");
                }
            }

            int roomSize;
            int[] validRoomSizes = { 20, 30, 40 };
            while (true)
            {
                Console.WriteLine("Ange storlek på rummet: (20/30/40) kvm ");
                if (int.TryParse(Console.ReadLine(), out roomSize) && validRoomSizes.Contains(roomSize))
                {
                    var validRoomSize = isDouble ? new int[] { 30, 40 }.Contains(roomSize) : roomSize == 20;
                    if (!validRoomSize)
                    {
                        if (isDouble)
                        {
                            Console.WriteLine("Ett dubbelrum kan endast vara 30 eller 40 kvm");
                        }
                        else
                        {
                            Console.WriteLine("Ett singelrum kan endast vara 20 kvm.");
                        }
                    }
                    else
                    {
                        room.RoomSize = roomSize;
                        break;
                    }
                }
                else
                {
                    Console.WriteLine("Inte giltig storlek. Vänligen ange 20, 30 eller 40 (kvm) enbart i siffor.");
                }
            }

            int nbrExtraBeds;
            int[] validNbrExtraBeds = { 0, 1, 2 };
            while (true)
            {
                Console.WriteLine("Ange antal extra sängar: (0/1/2) ");
                Console.WriteLine("OBS! Singelrum kan inte ha några extrasängar, dubbelrum 30 kvm max 1 extrasäng, dubbelrum 40 kvm max 2 ");
                if (int.TryParse(Console.ReadLine(), out nbrExtraBeds) && validNbrExtraBeds.Contains(nbrExtraBeds))
                {
                    var validNbrExtraBedPerRoomSize = new[]
                    {
                        new { RoomSize = 20, MaxExtraBeds = 0 },
                        new { RoomSize = 30, MaxExtraBeds = 1 },
                        new { RoomSize = 40, MaxExtraBeds = 2 }
                    };


                    var validNbrExtraBed = validNbrExtraBedPerRoomSize.FirstOrDefault(r => r.RoomSize == roomSize)?.MaxExtraBeds;

                    if (nbrExtraBeds > validNbrExtraBed)
                    {
                        Console.WriteLine($"För många extrasängar, {roomSize} kan max ha {validNbrExtraBed} extrasängar");
                    }
                    else
                    {
                        room.NbrExtraBeds = nbrExtraBeds;
                        break;
                    }
                }

                else
                {
                    Console.WriteLine("Inte giltigt antal extra sängar, du behöver välja 0, 1 eller 2");

                }
            }

            _roomService.UpdateHotelRoom(room);
            Console.WriteLine("Rummet har uppdaterats!");
            Console.ReadKey();
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
