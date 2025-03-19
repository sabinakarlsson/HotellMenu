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
    public class Menu
    {

        ApplicationDbContext _dbContext;

        public Menu(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void Start()
        {
            Console.WriteLine("Välkommen till hotellet!");

            bool isRunning = true;

            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("1. Hantera KUND");
                Console.WriteLine("2. Hantera RUM");
                Console.WriteLine("3. Hantera BOKNING");
                Console.WriteLine("4. Exit");

                ConsoleKeyInfo key = Console.ReadKey();
                

                switch (key.KeyChar)
                {
                    case '1':
                        Console.Clear();
                        new CustomerMenu(_dbContext).Start();
                        break;

                    case '2':
                        new RoomMenu(_dbContext).Start();
                        break;

                    case '3':
                        new BookingMenu(_dbContext).Start();
                        break;

                    case '4':
                        isRunning = false;
                        break;

                    default:
                        Console.WriteLine("Välj bland menyvalen 1 - 4");
                        break;
                }

            }
        }
    }

    
}
