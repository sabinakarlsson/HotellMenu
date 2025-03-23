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
            Console.WriteLine("Välkommen till bokningsmenyn!");
            bool isRunning = true;
            while (isRunning)
            {

                var bookingController = new BookingController(new BookingService(_dbContext));


                Console.Clear();
                Console.WriteLine("1. Lägg till bokning");
                Console.WriteLine("2. Redigera bokning");
                Console.WriteLine("3. Visa bokning(ar)");
                Console.WriteLine("4. Radera bokning");
                Console.WriteLine("5. Exit");
                ConsoleKeyInfo key = Console.ReadKey();

                switch (key.KeyChar)
                {
                    case '1':
                        bookingController.AddBooking(_dbContext);
                        break;
                    case '2':
                        //
                        break;
                    case '3':
                        //
                        break;
                    case '4':
                        //
                        break;
                    case '5':
                        //
                        break;
                    default:
                        Console.WriteLine("Välj bland menyvalen 1-5");
                        break;

                }

            }

        }
    }
}
