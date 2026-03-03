using TripXTest.Core.Entities;
using TripXTest.Core.Results;

namespace TripXTest.Application.Factories
{
    public class OptionFactory : IOptionFactory
    {
        public IEnumerable<Option> CreateOption(IEnumerable<FlightSearchResult> flights,
                                                IEnumerable<HotelSearchResult> hotels)
        {
            return (from flight in flights.DefaultIfEmpty()
                    from hotel in hotels
                    where hotel != null
                    select new Option
                    {
                        Uid = Guid.NewGuid(),
                        Code = RandomGenerator.GenerateCode(), // Optimistic aproach, in real life we should check for duplicates
                        HotelCode = hotel.HotelCode.ToString(),
                        FlightCode = flight != null ? flight.FlightCode.ToString() : String.Empty,
                        ArrivalAirport = flight != null ?  flight.ArrivalAirport.ToString() : String.Empty,
                        Price = RandomGenerator.GeneratePrice(),
                        Offers = hotel.ResultOffers.Concat(
                                        flight != null ? flight.ResultOffers : []).ToList()
                    }).ToList();
            
            
        }
    }
}
