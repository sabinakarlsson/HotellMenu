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
            Console.WriteLine("Ange kundens namn:");
            string customerName = Console.ReadLine();
            Console.WriteLine("Ange kundens email:");
            string email = Console.ReadLine();
            _customerService.AddCustomer(new Entities.Customers
            {
                CustomerName = customerName,
                Email = email
            });
        }


        public void EditCustomer()
        {
            Console.WriteLine("Ange kundens id:");
            int customerId = int.Parse(Console.ReadLine());
            var customer = _customerService.ShowCustomerById(customerId);

            if (customer != null)
            {
                _customerService.ShowCustomerById(customerId);

                Console.WriteLine("Ange kundens nya namn:");
                customer.CustomerName = Console.ReadLine();
                Console.WriteLine("Ange kundens nya email:");
                customer.Email = Console.ReadLine();
                _customerService.EditCustomer(customer);
            }

            else
            {
                Console.WriteLine("Kunden finns inte");
            }

            Console.WriteLine("\n========================\n");
            ShowAllCustomers();

        }


        public void ShowAllCustomers()
        {
            var customers = _customerService.ShowAllCustomers();
            foreach (var customer in customers)
            {
                Console.WriteLine($"Kundens ID: {customer.CustomersId}, Kundens namn: {customer.CustomerName}, Email: {customer.Email}");
            }
            Console.ReadKey();
        }

        public void DeleteCustomer()
        {
            Console.WriteLine("Ange kundens id:");
            int customerId = int.Parse(Console.ReadLine());
            _customerService.DeleteCustomer(customerId);

            Console.WriteLine("\n========================\n");
            ShowAllCustomers();
        }
    }
}
