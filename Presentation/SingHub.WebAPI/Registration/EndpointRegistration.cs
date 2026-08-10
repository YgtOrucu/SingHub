using SingHub.Application.Features.Users.Endpoint;

namespace SingHub.WebAPI.Registration
{
    public static class EndpointRegistration
    {
        public static void RegisterEndpoints(this IEndpointRouteBuilder app)
        {
            app.RegisterUsersEndpoint();
        }
    }
}
