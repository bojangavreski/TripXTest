using TripXTest.Core.Enums;

namespace TripXTest.Core.Entities
{
    public class Option : BaseEntity
    {
        public required string HotelCode { get; set; }

        public string FlightCode { get; set; } = String.Empty;

        public string ArrivalAirport { get; set; } = String.Empty;

        public required double Price { get; set; }

        public IEnumerable<OfferType>? Offers { get; set; }
    }
}
