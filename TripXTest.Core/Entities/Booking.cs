using TripXTest.Core.Enums;

namespace TripXTest.Core.Entities
{
    public class Booking : BaseEntity
    {
        public required int SleepTime { get; init; }

        public DateTime BookingTime { get; init; } = DateTime.Now;

        public BookingStatus Status { get; set; } = BookingStatus.Pending;

        public List<OfferType>? Offers { get; set; }
    }
}
