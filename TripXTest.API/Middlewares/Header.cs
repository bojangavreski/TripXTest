using Microsoft.AspNetCore.Authorization;

namespace TripXTest.API.Middlewares
{
    public class Header : IAuthorizationRequirement
    {
        public required string Name { get; set; }
        public required string Value { get; set; }
    }
}
