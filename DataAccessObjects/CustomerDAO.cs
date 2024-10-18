using System;
using System.Collections.Generic;
using System.Linq;
using BusinessObjects;

namespace DataAccessLayer
{
    public class CustomerDAO 
    {
        private static List<Customer> customers = new List<Customer>
        {
            new Customer { CustomerID = 1, CustomerFullName = "John Doe", Telephone = "1234567890", EmailAddress = "john.doe@example.com", CustomerBirthday = new DateTime(1990, 1, 1), CustomerStatus = 1, Password = "password123" },
            new Customer { CustomerID = 2, CustomerFullName = "Jane Smith", Telephone = "0987654321", EmailAddress = "jane.smith@example.com", CustomerBirthday = new DateTime(1985, 5, 12), CustomerStatus = 1, Password = "password456" },
        };

        private static HashSet<int> usedIds = new HashSet<int> { 1, 2 };

        public List<Customer> GetAllCustomers() => customers.OrderBy(c => c.CustomerID).ToList();

        public Customer GetCustomerById(int customerId) => customers.FirstOrDefault(c => c.CustomerID == customerId);

        public void AddCustomer(Customer customer)
        {
            int newId = 1;
            while (usedIds.Contains(newId))
            {
                newId++;
            }
            customer.CustomerID = newId;
            usedIds.Add(newId);
            customers.Add(customer);
        }

        public void UpdateCustomer(Customer customer)
        {
            var existingCustomer = GetCustomerById(customer.CustomerID);
            if (existingCustomer != null)
            {
                existingCustomer.CustomerFullName = customer.CustomerFullName;
                existingCustomer.Telephone = customer.Telephone;
                existingCustomer.EmailAddress = customer.EmailAddress;
                existingCustomer.CustomerBirthday = customer.CustomerBirthday;
                existingCustomer.CustomerStatus = customer.CustomerStatus;
                existingCustomer.Password = customer.Password;
            }
        }

        public void DeleteCustomer(int customerId)
        {
            var customer = GetCustomerById(customerId);
            if (customer != null)
            {
                customers.Remove(customer);
                usedIds.Remove(customerId);
            }
        }
    }
}
