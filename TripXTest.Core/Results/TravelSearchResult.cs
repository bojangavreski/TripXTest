using TripXTest.Core.Entities;
using TripXTest.Core.Enums;

namespace TripXTest.Core.Results
{
    public abstract class TravelSearchResult
    {
        public abstract SearchResultType ResultType { get; }

        public IEnumerable<OfferType> ResultOffers { get; set; } = [];
    }
}
