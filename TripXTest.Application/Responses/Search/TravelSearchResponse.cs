using TripXTest.Core.Results;

namespace TripXTest.Application.Responses.Search
{
    public class TravelSearchResponse
    {
        public DateTime FromDate { get; init; }
        
        public DateTime ToDate { get; init; }

        public IReadOnlyList<FlightSearchResult> Flights { get; init; } = [];

        public IReadOnlyList<HotelSearchResult> Hotels { get; init; } = [];
    }
}
