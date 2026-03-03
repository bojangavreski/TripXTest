using TripXTest.Application.Contracts;
using TripXTest.Application.Factories;
using TripXTest.Application.Responses;
using TripXTest.Core.Entities;
using TripXTest.Core.Enums;

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

            if (option == null)
            {
                // Bad practice - in real application we would return a Result type or throw a custom exception
                throw new Exception($"Option with code {optionCode} not found.");
            }

            var offers = option.Offers?.ToList();

            var booking = new Booking
            {
                Code = RandomGenerator.GenerateCode(),
                SleepTime = RandomGenerator.GenerateSleepTime(),
                BookingTime = DateTime.Now,
                Offers = offers
            };

            _bookingContext.Save(booking);

            _completitionBackgroundService.TriggerWithInterval(booking.SleepTime, booking.Code);

            return new BookingResponse
            {
                BookingCode = booking.Code,
                BookingTime = booking.BookingTime
            };
        }

        public BookingStatus CheckStatus(string bookingCode)
        {

            Booking? booking = _bookingContext.Get(bookingCode);
            return booking == null ? throw new Exception($"Booking with code: {bookingCode} not found") : booking.Status;
        }

        public void CompleteBooking(string bookingCode)
        {
            var booking = _bookingContext.Get(bookingCode);

            if (booking.Offers != null && booking.Offers.Contains(OfferType.LastMinuteHotel))
            {
                booking.Status = BookingStatus.Failed;

            }
            else
            {
                booking.Status = BookingStatus.Complete;
            }

            _bookingContext.Save(booking);
        }
    }
}
