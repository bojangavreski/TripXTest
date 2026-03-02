using TripXTest.Core.Enums;

namespace TripXTest.Core.Entities.Search
{
    public class HotelSearchResult : TravelSearchResult
    {
        public override SearchResultType ResultType => SearchResultType.Hotel;

        public int? Id { get; init; }

        public int HotelCode { get; init; }

        public required string HotelName { get; init; }

        public required string DestinationCode { get; init; }

        public required string City { get; init; }
    }
}
