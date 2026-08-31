using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SingHub.Application.Features.ForUsersFeatures.PermissionToSongPlay.Queries;


namespace SingHub.Application.Features.ForUsersFeatures.PermissionToPermissionToSongPlayPlay.Endpoint;

public static class PermissionToSongPlayEndpoint
{
    public static void RegisterPermissionToSongPlaysEndpoint(this IEndpointRouteBuilder app)
    {
        var permissionToSongPlay = app.MapGroup("/permissionToSongPlay").WithTags("PermissionToSongPlay");
        permissionToSongPlay.MapGet(string.Empty, GetPermissionToSongPlaysAsync);
    }

    private static async Task<IResult> GetPermissionToSongPlaysAsync(int SongId, string UserName, IMediator mediator)
    {
        var response = await mediator.Send(new CheckPlayAccessQuery(SongId, UserName));
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }
}