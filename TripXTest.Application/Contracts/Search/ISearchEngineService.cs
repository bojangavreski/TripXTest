using System;
using System.Collections.Generic;
using System.Text;
using TripXTest.Application.Requests.Search;
using TripXTest.Application.Responses.Search;

namespace TripXTest.Application.Contracts.Search
{ 
    public interface ISearchEngineService
    {
        /// <summary>
        /// Searches for travel arrangements based on the provided search request.
        /// </summary>
        /// <param name="searchRequest"></param>
        /// <returns></returns>
        Task<TravelSearchResponse> SearchAsync(SearchRequest searchRequest);
    }
}
