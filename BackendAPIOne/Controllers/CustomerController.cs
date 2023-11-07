using Microsoft.AspNetCore.Mvc;

namespace OrderProcessing.Customer.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerRepository _customerRepository;
        public CustomerController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        [HttpGet]
        [Route("api/v1/customers")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<List<Customer>>> GetAllCustomers()
        {
            return await _customerRepository.GetAllCustomers();
        }

        [HttpGet]
        [Route("api/v1/customer/{customerId}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<Customer> GetcustomerById(int customerId)
        {
            await Task.Delay(2000).ConfigureAwait(false);
            //throw new NotImplementedException();
            return await _customerRepository.GetCustomerById(customerId);
        }
    }
}
