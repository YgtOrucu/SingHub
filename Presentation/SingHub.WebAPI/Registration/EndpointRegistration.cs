using SingHub.Application.Features.Auths.Endpoint;
using SingHub.Application.Features.ForAdminFeatures.Albums.Endpoint;
using SingHub.Application.Features.ForAdminFeatures.Artists.Endpoint;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.StatGridCards.Endpoint;
using SingHub.Application.Features.ForAdminFeatures.Genres.Endpoint;
using SingHub.Application.Features.ForAdminFeatures.Songs.Endpoint;
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
            app.RegisterArtistsEndpoint();
            app.RegisterAlbumsEndpoint();
            app.RegisterSongsEndpoint();
            app.RegisterDashboardsEndpoint();
        }
    }
}
