using TripXTest.API.Middlewares;
using TripXTest.Application.Factories;

namespace TripXTest.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.RegisterPersistence()
                            .RegisterBackgroundServices()
                            .RegisterApplicationServices()
                            .RegisterProviders()
                            .RegisterApiClients()
                            .RegisteOfferPipeline()
                            .RegisterAuthorization()
                            .AddScoped<IOptionFactory, OptionFactory>();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            app.UseMiddleware<GlobalExeptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
