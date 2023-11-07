using Microsoft.Extensions.Logging;
using OrderProcessing.Model;
using OrderProcessingApi.Exceptions;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace OrderProcessing.Services
{
    public class AuthService : IAuthService
    {

        ILogger<AuthService> _logger;
        private AuthToken authToken;
        public AuthService(ILogger<AuthService> logger)
        {

            _logger = logger;
            authToken = new AuthToken();
    }

        public async Task<string> GetToken()
        {
            authToken = await GetNewAcessToken();
            return authToken.AccessToken;

        }
        private async Task<AuthToken> GetNewAcessToken()
        {

            try
            {
                var client = new HttpClient();

                DotNetEnv.Env.TraversePath().Load();
                var postMessage = new Dictionary<string, string>();

                var clientId = Environment.GetEnvironmentVariable("CLIENT-ID");
                var clientSecret = Environment.GetEnvironmentVariable("CLIENT-SECRECT");
                var audience = Environment.GetEnvironmentVariable("AUDIENCE");
                var grantTtype = Environment.GetEnvironmentVariable("GRANT-TYPE");
                var tokenUurl = Environment.GetEnvironmentVariable("TOKEN-URL");

                postMessage.Add("client_id", clientId);
                postMessage.Add("client_secret", clientSecret);
                postMessage.Add("audience", audience);
                postMessage.Add("grant_type", grantTtype);

                var request = new HttpRequestMessage(HttpMethod.Post, tokenUurl)
                {
                    Content = new FormUrlEncodedContent(postMessage)
                };

                var response = await client.SendAsync(request);
                
                if(response.IsSuccessStatusCode)
                {
                    var json = await response.Content.ReadAsStringAsync();
                    authToken = System.Text.Json.JsonSerializer.Deserialize<AuthToken>(json);
                }

                return authToken;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Failed to obtain authorization token due to {ex}");
                throw new UnauthorizedException("Unable to retrieve access token from okta",ex);

            }
        }
    }
}
