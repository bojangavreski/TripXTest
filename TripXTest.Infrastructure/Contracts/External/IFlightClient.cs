using TripXTest.Application.Requests.Search;
using TripXTest.Infrastructure.Dtos;

namespace TripXTest.Infrastructure.Contracts.External
{
    public interface IFlightClient
    {
        public Task<IEnumerable<ExternalFlightDto>> SearchFlightsAsync(SearchRequest searchRequest);
    }
}
