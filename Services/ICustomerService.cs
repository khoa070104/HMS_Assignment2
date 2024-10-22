using System.Collections.Generic;
using BusinessObjects;

namespace Services
{
    public interface ICustomerService
    {
        IEnumerable<Customer> GetAllCustomers();
        Customer GetCustomerById(int id);
        void AddCustomer(Customer customer);
        void UpdateCustomer(Customer customer);
        void DeleteCustomer(int id);
        Customer GetCustomerByUsernameAndPassword(string username, string password);
    }
}
