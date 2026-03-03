using TripXTest.Application.Contracts;
using TripXTest.Application.Requests;
using TripXTest.Core.Entities;
using TripXTest.Core.Enums;
using TripXTest.Core.Results;

namespace TripXTest.Application.Services.Offers
{
    public class HotelOnlyOffer : IConcreteOffer
    {
        public TravelSearchResult Apply(TravelSearchResult travelResult, SearchRequest searchRequest)
        {
            if(!CanApply(travelResult, searchRequest))
            {
                return travelResult;
            }

            List<ResultOffer> offers = [.. travelResult.ResultOffers, new ResultOffer { Code = "HotelOnlyOffer" }];

            travelResult.ResultOffers = offers;
            return travelResult;
        }

        public bool CanApply(TravelSearchResult travelResult, SearchRequest searchRequest)
        {
            return travelResult.ResultType == SearchResultType.Hotel && 
                            string.IsNullOrEmpty(searchRequest.DepartureAirportCode);
        }
    }
}
