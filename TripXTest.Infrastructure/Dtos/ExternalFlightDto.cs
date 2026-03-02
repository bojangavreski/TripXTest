namespace TripXTest.Infrastructure.Dtos
{
    public record ExternalFlightDto
    {
        public int FlightCode { init; get; }

        public required string FlightNumber{ init; get; }

        public required string DepartureAirport { init; get; }

        public required string ArrivalAirport { init; get; }
    }
}
