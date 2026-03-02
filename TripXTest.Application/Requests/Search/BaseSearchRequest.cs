namespace TripXTest.Application.Requests.Search
{
    public record BaseSearchRequest
    {
        public DateTime FromDate { init; get; }

        public DateTime ToDate { init; get; }

        //public RequestFilter Filter { init; get; }
    }
}
