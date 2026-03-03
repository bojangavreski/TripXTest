namespace TripXTest.Application.Responses
{
    public record BookingResponse
    {
        public required string BookingCode { get; init; }

        public DateTime BookingTime { get; init; } = DateTime.Now;
    }
}
