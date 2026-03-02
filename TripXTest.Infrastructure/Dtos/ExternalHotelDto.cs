namespace TripXTest.Infrastructure.Dtos
{
    public record ExternalHotelDto
    {
        public int Id { get; init; }

        public int HotelCode { get; init; }

        public required string HotelName { get; init; } 

        public required string DestinationCode { get; init; }

        public required string City { get; init; }
    }

    //      {
    //  "id": 8695,
    //  "hotelCode": 8695,
    //  "hotelName": "Aloft Palm Jumeirah Hotel",
    //  "destinationCode": "DXB",
    //  "city": "Dubai"
    //},
}
