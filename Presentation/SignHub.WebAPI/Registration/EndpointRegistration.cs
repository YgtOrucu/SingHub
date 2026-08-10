using SignHub.Application.Features.Users.Endpoint;

namespace SignHub.WebAPI.Registration
{
    public static class EndpointRegistration
    {
        public static void RegisterEndpoints(this IEndpointRouteBuilder app)
        {
            app.RegisterUsersEndpoint();
        }
    }
}
