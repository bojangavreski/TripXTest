namespace TripXTest.Application.Requests.Search
{
    public record RequestFilter
    {
        public int Skip { init; get; } = 0;

        public int Take { init; get; } = 10;

        public int Page { init; get; } = 1;
    }
}