using Microsoft.AspNetCore.Http;
using OrderProcessingApi.Exceptions;
using System;
using System.Text.Json;
using System.Threading.Tasks;

namespace OrderProcessingApi.Pipelines
{
    public class ErrorHandling
    {

        private readonly RequestDelegate _nextrequest;
        public ErrorHandling(RequestDelegate next)
        {

            _nextrequest = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _nextrequest(context);
            }
            catch (Exception error)
            {
                var response = context.Response;
                response.ContentType = "application/json";
                var responseModel = Response<string>.Fail(error.Message);

                switch (error)
                {
                    case UnauthorizedException:
                        response.StatusCode = StatusCodes.Status401Unauthorized;
                        break;
                    default:
                        response.StatusCode = StatusCodes.Status500InternalServerError;
                        break;
                }


                var result = JsonSerializer.Serialize(responseModel);
                await response.WriteAsync(result);
            }
        }
    }
}
