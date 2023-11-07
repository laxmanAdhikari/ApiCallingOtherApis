
using Microsoft.AspNetCore.Mvc;
using OrderProcessing.Services;
using OrderProcessingApi.Model;
using Polly;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FrontEndApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        private readonly IHttpClientFactory _httpClientFactory;
        private readonly IAuthService _authService;
        public OrderController(IHttpClientFactory httpClientFactory, IAuthService authService)
        {
            _httpClientFactory = httpClientFactory;
            _authService = authService;
        }

        [HttpPost]
        [Route("api/v1/placeorder")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(500)]
        public async Task<IActionResult> PlaceOrder([FromBody] OrderRequest orderRequest)
        {

            DotNetEnv.Env.TraversePath().Load();
            var postMessage = new Dictionary<string, string>();

            var byPassSecurity = Environment.GetEnvironmentVariable("BYPASS_SECURITY");

            if (byPassSecurity != null && !byPassSecurity.Equals("1"))
                {

                var token = await _authService.GetToken();
                if (token == null)
                    return Unauthorized("Please make sure you have access to API. Contact your administrator");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int customerId = orderRequest.CustomerId;
            int productId = orderRequest.ProductId;

            var policy = CreateRetryPolicy();

            var customerApi = _httpClientFactory.CreateClient(OrderProcessingApi.Constants.ApplicationConstants.CUSTOMER_API);
            var productApi = _httpClientFactory.CreateClient(OrderProcessingApi.Constants.ApplicationConstants.PRODUCT_API);

            var customerResponse = await policy.ExecuteAsync(async () =>
                await customerApi.GetAsync($"{OrderProcessingApi.Constants.ApplicationConstants.GET_CUSSTOMER_BY_ID_ENDPOINT}{customerId}")
            );

            var productResponse = await policy.ExecuteAsync(async () =>
                await productApi.GetAsync($"{OrderProcessingApi.Constants.ApplicationConstants.GET_PRODUCT_BY_ID_ENDPOINT}{productId}")
            );

            bool customerApiSuccess = customerResponse.IsSuccessStatusCode;
            bool productApiSuccess = productResponse.IsSuccessStatusCode;

            if (customerApiSuccess && productApiSuccess)
            {
                return Ok("Both API calls successful");
            }
            else if (customerApiSuccess || productApiSuccess)
            {
                return Ok("One API call successful, one failed");
            }
            else
            {
                return StatusCode(500, "Both API calls failed");
            }
        }

        private AsyncPolicy<HttpResponseMessage> CreateRetryPolicy()
        {
            return Policy.HandleResult<HttpResponseMessage>(r => !r.IsSuccessStatusCode)
                         .RetryAsync(3, (result, retryCount, context) =>
                         {
                             // Log the retry attempt
                             LogRetryAttempt(retryCount, context);

                             // You can also implement custom logic or handle retries here

                             // Delay before the next retry (if needed)
                             Task.Delay(TimeSpan.FromSeconds(2));
                         });
        }

        private void LogRetryAttempt(int retryCount, Context context)
        {
            // You can log the retry attempt, retry count, and additional context information
            Console.WriteLine($"Retry attempt {retryCount}, Context: {context}");
        }

    }
}

