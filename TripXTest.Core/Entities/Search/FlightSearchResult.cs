using TripXTest.Core.Enums;

namespace TripXTest.Core.Entities.Search
{
    public class FlightSearchResult : TravelSearchResult
    {
        public override SearchResultType ResultType => SearchResultType.Flight;

        public int FlightCode { init; get; }

        public required string FlightNumber { init; get; }

        public required string DepartureAirport { init; get; }

        public required string ArrivalAirport { init; get; }
    }
}
