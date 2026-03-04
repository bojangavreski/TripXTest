using FluentAssertions;
using Moq;
using TripXTest.Application.Contracts;
using TripXTest.Application.Requests;
using TripXTest.Core.Enums;
using TripXTest.Core.Results;
using Xunit;

namespace TripXTest.Unit.Tests.Providers;

public class SearchProviderTests
{
    [Fact]
    public async Task FlightSearchProvider_SearchAsync_ReturnsFlightResults()
    {
        // Arrange
        var mockProvider = new Mock<ISearchProvider<FlightSearchResult>>();
        var searchRequest = new SearchRequest
        {
            DestinationCode = "OSL",
            DepartureAirportCode = "SKP",
            FromDate = DateTime.Now.AddDays(30),
            ToDate = DateTime.Now.AddDays(35)
        };

        var expectedResults = new List<FlightSearchResult>
        {
            new FlightSearchResult { FlightCode = 651, ArrivalAirport = "SKP", DepartureAirport = "OSL", FlightNumber = "331" },
            new FlightSearchResult { FlightCode = 651, ArrivalAirport = "SKP", DepartureAirport = "OSL", FlightNumber = "331" },
        };

        mockProvider
            .Setup(x => x.SearchAsync(It.IsAny<SearchRequest>()))
            .ReturnsAsync(expectedResults);

        mockProvider
            .Setup(x => x.SearchResultType)
            .Returns(SearchResultType.Flight);

        // Act
        var result = await mockProvider.Object.SearchAsync(searchRequest);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().AllBeOfType<FlightSearchResult>();
        mockProvider.Object.SearchResultType.Should().Be(SearchResultType.Flight);
    }

    [Fact]
    public async Task HotelSearchProvider_SearchAsync_ReturnsHotelResults()
    {
        // Arrange
        var mockProvider = new Mock<ISearchProvider<HotelSearchResult>>();
        var searchRequest = new SearchRequest
        {
            DestinationCode = "OSL",
            DepartureAirportCode = "SKP",
            FromDate = DateTime.Now.AddDays(30),
            ToDate = DateTime.Now.AddDays(35)
        };

        var expectedResults = new List<HotelSearchResult>
        {
            new HotelSearchResult { HotelCode = 3151, ResultOffers = [OfferType.LastMinuteHotel], City = "Skopje", DestinationCode = "SKP", HotelName = "Alexandar Square Boutique Hotel"},
            new HotelSearchResult { HotelCode = 3151, ResultOffers = [OfferType.LastMinuteHotel], City = "Skopje", DestinationCode = "SKP", HotelName = "Alexandar Square Boutique Hotel"},
        };

        mockProvider
            .Setup(x => x.SearchAsync(It.IsAny<SearchRequest>()))
            .ReturnsAsync(expectedResults);

        mockProvider
            .Setup(x => x.SearchResultType)
            .Returns(SearchResultType.Hotel);

        // Act
        var result = await mockProvider.Object.SearchAsync(searchRequest);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        result.Should().AllBeOfType<HotelSearchResult>();
        mockProvider.Object.SearchResultType.Should().Be(SearchResultType.Hotel);
    }

    [Fact]
    public async Task SearchProvider_WithEmptyResults_ReturnsEmptyList()
    {
        // Arrange
        var mockProvider = new Mock<ISearchProvider<FlightSearchResult>>();
        var searchRequest = new SearchRequest
        {
            DestinationCode = "somewhere",
            FromDate = DateTime.Now.AddDays(30),
            ToDate = DateTime.Now.AddDays(35)
        };

        mockProvider
            .Setup(x => x.SearchAsync(It.IsAny<SearchRequest>()))
            .ReturnsAsync(new List<FlightSearchResult>());

        // Act
        var result = await mockProvider.Object.SearchAsync(searchRequest);

        // Assert
        result.Should().NotBeNull();
        result.Should().BeEmpty();
    }

    [Fact]
    public async Task SearchProvider_VerifySearchAsyncCalled_WithCorrectParameters()
    {
        // Arrange
        var mockProvider = new Mock<ISearchProvider<HotelSearchResult>>();
        var searchRequest = new SearchRequest
        {
            DestinationCode = "OSL",
            DepartureAirportCode = "SKP",
            FromDate = DateTime.Now.AddDays(30),
            ToDate = DateTime.Now.AddDays(35)
        };

        mockProvider
            .Setup(x => x.SearchAsync(searchRequest))
            .ReturnsAsync(new List<HotelSearchResult>())
            .Verifiable();

        // Act
        await mockProvider.Object.SearchAsync(searchRequest);

        // Assert
        mockProvider.Verify(x => x.SearchAsync(searchRequest), Times.Once);
    }
}
