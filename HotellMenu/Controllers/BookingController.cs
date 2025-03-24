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
    public class BookingController
    {
        public BookingService _bookingService;
        public RoomController _roomController;
        public RoomService _roomService;

        public BookingController(BookingService bookingService)
        {
            _bookingService = bookingService;
            //_roomController = roomController;
            //_roomService = roomService;
            //RoomController roomController, RoomService roomService (inne i paratesen)
        }



        public void AddBooking(ApplicationDbContext dbContext)
        {
            bool isRunning = true;
            
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("1. Checka in i ett av hotellets tillgängliga rum");
                Console.WriteLine("2. Skapa ett nytt rum");
                Console.WriteLine("3. Exit");
                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.KeyChar)
                {
                    case '1':
                        BookingInfo();
                        var bookingDetails = BookingInfo();
                        int nbrOfGuests = bookingDetails.nbrOfGuests;
                        _roomController.ShowAllAvaliableRoomsWithGuestNbr(nbrOfGuests);
                        Console.WriteLine("");
                        Console.WriteLine("Ange rumsId på ovan rum som vill du checka in i: ");
                        int roomId = int.Parse(Console.ReadLine());

                        var newBooking = new Bookings
                        {
                            CheckInDate = bookingDetails.checkInDate,
                            TotalStay = bookingDetails.totalStay,
                            NbrOfGuests = bookingDetails.nbrOfGuests
                        };

                        _bookingService.AddBooking(newBooking);
                        Console.WriteLine("Bokningen är nu genomförd.");
                        Console.WriteLine("Vill du koppla bokningen till en befintlig kund [1] eller skapa en ny kund [2]");
                        Console.ReadLine();
                        break;

                    case '2':
                        _roomController.AddNewRoom();
                        break;

                    case '3':
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Välj bland menyvalen 1-3");
                        break;

                }
            }
        }

        public (DateTime checkInDate, DateTime totalStay, int nbrOfGuests) BookingInfo()
        {
            bool isRunning = true;
            DateTime checkInDate = DateTime.MinValue;

            while (isRunning)
            {
                Console.WriteLine("Ange incheckningsdatum (i formatet dd/MM/yyyy) :");
                bool IsDateValid = DateTime.TryParse(Console.ReadLine(), out checkInDate);
                if(!IsDateValid)
                {
                    Console.WriteLine("Felaktigt datum, vänligen försök igen");
                }
                if (checkInDate < DateTime.Today)
                {
                    Console.WriteLine("Du kan inte ange ett datum som har passerat, vänligen välj ett annat datum för checkin");
                }
                else
                {
                    isRunning = false;
                }
            }

            Console.WriteLine("Hur många nätter vill du övernatta?");
            int chosenDays;
            while (!int.TryParse(Console.ReadLine(), out chosenDays) || chosenDays <= 0)
            {
                Console.WriteLine("Ogiltigt antal nätter, vänligen ange ett positivt heltal.");
            }
            DateTime totalStay = checkInDate.AddDays(chosenDays);

            bool isRunning2 = true;
            int nbrOfGuests = 0;
            while (isRunning2)
            {
                Console.WriteLine("Ange antal gäster: ");
                if (int.TryParse(Console.ReadLine(), out nbrOfGuests))
                {
                    if (nbrOfGuests > 4)
                    {
                        Console.WriteLine("Vi kan enbart ta emot bokningar med max 4 gäster, vänligen boka vid två separata bokningar.");
                    }
                    else if (nbrOfGuests < 1)
                    {
                        Console.WriteLine("Du måste ange minst en gäst för att kunna boka.");
                    }
                    else
                    {
                        isRunning2 = false;
                    }
                }
                else
                {
                    Console.WriteLine("Felaktig inmatning, vänligen försök igen.");
                }


            }

            return (checkInDate, totalStay, nbrOfGuests);
        }
    }
}
