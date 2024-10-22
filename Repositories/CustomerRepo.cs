using System.Collections.Generic;
using System.Linq;
using BusinessObjects;
using DataAccessObjects;
using Microsoft.EntityFrameworkCore;

namespace Repositories
{
    public class CustomerRepo : ICustomerRepo
    {
        private HmsDbContext _context;

        public CustomerRepo()
        {
            _context = new HmsDbContext();
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _context.Customers.ToList();
        }

        public Customer GetCustomerById(int customerId)
        {
            return _context.Customers.Find(customerId);
        }

        public void AddCustomer(Customer customer)
        {
            _context.Customers.Add(customer);
            _context.SaveChanges();
        }

        public void UpdateCustomer(Customer customer)
        {
            _context.Entry(customer).State = EntityState.Modified;
            _context.SaveChanges();
        }

        public void DeleteCustomer(int customerId)
        {
            var customer = _context.Customers.Find(customerId);
            if (customer != null)
            {
                _context.Customers.Remove(customer);
                _context.SaveChanges();
            }
        }

        public Customer GetCustomerByUsernameAndPassword(string username, string password)
        {
            return _context.Customers.FirstOrDefault(c => c.EmailAddress == username && c.Password == password);
        }
    }
}
