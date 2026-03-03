using TripXTest.Application.Contracts;
using TripXTest.Application.Requests;
using TripXTest.Core.Enums;
using TripXTest.Core.Results;
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
            if(string.IsNullOrEmpty(searchRequest.DepartureAirportCode))
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
