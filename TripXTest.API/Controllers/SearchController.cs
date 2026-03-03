using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TripXTest.Application.Contracts;
using TripXTest.Application.Requests;
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
        public ActionResult<Option> GetOptionByCode([FromQuery]
                                                    [RegularExpression("^[A-Z]{3}$", ErrorMessage = "{0} must by in IATA airport code format")]
                                                    [Required(ErrorMessage = "{0} is required")] string optionCode)
        {
            return Ok(_searchEngineService.GetOptionByCode(optionCode));
        }
    }
}
