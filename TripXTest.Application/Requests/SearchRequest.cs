using System.ComponentModel.DataAnnotations;

namespace TripXTest.Application.Requests
{
    public record SearchRequest
    {
        [RegularExpression("^[A-Z]{3}$", ErrorMessage = "{0} must by in IATA airport code format")]
        [Required(ErrorMessage = "{0} is required")]
        public required string DestinationCode { init; get; }

        [RegularExpression("^[A-Z]{3}$", ErrorMessage = "{0} must by in IATA airport code format")]
        public string? DepartureAirportCode { init; get; }

        [Required(ErrorMessage = "{0} is required")]
        public required DateTime FromDate { init; get; }

        [Required(ErrorMessage = "{0} is required")]
        public required DateTime ToDate { init; get; }
    }
}
