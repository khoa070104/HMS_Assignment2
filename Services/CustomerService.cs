using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;
namespace Services
{
    public class CustomerService
    {
        private readonly ICustomerRepo customerRepository;
        public CustomerService(){}

        public CustomerService(ICustomerRepo repository)
        {
            customerRepository = repository;
        }

        public List<Customer> GetAllCustomers() => customerRepository.GetAllCustomers();

        public Customer GetCustomerById(int id) => customerRepository.GetCustomerById(id);

        public void AddCustomer(Customer customer) => customerRepository.AddCustomer(customer);

        public void UpdateCustomer(Customer customer) => customerRepository.UpdateCustomer(customer);

        public void DeleteCustomer(int id) => customerRepository.DeleteCustomer(id);
    }
}
