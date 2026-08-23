using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SingHub.Application.Features.ForAdminFeatures.Albums.Commands;
using SingHub.Application.Features.ForAdminFeatures.Albums.Queries;

namespace SingHub.Application.Features.ForAdminFeatures.Albums.Endpoint;

public static class AlbumEndpoint
{
    public static void RegisterAlbumsEndpoint(this IEndpointRouteBuilder app)
    {
        var album = app.MapGroup("/album").WithTags("Album");

        album.MapPost(string.Empty, CreateAlbumsAsync);
        album.MapPut(string.Empty, UpdateAlbumsAsync);
        album.MapGet(string.Empty, GetAlbumsAsync);
        album.MapGet("GetArtistForUpsert", GetArtistForUpsertAsync);
        album.MapGet("{id}", GetAlbumsByIdAsync);
        album.MapDelete("{id}", RemoveAlbumsAsync);
    }

    private static async Task<IResult> CreateAlbumsAsync(IMediator mediator, CreateAlbumCommand command)
    {
        var response = await mediator.Send(command);
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }

    private static async Task<IResult> UpdateAlbumsAsync(IMediator mediator, UpdateAlbumCommand command)
    {
        var response = await mediator.Send(command);
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }

    private static async Task<IResult> GetAlbumsAsync(IMediator mediator)
    {
        var response = await mediator.Send(new GetAlbumQuery());
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }

    private static async Task<IResult> GetArtistForUpsertAsync(IMediator mediator)
    {
        var response = await mediator.Send(new GetArtistForUpsertQuery());
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }

    private static async Task<IResult> GetAlbumsByIdAsync(int id, IMediator mediator)
    {
        var response = await mediator.Send(new GetAlbumByIdQuery(id));
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }

    private static async Task<IResult> RemoveAlbumsAsync(int id, IMediator mediator)
    {
        var response = await mediator.Send(new RemoveAlbumCommand(id));
        return Results.Ok(response);
    }
}