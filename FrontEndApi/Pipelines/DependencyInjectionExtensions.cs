
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OpenApi.Models;
using OrderProcessing.Services;

namespace OrderProcessingApi.Pipelines
{
    public static class DependencyInjectionExtensions
    {

        public static IServiceCollection ServicesDependencyInjection(this IServiceCollection services) 
        {
           services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddAutoMapper(typeof(Program));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "OrderProcessingApi", Version = "v1" });
            });

            services.AddCors(options =>
            {

                options.AddPolicy("CorsPolicy",
                    builder => builder
                    .SetIsOriginAllowed((host) => true)
                    .AllowAnyMethod()
                    .AllowAnyHeader()
                    .AllowCredentials());
            });

            services.AddSingleton<IAuthService, AuthService>();
            services.AddAuthorization();
            services.AddControllers();

            services.AddHttpClient(Constants.ApplicationConstants.CUSTOMER_API, client =>
            {
                client.BaseAddress = new System.Uri(Constants.ApplicationConstants.CUSTOMER_API_URL);
            });

            services.AddHttpClient(Constants.ApplicationConstants.PRODUCT_API, client =>
            {
                client.BaseAddress = new System.Uri(Constants.ApplicationConstants.PRODUCT_API_URL);
            });

            return services;
        }
    }
}
