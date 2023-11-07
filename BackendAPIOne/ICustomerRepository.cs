using System.Collections.Generic;
using System.Threading.Tasks;

namespace OrderProcessing.Customer
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetAllCustomers();

        Task<Customer> GetCustomerById(int CustomerId);
    }
}
