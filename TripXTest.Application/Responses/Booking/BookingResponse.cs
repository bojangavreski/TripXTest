namespace TripXTest.Application.Responses.Booking
{
    public record BookingResponse
    {
        public required string BookingCode { get; init; }

        public DateTime BookingTime { get; init; } = DateTime.Now;
    }
}
