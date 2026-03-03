using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Web;
using TripXTest.Application.Requests;
using TripXTest.Infrastructure.Contracts.External;
using TripXTest.Infrastructure.Dtos;

namespace TripXTest.Infrastructure.Clients
{
    public class FlightClient : IFlightClient
    {
        private readonly IConfiguration _configuration;

        private readonly string _FLIGHTS_URL;
        private readonly string _DepartureAirportQueryParam;
        private readonly string _ArrivalAirportQueryParam;

        public FlightClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var externalClientsSection = _configuration.GetSection("ExternalApiClients");
            _FLIGHTS_URL = externalClientsSection["FlightsUrl"] ?? String.Empty;
            _DepartureAirportQueryParam = externalClientsSection["DepartureAirportQueryParam"] ?? String.Empty;
            _ArrivalAirportQueryParam = externalClientsSection["ArrivalAirportQueryParam"] ?? String.Empty;
        }

        public async Task<IEnumerable<ExternalFlightDto>> SearchFlightsAsync(SearchRequest searchRequest)
        {
            using var httpClient = new HttpClient();

            var builder = new UriBuilder(_FLIGHTS_URL);

            var query = HttpUtility.ParseQueryString(builder.Query);

            query[_DepartureAirportQueryParam] = searchRequest.DepartureAirportCode;
            query[_ArrivalAirportQueryParam] = searchRequest.DestinationCode;

            builder.Query = query.ToString();

            //This should be validated 
            var response = await httpClient.GetAsync(builder.ToString());

            return (await response.Content.ReadFromJsonAsync<IEnumerable<ExternalFlightDto>>())!;
        }
    }
}
