using FluentAssertions;
using Moq;
using TripXTest.Application.Contracts;
using TripXTest.Application.Responses;
using TripXTest.Core.Enums;
using Xunit;

namespace TripXTest.Unit.Tests.Services;

public class BookingServiceTests
{
    private readonly Mock<IBookingService> _mockBookingService;

    public BookingServiceTests()
    {
        _mockBookingService = new Mock<IBookingService>();
    }

    [Fact]
    public void Book_WithValidOptionCode_ReturnsBookingResponse()
    {
        // Arrange
        var optionCode = "OPT001";
        var expectedResponse = new BookingResponse
        {
            BookingCode = "BK12345",
            BookingTime = DateTime.Now,
        };

        _mockBookingService
            .Setup(x => x.Book(optionCode))
            .Returns(expectedResponse);

        // Act
        var result = _mockBookingService.Object.Book(optionCode);

        // Assert
        result.Should().NotBeNull();
        result.BookingCode.Should().Be("BK12345");
    }

    [Fact]
    public void CompleteBooking_WithValidBookingCode_CompletesSuccessfully()
    {
        // Arrange
        var bookingCode = "BK12345";

        _mockBookingService
            .Setup(x => x.CompleteBooking(bookingCode))
            .Verifiable();

        // Act
        _mockBookingService.Object.CompleteBooking(bookingCode);

        // Assert
        _mockBookingService.Verify(x => x.CompleteBooking(bookingCode), Times.Once);
    }

    [Fact]
    public void CheckStatus_WithValidBookingCode_ReturnsStatus()
    {
        // Arrange
        var bookingCode = "BK12345";
        var expectedStatus = BookingStatus.Complete.ToString();

        _mockBookingService
            .Setup(x => x.CheckStatus(bookingCode))
            .Returns(expectedStatus);

        // Act
        var result = _mockBookingService.Object.CheckStatus(bookingCode);

        // Assert
        result.Should().Be(expectedStatus);
    }
}
