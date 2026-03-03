
using Microsoft.AspNetCore.Authorization;
using TripXTest.API.Middlewares;
using TripXTest.Application.Contracts;
using TripXTest.Application.Factories;
using TripXTest.Application.Services;
using TripXTest.Application.Services.OfferPipeline;
using TripXTest.Application.Services.Offers;
using TripXTest.Application.Services.Search;
using TripXTest.Core.Results;
using TripXTest.Infrastructure;
using TripXTest.Infrastructure.Clients.Flight;
using TripXTest.Infrastructure.Contracts.External;
using TripXTest.Infrastructure.Providers;

namespace TripXTest.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddMemoryCache();

            builder.Services.AddSingleton<CompletitionBackgroundService>();
            builder.Services.AddHostedService(sp => sp.GetRequiredService<CompletitionBackgroundService>());

            builder.Services.AddScoped(typeof(ITripXContext<>), typeof(TripXContext<>));

            builder.Services.AddScoped<ISearchEngineService, SearchEngineService>();
            builder.Services.AddScoped<IBookingService, BookingService>();

            builder.Services.AddScoped<ISearchProvider<TravelSearchResult>, FlightProvider>();
            builder.Services.AddScoped<ISearchProvider<TravelSearchResult>, HotelProvider>();

            builder.Services.AddScoped<IFlightClient, FlightClient>();
            builder.Services.AddScoped<IHotelClient, HotelClient>();

            builder.Services.AddScoped<IOfferPipeline, OfferPipeline>();

            builder.Services.AddScoped<IConcreteOffer, LastMinuteHotelOffer>();
            builder.Services.AddScoped<IConcreteOffer, HotelOnlyOffer>();

            builder.Services.AddScoped<IOptionFactory, OptionFactory>();

            builder.Services.AddHttpContextAccessor();
            builder.Services.AddSingleton<IAuthorizationHandler, HeaderAuthHandler>();

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("MockHeaderAuthorization", policy =>
                    policy.AddRequirements(new Header{ Name = "HeaderAuth", Value = "TickXSecret" }));
            });

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
