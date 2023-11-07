using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Moq.Protected;
using OrderProcessingApi.Model;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace OrderProcessingApi.Tests
{
    [TestFixture]
    public class OrderProcessingApiTests
    {
        private ApiTestServer apiTestServer;
        private HttpClient client;

        [SetUp]
        public void Setup()
        {
            apiTestServer = new ApiTestServer(ApiTestServer.DEFAULT_ENVIRONMENT);
            client = apiTestServer.CreateClient();
        }

        [TearDown]
        public void Teardown()
        {
            apiTestServer.Dispose();
            client.Dispose();
        }

        [Test]
        public async Task GetAuthTokenTest()
        {
            await using var testServer = new ApiTestServer(ApiTestServer.DEFAULT_ENVIRONMENT);
            using var client = testServer.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = await client.GetAsync("Authorization/api/v1/authtoken");

            Assert.IsNotNull(response);
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
        }

        [Test]
        public async Task PlaceOrder_ReturnsBothCallSuccessful()
        {
            // Arrange
           // await using var testServer = new ApiTestServer(ApiTestServer.DEFAULT_ENVIRONMENT);
           // var client = testServer.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Mock the HttpClient for the customer and product APIs
            var customerApiMock = new Mock<HttpMessageHandler>();
            var productApiMock = new Mock<HttpMessageHandler>();

            // Replace the HttpClientFactory with mocked clients
            client = apiTestServer.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddHttpClient(OrderProcessingApi.Constants.ApplicationConstants.CUSTOMER_API)
                        .ConfigurePrimaryHttpMessageHandler(() => customerApiMock.Object);
                    services.AddHttpClient(OrderProcessingApi.Constants.ApplicationConstants.PRODUCT_API)
                        .ConfigurePrimaryHttpMessageHandler(() => productApiMock.Object);
                });
            }).CreateClient();

            // Mock the expected responses for the customer and product API calls
            customerApiMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });

            productApiMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });

            var orderRequest = new OrderRequest
            {
                CustomerId = 1,
                ProductId = 2,
            };

            // Act
            var response = await client.PostAsJsonAsync("Order/api/v1/placeorder", orderRequest);

            var result = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result, Is.EqualTo("\"Both API calls successful\""));

        }

        [Test]
        public async Task PlaceOrder_ReturnsOneCallSuccessful()
        {
            // Arrange
           // await using var testServer = new ApiTestServer(ApiTestServer.DEFAULT_ENVIRONMENT);
           // var client = testServer.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Mock the HttpClient for the customer and product APIs
            var customerApiMock = new Mock<HttpMessageHandler>();
            var productApiMock = new Mock<HttpMessageHandler>();

            // Replace the HttpClientFactory with mocked clients
            client = apiTestServer.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddHttpClient(OrderProcessingApi.Constants.ApplicationConstants.CUSTOMER_API)
                        .ConfigurePrimaryHttpMessageHandler(() => customerApiMock.Object);
                    services.AddHttpClient(OrderProcessingApi.Constants.ApplicationConstants.PRODUCT_API)
                        .ConfigurePrimaryHttpMessageHandler(() => productApiMock.Object);
                });
            }).CreateClient();

            // Mock the expected responses for the customer and product API calls
            customerApiMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.Unauthorized });

            productApiMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.OK });

            var orderRequest = new OrderRequest
            {
                CustomerId = 1,
                ProductId = 2,
            };

            // Act
            var response = await client.PostAsJsonAsync("Order/api/v1/placeorder", orderRequest);

            var result = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result, Is.EqualTo("\"One API call successful, one failed\""));
        }

        [Test]
        public async Task PlaceOrder_ReturnsBothCallUnsuccessful()
        {
            // Arrange
           // await using var testServer = new ApiTestServer(ApiTestServer.DEFAULT_ENVIRONMENT);
           // var client = testServer.CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Mock the HttpClient for the customer and product APIs
            var customerApiMock = new Mock<HttpMessageHandler>();
            var productApiMock = new Mock<HttpMessageHandler>();

            // Replace the HttpClientFactory with mocked clients
            client = apiTestServer.WithWebHostBuilder(builder =>
            {
                builder.ConfigureTestServices(services =>
                {
                    services.AddHttpClient(OrderProcessingApi.Constants.ApplicationConstants.CUSTOMER_API)
                        .ConfigurePrimaryHttpMessageHandler(() => customerApiMock.Object);
                    services.AddHttpClient(OrderProcessingApi.Constants.ApplicationConstants.PRODUCT_API)
                        .ConfigurePrimaryHttpMessageHandler(() => productApiMock.Object);
                });
            }).CreateClient();

            // Mock the expected responses for the customer and product API calls
            customerApiMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest });

            productApiMock.Protected()
                .Setup<Task<HttpResponseMessage>>("SendAsync", ItExpr.IsAny<HttpRequestMessage>(), ItExpr.IsAny<CancellationToken>())
                .ReturnsAsync(new HttpResponseMessage { StatusCode = HttpStatusCode.BadRequest });

            var orderRequest = new OrderRequest
            {
                CustomerId = 1,
                ProductId = 2,
            };

            // Act
            var response = await client.PostAsJsonAsync("Order/api/v1/placeorder", orderRequest);

            var result = await response.Content.ReadAsStringAsync();

            // Assert
            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.InternalServerError));
            Assert.That(result, Is.EqualTo("\"Both API calls failed\""));

        }

    }
}