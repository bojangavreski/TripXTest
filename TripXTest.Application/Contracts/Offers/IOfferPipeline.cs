using TripXTest.Application.Requests.Search;
using TripXTest.Core.Results;

namespace TripXTest.Application.Contracts.OfferPipeline
{
    public interface IOfferPipeline
    {
        IEnumerable<TravelSearchResult> Apply(IEnumerable<TravelSearchResult> travelSearchResults, SearchRequest request);
    }
}
