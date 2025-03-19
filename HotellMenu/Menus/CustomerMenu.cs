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
    public class CustomerMenu
    {
        ApplicationDbContext _dbContext;

        public CustomerMenu(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }




        public void Start()
        {
            Console.WriteLine("Välkommen till kundmenyn!");
            bool isRunning = true;
            while (isRunning)
            {
                Console.Clear();
                Console.WriteLine("1. Lägg till kund");
                Console.WriteLine("2. Redigera kund");
                Console.WriteLine("3. Visa kund(er)");
                Console.WriteLine("4. Radera kund");
                Console.WriteLine("5. Exit");
                ConsoleKeyInfo key = Console.ReadKey();

                var customerController = new CustomerController(new CustomerService(_dbContext));

                switch (key.KeyChar)
                {
                    case '1':
                        customerController.AddCustomer();
                        break;
                    case '2':
                        customerController.EditCustomer();
                        break;
                    case '3':
                        customerController.ShowAllCustomers();
                        break;
                    case '4':
                        customerController.DeleteCustomer();
                        break;
                    case '5':
                        isRunning = false;
                        //Exit
                        break;
                    default:
                        Console.WriteLine("Välj bland menyvalen 1-5");
                        break;

                }

            }

        }
    }
}
