namespace TripXTest.Application.Requests.Search
{
    public record FlightRequest
    {
        public string DepartureAirportCode { init; get; }
    }
}