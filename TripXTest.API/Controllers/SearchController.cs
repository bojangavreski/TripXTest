using Microsoft.AspNetCore.Authorization;
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
                                                    [RegularExpression("^[0-9a-zA-Z]{6}$", ErrorMessage = "{0} must be 6 character alphanumeric code ")]
                                                    [Required(ErrorMessage = "{0} is required")] string optionCode)
        {
            return Ok(_searchEngineService.GetOptionByCode(optionCode));
        }

        /// <summary>
        ///  This is a mock ep for authorization without authentication provider
        ///  For this to work we need to register AuthenticationHandler mock or real one with the actual scope inside
        /// </summary>
        /// <returns></returns>
        [HttpGet("authTest")]
        [Authorize(Policy = "MockHeaderAuthorization")]
        public ActionResult<Option> TestAuthorization()
        {
            return Ok("This should fail");
        }
    }
}
