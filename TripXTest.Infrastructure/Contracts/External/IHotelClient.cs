using TripXTest.Application.Requests.Search;
using TripXTest.Infrastructure.Dtos;

namespace TripXTest.Infrastructure.Contracts.External
{
    public interface IHotelClient
    {
        public Task<IEnumerable<ExternalHotelDto>> SearchHotelAsync(SearchRequest searchRequest);
    }
}
