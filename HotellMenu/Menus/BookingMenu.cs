using HotellMenu.Contexts;
using HotellMenu.Controllers;
using HotellMenu.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotellMenu.Menus
{
    public class BookingMenu
    {
        ApplicationDbContext _dbContext;

        public BookingMenu(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        
        public void Start()
        {
            var roomService = new RoomService(_dbContext);
            var roomController = new RoomController(roomService);
            var customerService = new CustomerService(_dbContext);
            var customerController = new CustomerController(customerService);
            var bookingController = new BookingController(new BookingService(_dbContext), roomController, roomService, customerController, customerService);
            
            Console.WriteLine("Välkommen till bokningsmenyn!");
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("1. Lägg till bokning");
                Console.WriteLine("2. Visa bokningar");
                Console.WriteLine("3. Exit");
                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.KeyChar)
                {
                    case '1':
                        bookingController.AddBooking(_dbContext);
                        break;
                    case '2':
                        bookingController.ShowAllBookings();
                        Console.WriteLine("Klicka enter för att gå vidare");
                        Console.ReadKey();
                        break;
                    case '3':
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Välj bland menyvalen 1-2. Tryck på en tangent för att fortsätta");
                        Console.ReadKey();
                        break;

                }

            }

        }
    }
}
