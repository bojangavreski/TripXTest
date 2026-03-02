using System.Net.Http.Json;
using System.Web;
using TripXTest.Application.Requests.Search;
using TripXTest.Infrastructure.Contracts.External;
using TripXTest.Infrastructure.Dtos;

namespace TripXTest.Infrastructure.Clients.Flight
{
    public class HotelClient : IHotelClient
    {
        public async Task<IEnumerable<ExternalHotelDto>> SearchHotelAsync(SearchRequest searchRequest)
        {
            using var httpClient = new HttpClient();

            var builder = new UriBuilder("https://tripx-test-functions.azurewebsites.net/api/SearchHotels");

            var query = HttpUtility.ParseQueryString(builder.Query);

            query["destinationCode"] = searchRequest.HotelRequest.DestinationCode;

            builder.Query = query.ToString();

            var response = await httpClient.GetAsync(builder.ToString());

            response.EnsureSuccessStatusCode();

            return (await response.Content.ReadFromJsonAsync<IEnumerable<ExternalHotelDto>>())!;
        }
    }
}
