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
            var bookingController = new BookingController(new BookingService(_dbContext));

            Console.WriteLine("Välkommen till bokningsmenyn!");
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("1. Lägg till bokning");
                Console.WriteLine("2. Exit");
                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.KeyChar)
                {
                    case '1':
                        bookingController.AddBooking(_dbContext);
                        break;
                    case '2':
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
