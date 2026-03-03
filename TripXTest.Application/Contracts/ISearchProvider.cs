using TripXTest.Application.Requests;
using TripXTest.Core.Enums;
using TripXTest.Core.Results;

namespace TripXTest.Application.Contracts
{
    public interface ISearchProvider<TResult> where TResult : TravelSearchResult
    {
        public SearchResultType SearchResultType { get; }

        public Task<IReadOnlyList<TResult>> SearchAsync(SearchRequest searchRequest);
    }
}
