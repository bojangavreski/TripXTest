
using TripXTest.Application.Requests.Search;
using TripXTest.Core.Entities.Search;
using TripXTest.Core.Enums;

namespace TripXTest.Application.Contracts.Providers
{
    public interface ISearchProvider<TResult> where TResult : TravelSearchResult
    {
        public SearchResultType SearchResultType { get; }

        public Task<IReadOnlyList<TResult>> SearchAsync(SearchRequest searchRequest);
    }
}
