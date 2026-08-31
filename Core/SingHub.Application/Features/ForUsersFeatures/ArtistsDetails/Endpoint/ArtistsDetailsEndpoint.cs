using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SingHub.Application.Features.ForUsersFeatures.ArtistsDetails.Queries;

namespace SingHub.Application.Features.ForUsersFeatures.ArtistsDetails.Endpoint;

public static class ArtistsDetailsEndpoint
{
    public static void RegisterArtistDetailsEndpoint(this IEndpointRouteBuilder app)
    {
        var artistsDetails = app.MapGroup("/artistsDetails").WithTags("ArtistsDetails");
        artistsDetails.MapGet(string.Empty, GetArtistDetailsAsync).AllowAnonymous();
    }

    private static async Task<IResult> GetArtistDetailsAsync(int Id, IMediator mediator)
    {
        var response = await mediator.Send(new ArtistDetailsAndSongQuery(Id));
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }
}