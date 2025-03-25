using HotellMenu.Contexts;
using HotellMenu.Entities;
using HotellMenu.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
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
        public CustomerController _customerController;
        public CustomerService _customerService;

        public BookingController(BookingService bookingService, RoomController roomController, RoomService roomService, CustomerController customerController, CustomerService customerService)
        {
            _bookingService = bookingService;
            _roomController = roomController;
            _roomService = roomService;
            _customerController = customerController;
            _customerService = customerService;
            

        }

        public void AddBooking(ApplicationDbContext dbContext)
        {
            bool isRunning = true;
            
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("1. Checka in i ett av hotellets tillgängliga rum");
                Console.WriteLine("2. Checka in i ett rum du skapar");
                Console.WriteLine("3. Exit");
                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.KeyChar)
                {
                    case '1':
                        var bookingDetails = BookingInfo();
                        _roomController.ShowAllAvailableRoomsWithGuestNbr(bookingDetails.nbrOfGuests);
                        Console.WriteLine("");
                        Console.WriteLine("Ange rumsId på ovan rum som vill du checka in i: ");
                        int roomId = int.Parse(Console.ReadLine());
                        var selectedRoom = _roomService.GetHotelRoomById(roomId);
                        if (selectedRoom == null)
                        {
                            Console.WriteLine("Rummet finns inte, vänligen försök igen.");
                            break;
                        }
                        if (!_bookingService.IsRoomAvailable(roomId, bookingDetails.checkInDate, bookingDetails.totalStay))
                        {
                            Console.WriteLine("Rummet är redan bokat för de angivna datumen, vänligen välj ett annat rum.");
                            break;
                        }

                        ChooseAddCustomerOrConnectCustomer();
                        Console.WriteLine("Vilket kundId vill du koppla till bokningen?");
                        int customerId = int.Parse(Console.ReadLine());
                        var customer = _customerService.GetCustomerById(customerId);
                        if (customer == null)
                        {
                            Console.WriteLine("Kunden finns inte, försök igen");
                            break;
                        }
                        
                        var newBooking = new Bookings
                        {
                            CheckInDate = bookingDetails.checkInDate,
                            TotalStay = bookingDetails.totalStay,
                            NbrOfGuests = bookingDetails.nbrOfGuests,
                            HotelRooms = selectedRoom,
                            Customers = customer
                        };

                        _bookingService.AddBooking(newBooking);
                        selectedRoom.RoomAvailability = false;
                        Console.WriteLine("Rumsbokning är nu genomförd.");
                        Console.ReadKey();
                        break;

                    case '2':
                        _roomController.AddNewRoom();
                        var newbookingDetails = BookingInfo();
                        _roomController.ShowAllAvailableRoomsWithGuestNbr(newbookingDetails.nbrOfGuests);
                        Console.WriteLine("Vilket rumsId tilldelades rummet? ");
                        Console.WriteLine("OBS Om inte ditt nyskapade rum syns är det pga antalet gäster inte överrensstämde med antal tillåtna personer för den rumsstorleken");
                        int roomId2 = int.Parse(Console.ReadLine());
                        var newRoom = _roomService.GetHotelRoomById(roomId2);

                        if (!_bookingService.IsRoomAvailable(roomId2, newbookingDetails.checkInDate, newbookingDetails.totalStay))
                        {
                            Console.WriteLine("Rummet är redan bokat för de angivna datumen, vänligen välj ett annat rum.");
                            break;
                        }

                        ChooseAddCustomerOrConnectCustomer();
                        Console.WriteLine("Vilket kundId vill du koppla till bokningen?");
                        int customerId2 = int.Parse(Console.ReadLine());
                        var customer2 = _customerService.GetCustomerById(customerId2);
                        if (customer2 == null)
                        {
                            Console.WriteLine("Kunden finns inte, försök igen");
                            break;
                        }
                        var newBooking2 = new Bookings
                        {
                            CheckInDate = newbookingDetails.checkInDate,
                            TotalStay = newbookingDetails.totalStay,
                            NbrOfGuests = newbookingDetails.nbrOfGuests,
                            HotelRooms = newRoom,
                            Customers = customer2
                        };
                        _bookingService.AddBooking(newBooking2);
                        newRoom.RoomAvailability = false;
                        Console.WriteLine("Rumsbokning är nu genomförd.");
                        Console.ReadKey();
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
                bool IsDateValid = DateTime.TryParseExact(Console.ReadLine(), "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out checkInDate);
                if(!IsDateValid)
                {
                    Console.WriteLine("Felaktigt datum, vänligen försök igen");
                }
                else if (checkInDate < DateTime.Today)
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

        public void ChooseAddCustomerOrConnectCustomer()
        {
            Console.WriteLine("Vill du koppla till en befintlig kund [1] eller skapa en ny kund [2]");
            ConsoleKeyInfo key2 = Console.ReadKey();
            switch (key2.KeyChar)
            {
                case '1':
                    _customerController.ShowAllCustomers();
                    break;
                case '2':
                    _customerController.AddCustomer();
                    _customerController.ShowAllCustomers();
                    break;
                default:
                    Console.WriteLine("Vänligen välj ett av alternativen 1 eller 2");
                    break;
            }
        }

        public void ShowAllBookings()
        {
            Console.Clear();
            var bookings = _bookingService.ShowAllTheBookings();
            if (bookings == null)
            {
                Console.WriteLine("Det finns inga bokningar att visa.");
                return;
            }
            foreach (var booking in bookings)
            {
                
                Console.WriteLine("HotellrumsId: " + booking.HotelRooms.HotelRoomsId + "\t är kopplat till KundId: " + booking.Customers.CustomersId);
            }
        }
    }
}
