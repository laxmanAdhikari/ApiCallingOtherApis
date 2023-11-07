using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderProcessingApi.Tests
{
    public class ApiTestServer : WebApplicationFactory<Program>
    {
        private const string API_BASE_URL = "http://localhost:8080";
        public const string DEFAULT_ENVIRONMENT = "Development";
        private readonly string _environment;

        public ApiTestServer(string environment= DEFAULT_ENVIRONMENT)
        {
            _environment = environment;
           WebApplicationFactoryClientOptions options = new WebApplicationFactoryClientOptions();
           options.BaseAddress = new Uri(API_BASE_URL);
        }

        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.UseEnvironment(_environment);
            return base.CreateHost(builder);
        }

        protected override void ConfigureClient(HttpClient client)
        {
            base.ConfigureClient(client);
        }

        public HttpClient CreateHttpClient()
        {
            var client = CreateClient();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
            return client;
        }
    }
}
