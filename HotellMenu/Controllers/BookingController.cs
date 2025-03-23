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
            Console.WriteLine("Ange incheckningsdatum:");
            DateTime checkInDate = DateTime.Parse(Console.ReadLine());

            Console.WriteLine("Hur många nätter vill du övernatta? :");
            int chosenDays = int.Parse(Console.ReadLine());
            DateTime totalStay = checkInDate.AddDays(chosenDays);

            Console.WriteLine("Ange antal gäster: ");
            int nbrOfGuests = int.Parse(Console.ReadLine());

            return (checkInDate, totalStay, nbrOfGuests);
        }
    }
}
