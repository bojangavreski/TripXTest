using TripXTest.Application.Responses;
using TripXTest.Core.Enums;

namespace TripXTest.Application.Contracts
{
    public interface IBookingService
    {
        public BookingResponse Book(string optionCode);

        public void CompleteBooking(string bookingCode);

        public BookingStatus CheckStatus(string bookingCode);
    }
}
