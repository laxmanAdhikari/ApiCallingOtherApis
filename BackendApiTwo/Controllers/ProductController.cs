using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace OrderProcessing.Product.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductRepository _productRepository;
        public ProductController(IProductRepository customerRepository)
        {
            _productRepository = customerRepository;
        }

        [HttpGet]
        [Route("api/v1/products")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<ActionResult<List<Product>>> GetAllProducts()
        {
            return await _productRepository.GetAllProducts();
        }

        [HttpGet]
        [Route("api/v1/product/{productId}")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public async Task<IActionResult> GetProductById(int productId)
        {
            await Task.Delay(5000).ConfigureAwait(false);
            var result = await _productRepository.GetById(productId);
            //throw new NotSupportedException(); 
            return Ok(result);
        }
    }
}
