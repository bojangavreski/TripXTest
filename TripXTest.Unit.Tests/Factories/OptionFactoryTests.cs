using FluentAssertions;
using TripXTest.Application.Factories;
using TripXTest.Core.Enums;
using TripXTest.Core.Results;
using Xunit;

namespace TripXTest.Unit.Tests.Factories;

public class OptionFactoryTests
{
    private readonly OptionFactory _sut;

    public OptionFactoryTests()
    {
        _sut = new OptionFactory();
    }

    [Fact]
    public void CreateOption_WithFlightsAndHotels_ReturnsOptions()
    {
        // Arrange
        var flights = new List<FlightSearchResult>
        {
            new FlightSearchResult { FlightCode = 651, ArrivalAirport = "SKP", DepartureAirport = "OSL", FlightNumber = "331" },
        };
        var hotels = new List<HotelSearchResult>
        {
            new HotelSearchResult { HotelCode = 3151, ResultOffers = [OfferType.LastMinuteHotel], City = "Skopje", DestinationCode = "SKP", HotelName = "Alexandar Square Boutique Hotel"},
        };

        // Act
        var result = _sut.CreateOption(flights, hotels);

        // Assert
        result.Should().NotBeEmpty();
        result.Should().HaveCount(1);
        result.First().HotelCode.Should().NotBeNullOrEmpty();
        result.First().FlightCode.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void CreateOption_WithNoFlights_ReturnsHotelOnlyOptions()
    {
        // Arrange
        var flights = new List<FlightSearchResult>();
        var hotels = new List<HotelSearchResult>
        {
            new HotelSearchResult { HotelCode = 3151, ResultOffers = [OfferType.LastMinuteHotel], City = "Skopje", DestinationCode = "SKP", HotelName = "Alexandar Square Boutique Hotel"},
        };

        // Act
        var result = _sut.CreateOption(flights, hotels);

        // Assert
        result.Should().NotBeEmpty();
        result.First().FlightCode.Should().BeEmpty();
        result.First().HotelCode.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public void CreateOption_WithMultipleFlightsAndHotels_ReturnsCartesianProduct()
    {
        // Arrange
        var flights = new List<FlightSearchResult>
        {
            new FlightSearchResult { FlightCode = 651, ArrivalAirport = "SKP", DepartureAirport = "OSL", FlightNumber = "331" },
            new FlightSearchResult { FlightCode = 338, ArrivalAirport = "MXP", DepartureAirport = "FHN", FlightNumber = "189" },
        };
        var hotels = new List<HotelSearchResult>
        {
            new HotelSearchResult { HotelCode = 3151, ResultOffers = [OfferType.LastMinuteHotel], City = "Skopje", DestinationCode = "SKP", HotelName = "Alexandar Square Boutique Hotel"},
            new HotelSearchResult { HotelCode = 8627, ResultOffers = [OfferType.LastMinuteHotel], City = "Skopje", DestinationCode = "SKP", HotelName = "Skopje Marriott Hotel"},
        };

        // Act
        var result = _sut.CreateOption(flights, hotels);

        // Assert
        result.Should().HaveCount(4);
    }
}
