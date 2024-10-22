using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepo _customerRepository;

        public CustomerService(ICustomerRepo customerRepo)
        {
            _customerRepository = customerRepo;
        }

        public CustomerService()
        {
            _customerRepository = new CustomerRepo();
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            return _customerRepository.GetAllCustomers();
        }

        public Customer GetCustomerById(int id) => _customerRepository.GetCustomerById(id);

        public void AddCustomer(Customer customer) => _customerRepository.AddCustomer(customer);

        public void UpdateCustomer(Customer customer) => _customerRepository.UpdateCustomer(customer);

        public void DeleteCustomer(int id) => _customerRepository.DeleteCustomer(id);

        public Customer GetCustomerByUsernameAndPassword(string username, string password)
        {
            return _customerRepository.GetAllCustomers()
                .FirstOrDefault(c => c.EmailAddress == username && c.Password == password);
        }
    }
}
