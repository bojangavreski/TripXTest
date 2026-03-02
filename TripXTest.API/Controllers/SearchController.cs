using Microsoft.AspNetCore.Mvc;
using TripXTest.Application.Contracts.Search;
using TripXTest.Application.Requests.Search;
using TripXTest.Application.Responses.Search;
using TripXTest.Core.Entities;

namespace TripXTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SearchController : ControllerBase
    {
        private readonly ISearchEngineService _searchEngineService;

        public SearchController(ISearchEngineService searchEngineService)
        {
            _searchEngineService = searchEngineService;
        }

        [HttpPost]
        public async Task<ActionResult<List<Option>>> Search([FromBody]SearchRequest searchRequest)
        {
            return Ok(await _searchEngineService.SearchAsync(searchRequest));   
        }

        [HttpGet]
        public async Task<ActionResult<TravelSearchResponse>> GetOptionByCode([FromQuery]string optionCode)
        {
            return Ok(_searchEngineService.GetOptionByCode(optionCode));
        }
    }
}
