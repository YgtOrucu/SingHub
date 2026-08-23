using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SingHub.Application.Features.ForAdminFeatures.Artists.Commands;
using SingHub.Application.Features.ForAdminFeatures.Artists.Queries;

namespace SingHub.Application.Features.ForAdminFeatures.Artists.Endpoint;

public static class ArtistEndpoint
{
    public static void RegisterArtistsEndpoint(this IEndpointRouteBuilder app)
    {
        var artist = app.MapGroup("/artist").WithTags("Artist");

        artist.MapPost(string.Empty, CreateArtistsAsync);
        artist.MapPut(string.Empty, UpdateArtistsAsync);
        artist.MapGet(string.Empty, GetArtistsAsync);
        artist.MapGet("{id}", GetArtistsByIdAsync);
        artist.MapDelete("{id}", RemoveArtistsAsync);
    }

    private static async Task<IResult> CreateArtistsAsync(IMediator mediator, CreateArtistCommand command)
    {
        var response = await mediator.Send(command);
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }

    private static async Task<IResult> UpdateArtistsAsync(IMediator mediator, UpdateArtistCommand command)
    {
        var response = await mediator.Send(command);
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }

    private static async Task<IResult> GetArtistsAsync(IMediator mediator)
    {
        var response = await mediator.Send(new GetArtistQuery());
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }

    private static async Task<IResult> GetArtistsByIdAsync(int id, IMediator mediator)
    {
        var response = await mediator.Send(new GetArtistByIdQuery(id));
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }

    private static async Task<IResult> RemoveArtistsAsync(int id, IMediator mediator)
    {
        var response = await mediator.Send(new RemoveArtistCommand(id));
        return Results.Ok(response);
    }
}