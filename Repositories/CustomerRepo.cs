using BusinessObjects;

namespace Repositories
{
    public class CustomerRepo : ICustomerRepo
    {
        private static List<Customer> customers = new List<Customer>();

        public List<Customer> GetAllCustomers() => customers;

        public Customer GetCustomerById(int customerId) =>
            customers.FirstOrDefault(c => c.CustomerID == customerId);

        public void AddCustomer(Customer customer) => customers.Add(customer);

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
            }
        }
    }
}
