
using Microsoft.AspNetCore.Authorization;
using TripXTest.API.Middlewares;
using TripXTest.Application.Contracts;
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
    public static class Register
    {
        public static IServiceCollection RegisterPersistence(this IServiceCollection services)
        {
           return services.AddMemoryCache()
                          .AddScoped(typeof(ITripXContext<>), typeof(TripXContext<>));
        }

        public static IServiceCollection RegisterBackgroundServices(this IServiceCollection services)
        {
            services.AddSingleton<CompletitionBackgroundService>();
            return services.AddHostedService(sp => sp.GetRequiredService<CompletitionBackgroundService>());
        }

        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            return services.AddScoped<ISearchEngineService, SearchEngineService>()
                           .AddScoped<IBookingService, BookingService>();
        }

        public static IServiceCollection RegisterProviders(this IServiceCollection services)
        {
            return services.AddScoped<ISearchProvider<TravelSearchResult>, FlightProvider>()
                           .AddScoped<ISearchProvider<TravelSearchResult>, HotelProvider>();
        }

        public static IServiceCollection RegisterApiClients(this IServiceCollection services)
        {
            return services.AddScoped<IFlightClient, FlightClient>()
                           .AddScoped<IHotelClient, HotelClient>();
        }

        public static IServiceCollection RegisteOfferPipeline(this IServiceCollection services)
        {
            return services.AddScoped<IOfferPipeline, OfferPipeline>()
                           .AddScoped<IConcreteOffer, LastMinuteHotelOffer>()
                           .AddScoped<IConcreteOffer, HotelOnlyOffer>();
        }

        public static IServiceCollection RegisterAuthorization(this IServiceCollection services)
        {
            return services.AddHttpContextAccessor()
                           .AddSingleton<IAuthorizationHandler, HeaderAuthHandler>()
                           .AddAuthorization(options =>
                            {
                                options.AddPolicy("MockHeaderAuthorization", policy =>
                                    policy.AddRequirements(new Header { Name = "HeaderAuth", Value = "TickXSecret" }));
                            });
        }
    }
}
