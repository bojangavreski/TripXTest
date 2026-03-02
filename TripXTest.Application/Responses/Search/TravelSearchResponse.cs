using TripXTest.Core.Entities.Search;

namespace TripXTest.Application.Responses.Search
{
    public record TravelSearchResponse
    {
        public Guid SearchResultUid { get; init; }

        public DateTime FromDate { get; init; }
        
        public DateTime ToDate { get; init; }

        public IReadOnlyList<FlightSearchResult> Flights { get; init; } = [];

        public IReadOnlyList<HotelSearchResult> Hotels { get; init; } = [];
    }
}
