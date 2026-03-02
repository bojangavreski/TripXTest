using TripXTest.Core.Enums;

namespace TripXTest.Core.Entities.Search
{
    public abstract class TravelSearchResult
    {
        public abstract SearchResultType ResultType { get; }

        public IEnumerable<ResultOffer> ResultOffers { get; set; } = [];
    }
}
