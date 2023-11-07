
using DotNetEnv;
using Microsoft.AspNetCore.Builder;
using OrderProcessingApi.Pipelines;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ServicesDependencyInjection();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "OrderProcessing API v1"));
});

app.UseHttpsRedirection();

app.UseCors(OrderProcessingApi.Constants.ApplicationConstants.CORS_POLICY);

app.UseMiddleware<ErrorHandling>();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();

public partial class Program { }