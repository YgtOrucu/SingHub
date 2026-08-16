using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SingHub.Application.Features.ForAdminFeatures.Genres.Commands;

namespace SingHub.Application.Features.ForAdminFeatures.Genres.Endpoint;

public static class GenreEndpoint
{
    public static void RegisterGenresEndpoint(this IEndpointRouteBuilder app) 
    {
        var Genre = app.MapGroup("/genre").WithTags("Genre");

        Genre.MapPost(string.Empty, CreateGenresAsync);
        Genre.MapPut(string.Empty, UpdateGenresAsync);
        //Genre.MapGet(string.Empty, GetGenresAsync);
        //Genre.MapGet("{id}", GetGenresByIdAsync);
        Genre.MapDelete("{id}", RemoveGenresAsync);
    }

    private static async Task<IResult> CreateGenresAsync(IMediator mediator, CreateGenreCommand command)
    {
        var response = await mediator.Send(command);
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }

    private static async Task<IResult> UpdateGenresAsync(IMediator mediator, UpdateGenreCommand command)
    {
        var response = await mediator.Send(command);
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }

    //private static async Task<IResult> GetGenresAsync(IMediator mediator)
    //{
    //    var response = await mediator.Send(new GetGenresQuery());
    //    return response != null ? Results.Ok(response) : Results.BadRequest(response);
    //}

    //private static async Task<IResult> GetGenresByIdAsync(int id, IMediator mediator)
    //{
    //    var response = await mediator.Send(new GetGenresByIdQuery(id));
    //    return response != null ? Results.Ok(response) : Results.BadRequest(response);
    //}

    private static async Task<IResult> RemoveGenresAsync(int id, IMediator mediator)
    {
        var response = await mediator.Send(new RemoveGenreCommand(id));
        return Results.Ok(response);
    }
}
