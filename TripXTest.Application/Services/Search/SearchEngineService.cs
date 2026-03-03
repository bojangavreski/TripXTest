using TripXTest.Application.Contracts;
using TripXTest.Application.Factories;
using TripXTest.Application.Requests;
using TripXTest.Core.Entities;
using TripXTest.Core.Results;

namespace TripXTest.Application.Services.Search
{
    public class SearchEngineService : ISearchEngineService
    {
        private readonly IEnumerable<ISearchProvider<TravelSearchResult>> _searchProviders;
        private readonly IOfferPipeline _offerPipeline;
        private readonly ITripXContext<Option> _tripXContext;
        private readonly IOptionFactory _optionFactory;

        public SearchEngineService(IEnumerable<ISearchProvider<TravelSearchResult>> searchProviders,
                                   IOfferPipeline offerPipeline,
                                   ITripXContext<Option> tripXContext,
                                   IOptionFactory optionFactory)
        {
            _searchProviders = searchProviders;
            _offerPipeline = offerPipeline;
            _tripXContext = tripXContext;
            _optionFactory = optionFactory;
        }

        public async Task<IEnumerable<Option>> SearchAsync(SearchRequest searchRequest)
        {
            var searchTasks = _searchProviders.Select(
                                    provider => ExecuteProviderAsync(provider, searchRequest)).ToList();

            var results = (await Task.WhenAll(searchTasks))
                                     .SelectMany(x => x)
                                     .ToList();

            var resultsDecoratedWithOffers = _offerPipeline.Apply(results, searchRequest);

            var options = _optionFactory.CreateOption(resultsDecoratedWithOffers.OfType<FlightSearchResult>(),
                                                      resultsDecoratedWithOffers.OfType<HotelSearchResult>());

            _tripXContext.SaveRange(options);

            return options;
        }

        public Option GetOptionByCode(string optionUid)
        {
            return _tripXContext.Get(optionUid);
        }

        private async Task<IReadOnlyList<TravelSearchResult>> ExecuteProviderAsync(
                                        ISearchProvider<TravelSearchResult> searchProvider,
                                        SearchRequest searchRequest)
        {
            var providerResult = await searchProvider.SearchAsync(searchRequest);
            return providerResult;
        }
    }
}
