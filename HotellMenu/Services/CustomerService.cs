using HotellMenu.Contexts;
using HotellMenu.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotellMenu.Services
{
    public class CustomerService
    {
        ApplicationDbContext _dbContext;

        public CustomerService(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void AddCustomer(Customers customer)
        {
            _dbContext.Customers.Add(customer);
            _dbContext.SaveChanges();
        }

        public void EditCustomer(Customers customer)
        {
            _dbContext.Customers.Update(customer);
            _dbContext.SaveChanges();
        }


        public void DeleteCustomer(int customerId)
        {
            var customer = _dbContext.Customers.FirstOrDefault(c => c.CustomersId == customerId);
            if (customer != null)
            {
                _dbContext.Customers.Remove(customer);
                _dbContext.SaveChanges();
            }
        }

        public List<Customers> ShowAllCustomers()
        {
            return _dbContext.Customers.ToList();
        }


        public Customers GetCustomerById(int customerId)
        {
            return _dbContext.Customers.FirstOrDefault(r => r.CustomersId == customerId);
        }

    }
}
