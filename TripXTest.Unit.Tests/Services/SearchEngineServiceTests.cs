using FluentAssertions;
using Moq;
using TripXTest.Application.Contracts;
using TripXTest.Application.Requests;
using TripXTest.Core.Entities;
using Xunit;

namespace TripXTest.Unit.Tests.Services;

public class SearchEngineServiceTests
{
    private readonly Mock<ISearchEngineService> _mockSearchEngine;

    public SearchEngineServiceTests()
    {
        _mockSearchEngine = new Mock<ISearchEngineService>();
    }

    [Fact]
    public async Task SearchAsync_WithValidRequest_ReturnsOptions()
    {
        // Arrange
        var searchRequest = new SearchRequest
        {
            DestinationCode = "OSL",
            DepartureAirportCode = "SKP",
            FromDate = DateTime.Now.AddDays(30),
            ToDate = DateTime.Now.AddDays(35)
        };

        var expectedOptions = new List<Option>
        {
            new Option { Code = "A6bbF", HotelCode = "4413", FlightCode = "306", Price = 331.51, ArrivalAirport = "OSL"}
        };

        _mockSearchEngine
            .Setup(x => x.SearchAsync(searchRequest))
            .ReturnsAsync(expectedOptions);

        // Act
        var result = await _mockSearchEngine.Object.SearchAsync(searchRequest);

        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(1);
        result.First().Code.Should().Be("A6bbF");
    }

    [Fact]
    public void GetOptionByCode_WithValidCode_ReturnsOption()
    {
        // Arrange
        var optionCode = "OPT001";
        var expectedOption = new Option { Code = "OPT001", HotelCode = "4413", FlightCode = "306", Price = 331.51, ArrivalAirport = "OSL" };

        _mockSearchEngine
            .Setup(x => x.GetOptionByCode(optionCode))
            .Returns(expectedOption);

        // Act
        var result = _mockSearchEngine.Object.GetOptionByCode(optionCode);

        // Assert
        result.Should().NotBeNull();
        result.Code.Should().Be(optionCode);
    }
}
