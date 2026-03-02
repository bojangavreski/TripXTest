
using TripXTest.Application.Requests.Search;
using TripXTest.Core.Enums;
using TripXTest.Core.Results;

namespace TripXTest.Application.Contracts.Providers
{
    public interface ISearchProvider<TResult> where TResult : TravelSearchResult
    {
        public SearchResultType SearchResultType { get; }

        public Task<IReadOnlyList<TResult>> SearchAsync(SearchRequest searchRequest);
    }
}
