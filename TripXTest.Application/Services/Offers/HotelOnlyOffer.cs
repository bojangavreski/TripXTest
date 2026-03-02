using TripXTest.Application.Contracts.Offers;
using TripXTest.Application.Requests.Search;
using TripXTest.Core.Entities;
using TripXTest.Core.Entities.Search;

namespace TripXTest.Application.Services.Offers
{
    public class HotelOnlyOffer : IConcreteOffer
    {
        public TravelSearchResult Apply(TravelSearchResult travelResult, SearchRequest searchRequest)
        {
            List<ResultOffer> offers = [.. travelResult.ResultOffers, new ResultOffer { Code = "HotelOnlyOffer" }];

            travelResult.ResultOffers = offers;
            return travelResult;
        }

        public bool CanApply(TravelSearchResult travelResult, SearchRequest searchRequest)
        {
            return searchRequest.FlightRequest == null ||
                   string.IsNullOrEmpty(searchRequest.FlightRequest.DepartureAirportCode);
        }
    }
}
