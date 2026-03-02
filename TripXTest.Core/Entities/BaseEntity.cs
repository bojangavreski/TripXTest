namespace TripXTest.Core.Entities
{
    public class BaseEntity
    {
        public Guid Uid { get; set; }

        public required string Code { get; set; }
    }
}
