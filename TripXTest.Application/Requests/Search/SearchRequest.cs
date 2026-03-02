using System;
using System.Collections.Generic;
using System.Text;

namespace TripXTest.Application.Requests.Search
{
    public record SearchRequest : BaseSearchRequest
    {
        public HotelRequest HotelRequest { init; get; }

        public FlightRequest FlightRequest { init; get; }
    }
}
