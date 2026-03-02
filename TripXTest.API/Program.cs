
using TripXTest.Application.Contracts.OfferPipeline;
using TripXTest.Application.Contracts.Offers;
using TripXTest.Application.Contracts.Providers;
using TripXTest.Application.Contracts.Search;
using TripXTest.Application.Services.OfferPipeline;
using TripXTest.Application.Services.Offers;
using TripXTest.Application.Services.Search;
using TripXTest.Core.Entities.Search;
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

            builder.Services.AddScoped<ISearchEngineService, SearchEngineService>();

            builder.Services.AddScoped<ISearchProvider<TravelSearchResult>, FlightProvider>();
            builder.Services.AddScoped<ISearchProvider<TravelSearchResult>, HotelProvider>();

            builder.Services.AddScoped<IFlightClient, FlightClient>();
            builder.Services.AddScoped<IHotelClient, HotelClient>();

            builder.Services.AddScoped<IOfferPipeline, OfferPipeline>();

            builder.Services.AddScoped<IConcreteOffer, LastMinuteHotelOffer>();
            builder.Services.AddScoped<IConcreteOffer, HotelOnlyOffer>();

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

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
