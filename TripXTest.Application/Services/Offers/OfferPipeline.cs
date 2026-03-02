using TripXTest.Application.Contracts.OfferPipeline;
using TripXTest.Application.Contracts.Offers;
using TripXTest.Application.Requests.Search;
using TripXTest.Core.Results;

namespace TripXTest.Application.Services.OfferPipeline
{
    public class OfferPipeline : IOfferPipeline
    {
        private readonly IEnumerable<IConcreteOffer> _concreteOffers;

        public OfferPipeline(IEnumerable<IConcreteOffer> concreteOffers)
        {
            _concreteOffers = concreteOffers;
        }

        public IEnumerable<TravelSearchResult> Apply(IEnumerable<TravelSearchResult> travelSearchResults, SearchRequest request)
        {
            List<TravelSearchResult> results = [];

            foreach (var result in travelSearchResults)
            {
                TravelSearchResult currentResult = result;

                foreach(var offer in _concreteOffers)
                {
                    currentResult = offer.Apply(currentResult, request);
                }
                
                results.Add(currentResult);
            }

            return results;
        }
    }
}
