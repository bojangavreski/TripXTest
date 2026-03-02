using TripXTest.Application.Contracts.Providers;
using TripXTest.Application.Requests.Search;
using TripXTest.Core.Entities.Search;
using TripXTest.Core.Enums;
using TripXTest.Infrastructure.Contracts.External;

namespace TripXTest.Infrastructure.Providers
{
    public class FlightProvider : ISearchProvider<TravelSearchResult>
    {
        private readonly IFlightClient _flightClient;

        public SearchResultType SearchResultType => SearchResultType.Flight;

        public FlightProvider(IFlightClient flightClient)
        {
            _flightClient = flightClient;
        }

        public async Task<IReadOnlyList<TravelSearchResult>> SearchAsync(SearchRequest searchRequest)
        {
            if(searchRequest.FlightRequest == null || 
                searchRequest.FlightRequest.DepartureAirportCode == null)
                {
                    return await Task.FromResult<IReadOnlyList<TravelSearchResult>>([]);
                }

            var flightResults = await _flightClient.SearchFlightsAsync(searchRequest);

            return flightResults.Select(x =>
                new FlightSearchResult
                {
                    ArrivalAirport = x.ArrivalAirport,
                    DepartureAirport = x.DepartureAirport,
                    FlightCode = x.FlightCode,
                    FlightNumber = x.FlightNumber
                }).ToList();
        }
    }
}
