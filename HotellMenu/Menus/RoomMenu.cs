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
                Console.WriteLine("5. Återgå till huvudmeny");
                ConsoleKeyInfo key = Console.ReadKey();


                var roomController = new RoomController(new RoomService(_dbContext));

                switch (key.KeyChar)
                {
                    case '1':
                        roomController.AddNewRoom();
                        break;
                    case '2':
                        roomController.EditRoom();
                        break;
                    case '3':
                        roomController.ShowAllRooms();
                        break;
                    case '4':
                        roomController.DeleteRoom();
                        break;
                    case '5':
                        isRunning = false;
                        break;
                    default:
                        Console.WriteLine("Välj bland menyvalen 1-5");
                        break;

                }

            }

        }
    }
}
