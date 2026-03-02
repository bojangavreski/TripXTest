using TripXTest.Application.Requests.Search;
using TripXTest.Core.Results;

namespace TripXTest.Application.Contracts.Offers
{
    public interface IConcreteOffer
    {

        public TravelSearchResult Apply(TravelSearchResult travelResult, SearchRequest searchRequest);

        public bool CanApply(TravelSearchResult travelResult, SearchRequest searchRequest);
    }
}
