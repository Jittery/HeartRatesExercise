using HeartRates.Business.Extensions;
using Microsoft.OpenApi;
using Microsoft.OpenApi.Models;

namespace HeartRates.WebApi;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Services.AddControllers();


        builder.Services
            .AddEndpointsApiExplorer()
            .AddSwaggerGen(c =>
            {
                c.EnableAnnotations();
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Heart Rates API", Version = "v1" });
                
                // // Add this filter block to force the version string downward 
                // c.PreSerializeFilters.Add((openApiDoc, httpRequest) =>
                // {
                //     // Intercept the generated JSON and swap 3.0.4 out for a universally supported version
                //     openApiDoc.Version = new Version(3, 0, 1); 
                // });
            })
            .AddLocalServices()
            .AddDatabaseSupport();

        var app = builder.Build();
        
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Heart Rates API V1"); 
            });
        }

        app
            .UseHttpsRedirection()
            .UseAuthorization();

        app.MapControllers();

        app.Run();
    }
}