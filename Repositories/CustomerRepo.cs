using BusinessObjects;
using DataAccessLayer;
using System.Collections.Generic;
using System.Linq;

namespace Repositories
{
    public class CustomerRepo : ICustomerRepo
    {
        private readonly CustomerDAO _customerDao = new();

        public List<Customer> GetAllCustomers() => _customerDao.GetAllCustomers();

        public Customer GetCustomerById(int customerId) => _customerDao.GetCustomerById(customerId);

        public void AddCustomer(Customer customer) => _customerDao.AddCustomer(customer);

        public void UpdateCustomer(Customer customer) => _customerDao.UpdateCustomer(customer);

        public void DeleteCustomer(int customerId) => _customerDao.DeleteCustomer(customerId);

        public Customer GetCustomerByUsernameAndPassword(string username, string password)
        {
            return _customerDao.GetAllCustomers()
                .FirstOrDefault(c => c.EmailAddress == username && c.Password == password);
        }
    }
}
