using TripXTest.Application.Requests;
using TripXTest.Core.Entities;

namespace TripXTest.Application.Contracts
{ 
    public interface ISearchEngineService
    {
        /// <summary>
        /// Searches for travel arrangements based on the provided search request.
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        Task<IEnumerable<Option>> SearchAsync(SearchRequest searchRequest);

        /// <summary>
        /// Retrieves search options that are previously searched
        /// </summary>
        /// <param name="searchResultUid"></param>
        /// <returns></returns>
        Option GetOptionByCode(string optionUid);
    }
}
