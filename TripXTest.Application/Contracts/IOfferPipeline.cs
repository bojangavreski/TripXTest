using TripXTest.Application.Requests;
using TripXTest.Core.Results;

namespace TripXTest.Application.Contracts
{
    public interface IOfferPipeline
    {
        IEnumerable<TravelSearchResult> Apply(IEnumerable<TravelSearchResult> travelSearchResults, SearchRequest request);
    }
}
