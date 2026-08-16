using SingHub.Application.Features.Auths.Endpoint;
using SingHub.Application.Features.ForAdminFeatures.Genres.Endpoint;
using SingHub.Application.Features.ForAdminFeatures.Users.Endpoint;

namespace SingHub.WebAPI.Registration
{
    public static class EndpointRegistration
    {
        public static void RegisterEndpoints(this IEndpointRouteBuilder app)
        {
            app.RegisterAuthsEndpoint();
            app.RegisterUsersEndpoint();
            app.RegisterGenresEndpoint();
        }
    }
}
