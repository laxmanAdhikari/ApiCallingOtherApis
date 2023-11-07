using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OrderProcessing.Customer
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly List<Customer> customers = new List<Customer>();
        public CustomerRepository()
        {
            customers.Add(new Customer()
            {
                Id = 10000,
                FirstName = "John",
                LastName = "Smith",
                EmailAddress = "jsmith@company.com"
            });

            customers.Add(new Customer()
            {
                Id = 10001,
                FirstName = "Steve",
                LastName = "Smith",
                EmailAddress = "ssmith@company.com"
            });
        }
        public Task<List<Customer>> GetAllCustomers()
        {
            return Task.FromResult(customers);
        }

        public Task<Customer> GetCustomerById(int customerId)
        {
            var customer = customers.FirstOrDefault(c => c.Id == customerId);
            return Task.FromResult(customer);

        }
    }
}
