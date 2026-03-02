using TripXTest.Application.Contracts.Offers;
using TripXTest.Application.Requests.Search;
using TripXTest.Core.Entities;
using TripXTest.Core.Entities.Search;
using TripXTest.Core.Enums;

namespace TripXTest.Application.Services.Offers
{
    public class LastMinuteHotelOffer : IConcreteOffer
    {
        public TravelSearchResult Apply(TravelSearchResult travelResult, SearchRequest searchRequest)
        {
            if (!CanApply(travelResult, searchRequest))
            {
                return travelResult;
            }

            List<ResultOffer> offers = [.. travelResult.ResultOffers, new ResultOffer { Code = "LastMinuteHotel" }];

            travelResult.ResultOffers = offers;
            return travelResult;
        }

        public bool CanApply(TravelSearchResult travelResult, SearchRequest searchRequest)
        {
            var daysUntilCheckIn = (searchRequest.FromDate - DateTime.Now).TotalDays;
            return travelResult.ResultType == SearchResultType.Hotel && daysUntilCheckIn <= 45;
        }
    }
}
