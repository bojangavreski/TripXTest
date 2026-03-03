using System.ComponentModel.DataAnnotations;

namespace TripXTest.Application.Requests
{
    public record SearchRequest
    {
        public required string DestinationCode { init; get; }

        public string? DepartureAirportCode { init; get; }

        public DateTime FromDate { init; get; }

        public DateTime ToDate { init; get; }
    }
}
