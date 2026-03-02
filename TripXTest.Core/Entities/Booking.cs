namespace TripXTest.Core.Entities
{
    public class Booking : BaseEntity
    {
        public required int SleepTime { get; init; }

        public DateTime BookingTime { get; init; } = DateTime.Now;

        public string Status { get; set; } = "Pending";

        public string[]? Offers { get; set; }
    }
}
