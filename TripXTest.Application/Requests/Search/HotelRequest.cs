using System.ComponentModel.DataAnnotations;

namespace TripXTest.Application.Requests.Search
{
    public class HotelRequest
    {
        [Required]
        public string DestinationCode { init; get; }
    }
}