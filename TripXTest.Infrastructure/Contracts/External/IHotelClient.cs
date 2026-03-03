using TripXTest.Application.Requests;
using TripXTest.Infrastructure.Dtos;

namespace TripXTest.Infrastructure.Contracts.External
{
    public interface IHotelClient
    {
        public Task<IEnumerable<ExternalHotelDto>> SearchHotelAsync(SearchRequest searchRequest);
    }
}
