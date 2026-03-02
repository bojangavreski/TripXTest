using TripXTest.Application.Contracts.Providers;
using TripXTest.Application.Requests.Search;
using TripXTest.Core.Enums;
using TripXTest.Core.Results;
using TripXTest.Infrastructure.Contracts.External;

namespace TripXTest.Infrastructure.Providers
{
    public class HotelProvider : ISearchProvider<TravelSearchResult>
    {
        private readonly IHotelClient _hotelClient;

        public SearchResultType SearchResultType => SearchResultType.Hotel;

        public HotelProvider(IHotelClient hotelClient)
        {
            _hotelClient = hotelClient;
        }

        public async Task<IReadOnlyList<TravelSearchResult>> SearchAsync(SearchRequest searchRequest)
        {
            if (searchRequest.HotelRequest == null ||
                    string.IsNullOrEmpty(searchRequest.HotelRequest.DestinationCode))
                {
                    return await Task.FromResult<IReadOnlyList<TravelSearchResult>>(new List<TravelSearchResult>());
                }

            var hotelResults = await _hotelClient.SearchHotelAsync(searchRequest);

            return hotelResults.Select(h => new HotelSearchResult
            {
                Id = h.Id,
                DestinationCode = h.DestinationCode,
                HotelName = h.HotelName,
                HotelCode = h.HotelCode,
                City = h.City
            }).ToList();
        }
    }
}
