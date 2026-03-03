using TripXTest.Core.Entities;
using TripXTest.Core.Results;

namespace TripXTest.Application.Factories
{
    public interface IOptionFactory
    {
        public IEnumerable<Option> CreateOption(IEnumerable<FlightSearchResult> flights,
                                                IEnumerable<HotelSearchResult> hotels);
    }
}
