using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TripXTest.Application.Contracts;

namespace TripXTest.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookingController : ControllerBase
    {
        private readonly IBookingService _bookingService;

        public BookingController(IBookingService bookingService)
        {
            _bookingService = bookingService;
        }   

        [HttpPost]
        public ActionResult Book([FromQuery]
                                 [RegularExpression("^[0-9a-zA-Z]{6}$", ErrorMessage = "{0} must be 6 character alphanumeric code ")]
                                 [Required(ErrorMessage = "{0} is required")] string optionCode)
        {
            return Ok(_bookingService.Book(optionCode));
        }

        [HttpGet("checkStatus")]
        public ActionResult CheckStatus([FromQuery]
                                        [RegularExpression("^[0-9a-zA-Z]{6}$", ErrorMessage = "{0} must be 6 character alphanumeric code ")]
                                        [Required(ErrorMessage = "{0} is required")] string bookingCode)
        {
            return Ok(_bookingService.CheckStatus(bookingCode));
        }
    }
}
