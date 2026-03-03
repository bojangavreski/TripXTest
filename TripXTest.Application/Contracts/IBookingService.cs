using TripXTest.Application.Responses;

namespace TripXTest.Application.Contracts
{
    public interface IBookingService
    {
        public BookingResponse Book(string optionCode);

        public void CompleteBooking(string bookingCode);

        public string CheckStatus(string bookingCode);
    }
}
