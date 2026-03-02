using Microsoft.AspNetCore.Mvc;
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
        public ActionResult Book([FromQuery] string optionCode)
        {
            return Ok(_bookingService.Book(optionCode));
        }

        [HttpGet("checkStatus")]
        public ActionResult CheckStatus([FromQuery] string bookingCode)
        {
            return Ok(_bookingService.CheckStatus(bookingCode));
        }
    }
}
