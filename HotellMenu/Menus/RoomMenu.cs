using HotellMenu.Contexts;
using HotellMenu.Controllers;
using HotellMenu.Entities;
using HotellMenu.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotellMenu.Menus
{
    public class RoomMenu
    {
        ApplicationDbContext _dbContext;

        public RoomMenu(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public void Start()
        {
            Console.WriteLine("Välkommen till rumsmenyn!");
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("1. Lägg till rum");
                Console.WriteLine("2. Redigera rum");
                Console.WriteLine("3. Visa (alla) rum");
                Console.WriteLine("4. Radera rum");
                Console.WriteLine("5. Exit");
                ConsoleKeyInfo key = Console.ReadKey();


                var RoomController = new RoomController(new RoomService(_dbContext));

                switch (key.KeyChar)
                {
                    case '1':
                        //
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
