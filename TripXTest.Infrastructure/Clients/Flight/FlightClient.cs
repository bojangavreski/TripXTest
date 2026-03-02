using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Web;
using TripXTest.Application.Requests.Search;
using TripXTest.Infrastructure.Contracts.External;
using TripXTest.Infrastructure.Dtos;

namespace TripXTest.Infrastructure.Clients.Flight
{
    public class FlightClient : IFlightClient
    {
        public async Task<IEnumerable<ExternalFlightDto>> SearchFlightsAsync(SearchRequest searchRequest)
        {
            using var httpClient = new HttpClient();

            var builder = new UriBuilder("https://tripx-test-functions.azurewebsites.net/api/SearchFlights");

            var query = HttpUtility.ParseQueryString(builder.Query);

            query["departureAirport"] = searchRequest.FlightRequest.DepartureAirportCode;
            query["arrivalAirport"] = searchRequest.HotelRequest.DestinationCode;

            builder.Query = query.ToString();

            var response = await httpClient.GetAsync(builder.ToString());

            response.EnsureSuccessStatusCode();

            return (await response.Content.ReadFromJsonAsync<IEnumerable<ExternalFlightDto>>())!;
        }
    }
}
