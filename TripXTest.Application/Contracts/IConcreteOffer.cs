using TripXTest.Application.Requests;
using TripXTest.Core.Results;

namespace TripXTest.Application.Contracts
{
    public interface IConcreteOffer
    {

        public TravelSearchResult Apply(TravelSearchResult travelResult, SearchRequest searchRequest);

        public bool CanApply(TravelSearchResult travelResult, SearchRequest searchRequest);
    }
}
