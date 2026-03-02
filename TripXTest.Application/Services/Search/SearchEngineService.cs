using TripXTest.Application.Contracts.Providers;
using TripXTest.Application.Contracts.Search;
using TripXTest.Application.Requests.Search;
using TripXTest.Application.Responses.Search;
using TripXTest.Core.Entities.Search;

namespace TripXTest.Application.Services.Search
{
    public class SearchEngineService : ISearchEngineService
    {
        private readonly IEnumerable<ISearchProvider<TravelSearchResult>> _searchProviders;

        public SearchEngineService(IEnumerable<ISearchProvider<TravelSearchResult>> searchProviders)
        {
            _searchProviders = searchProviders;
        }

        public async Task<TravelSearchResponse> SearchAsync(SearchRequest searchRequest)
        {
            var searchTasks = _searchProviders.Select(
                                provider => ExecuteProviderAsync(provider, searchRequest)).ToList();

            var providedResults = (await Task.WhenAll(searchTasks))
                                  .SelectMany(x => x)
                                  .ToList(); 

            return new TravelSearchResponse
            {
                SearchResultUid = Guid.NewGuid(),
                FromDate = searchRequest.FromDate,
                ToDate = searchRequest.ToDate,
                Flights = providedResults.OfType<FlightSearchResult>().ToList(),
                Hotels = providedResults.OfType<HotelSearchResult>().ToList()
            };
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
