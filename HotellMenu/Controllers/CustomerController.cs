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
    public class CustomerController
    {
        CustomerService _customerService;

        public CustomerController(CustomerService customerService)
        {
            _customerService = customerService;
        }

        public void AddCustomer()
        {
            Console.Clear();
            Console.WriteLine("Ange kundens namn:");
            string customerName = Console.ReadLine();
            Console.WriteLine("Ange kundens email:");
            string email = Console.ReadLine();
            _customerService.AddCustomer(new Entities.Customers
            {
                CustomerName = customerName,
                Email = email
            });

            Console.WriteLine("Kunden är nu tillagd. Klicka enter för att gå vidare");
            Console.ReadLine();
        }


        public void EditCustomer()
        {
            Console.Clear();
            int customerId;
            Customers customer = null;

            ShowAllCustomers(); 
            while (true)
            {
                Console.WriteLine("Ange Id på kunden du vill redigera:");
                if (int.TryParse(Console.ReadLine(), out customerId))
                {
                    customer = _customerService.ShowCustomerById(customerId);
                    if (customer != null)
                    {
                        Console.WriteLine("Du redigerar nu kund med kundId: " + customer.CustomersId);

                        Console.WriteLine("Ange kundens nya namn:");
                        customer.CustomerName = Console.ReadLine();
                        Console.WriteLine("Ange kundens nya email:");
                        customer.Email = Console.ReadLine();

                        _customerService.EditCustomer(customer);
                        Console.WriteLine("Kundens information är uppdaterad. Klicka enter för att gå vidare");
                        Console.ReadLine();
                        break;
                    }
                    else
                    {
                        Console.WriteLine("Kunden finns inte. Försök igen.");
                    }
                }
                else
                {
                    Console.WriteLine("Ange ett giltigt id");
                }
            }
        }


        public void ShowAllCustomers()
        {
            Console.Clear();
            var customers = _customerService.ShowAllCustomers();
            foreach (var customer in customers)
            {
                Console.WriteLine($"Kundens ID: {customer.CustomersId}, Kundens namn: {customer.CustomerName}, Email: {customer.Email}");
            }
        }

        public void DeleteCustomer()
        {
            Console.Clear();
            ShowAllCustomers();
            Console.WriteLine("\n========================");

            Console.Write("Ange id på kunden du vill radera:");

            int customerId = int.Parse(Console.ReadLine());



            _customerService.DeleteCustomer(customerId);
            Console.WriteLine("Kunden är nu raderad. Klicka enter för att gå vidare");
            Console.ReadLine();

        }

        
    }
}
