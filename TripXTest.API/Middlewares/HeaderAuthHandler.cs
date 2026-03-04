using Microsoft.AspNetCore.Authorization;

namespace TripXTest.API.Middlewares
{
    public class HeaderAuthHandler : AuthorizationHandler<Header>
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HeaderAuthHandler(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, Header requirement)
        {
            var httpRequest = _httpContextAccessor.HttpContext?.Request;

            if (httpRequest != null && httpRequest.Headers.TryGetValue(requirement.Name, out var extractedValue))
            {
                if (extractedValue == requirement.Value)
                {
                    context.Succeed(requirement);
                }
            }

            return Task.CompletedTask;
        }
    }
}
