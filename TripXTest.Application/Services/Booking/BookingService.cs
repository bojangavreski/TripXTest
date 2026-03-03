using TripXTest.Application.Contracts;
using TripXTest.Application.Factories;
using TripXTest.Application.Responses;
using TripXTest.Core.Entities;

namespace TripXTest.Application.Services
{
    public class BookingService : IBookingService
    {
        private readonly ITripXContext<Booking> _bookingContext;
        private readonly ITripXContext<Option> _optionsContext;
        private readonly CompletitionBackgroundService _completitionBackgroundService;

        public BookingService(ITripXContext<Booking> bookingContext,
                              ITripXContext<Option> optionsContext,
                              CompletitionBackgroundService completitionBackgroundService)
        {
            _bookingContext = bookingContext;
            _optionsContext = optionsContext;
            _completitionBackgroundService = completitionBackgroundService;
        }

        public BookingResponse Book(string optionCode)
        {
            var option = _optionsContext.Get(optionCode);

            var booking = new Booking
            {
                Code = RandomGenerator.GenerateCode(),
                SleepTime = RandomGenerator.GenerateSleepTime(),
                BookingTime = DateTime.Now,
                Offers = option.Offers?.Select(x => x.Code).ToArray(),
            };

            _bookingContext.Save(booking);

            _completitionBackgroundService.TriggerWithInterval(booking.SleepTime, booking.Code);

            return new BookingResponse
            {
                BookingCode = booking.Code,
                BookingTime = booking.BookingTime
            };
        }

        public string CheckStatus(string bookingCode)
        {
            var booking = _bookingContext.Get(bookingCode);

            return booking.Status;
        }

        public void CompleteBooking(string bookingCode)
        {
            var booking = _bookingContext.Get(bookingCode);

            if (booking.Offers != null && booking.Offers.Contains("LastMinuteHotel"))
            {
                booking.Status = "Failed";

            }
            else
            {
                booking.Status = "Completed";
            }

            _bookingContext.Save(booking);
        }
    }
}
