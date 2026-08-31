using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SingHub.Application.Features.ForAdminFeatures.Songs.Commands;
using SingHub.Application.Features.ForAdminFeatures.Songs.Queries;

namespace SingHub.Application.Features.ForAdminFeatures.Songs.Endpoint;

public static class SongEndpoint
{
    public static void RegisterSongsEndpoint(this IEndpointRouteBuilder app)
    {
        var song = app.MapGroup("/song").WithTags("Song");

        song.MapPost(string.Empty, CreateSongsAsync);
        song.MapPut(string.Empty, UpdateSongsAsync);
        song.MapGet(string.Empty, GetSongsAsync);
        song.MapGet("{id}", GetSongsByIdAsync);
        song.MapDelete("{id}", RemoveSongsAsync);
        song.MapGet("GetArtistForUpsert", GetArtistForUpsertAsync);
        song.MapGet("GetGenreForUpsert", GetGenreForUpsertAsync);
        song.MapGet("GetAlbumForUpsert", GetAlbumForUpsertAsync);
        song.MapGet("GetRoleForUpsert", GetRoleForUpsertAsync);
        
    }

    private static async Task<IResult> GetRoleForUpsertAsync(IMediator mediator)
    {
        var response = await mediator.Send(new GetRoleForUpsertQuery());
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }

    private static async Task<IResult> GetAlbumForUpsertAsync(IMediator mediator)
    {
        var response = await mediator.Send(new GetAlbumForUpsertQuery());
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }

    private static async Task<IResult> GetArtistForUpsertAsync(IMediator mediator)
    {
        var response = await mediator.Send(new GetArtistForUpsertQuery());
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }

    private static async Task<IResult> GetGenreForUpsertAsync(IMediator mediator)
    {
        var response = await mediator.Send(new GetGenreForUpsertQuery());
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }

    private static async Task<IResult> CreateSongsAsync(IMediator mediator, CreateSongCommand command)
    {
        var response = await mediator.Send(command);
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }

    private static async Task<IResult> UpdateSongsAsync(IMediator mediator, UpdateSongCommand command)
    {
        var response = await mediator.Send(command);
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }

    private static async Task<IResult> GetSongsAsync(IMediator mediator)
    {
        var response = await mediator.Send(new GetSongQuery());
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }

    private static async Task<IResult> GetSongsByIdAsync(IMediator mediator, int id)
    {
        var response = await mediator.Send(new GetSongByIdQuery(id));
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }

    private static async Task<IResult> RemoveSongsAsync(IMediator mediator, int id)
    {
        var response = await mediator.Send(new RemoveSongCommand(id));
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }
}