using Microsoft.Extensions.Configuration;
using System.Net.Http.Json;
using System.Web;
using TripXTest.Application.Requests;
using TripXTest.Infrastructure.Contracts.External;
using TripXTest.Infrastructure.Dtos;

namespace TripXTest.Infrastructure.Clients
{
    public class HotelClient : IHotelClient
    {
        private readonly IConfiguration _configuration;
        private string _HOTELS_URL;
        private readonly string _DESTINATION_CODE_PARAM;

        public HotelClient(IConfiguration configuration)
        {
            _configuration = configuration;
            var externalClientsSection = _configuration.GetSection("ExternalApiClients");
            _HOTELS_URL = externalClientsSection["HotelsUrl"] ?? String.Empty;
            _DESTINATION_CODE_PARAM = externalClientsSection["DestinationCode"] ?? String.Empty;
        }


        public async Task<IEnumerable<ExternalHotelDto>> SearchHotelAsync(SearchRequest searchRequest)
        {
            using var httpClient = new HttpClient();

            var builder = new UriBuilder(_HOTELS_URL);

            var query = HttpUtility.ParseQueryString(builder.Query);

            query[_DESTINATION_CODE_PARAM] = searchRequest.DestinationCode;

            builder.Query = query.ToString();

            var response = await httpClient.GetAsync(builder.ToString());

            return (await response.Content.ReadFromJsonAsync<IEnumerable<ExternalHotelDto>>())!;
        }
    }
}
