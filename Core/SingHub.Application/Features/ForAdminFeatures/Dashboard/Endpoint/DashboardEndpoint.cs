using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.GenreStatisticalDistributions.Queries;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.GetRoleBasedUserDistribution.Queries;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.StatGridCards.Queries;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.Top5MostListenedToSongs.Queries;

namespace SingHub.Application.Features.ForAdminFeatures.Dashboard.Endpoint;

public static class DashboardEndpoint
{
    public static void RegisterDashboardsEndpoint(this IEndpointRouteBuilder app)
    {
        var dashboard = app.MapGroup("/dashboard").WithTags("Dashboard");   
        dashboard.MapGet("StatGridCard", GetStatGridCardsAsync).AllowAnonymous();
        dashboard.MapGet("Top5MostListenedToSongs", GetTop5MostListenedToSongsAsync).AllowAnonymous();
        dashboard.MapGet("GenreStatisticalDistribution", GenreStatisticalDistributionAsync).AllowAnonymous();
        dashboard.MapGet("RoleBasedUserDistribution", RoleBasedUserDistributionAsync).AllowAnonymous();
    }

    private static async Task<IResult> RoleBasedUserDistributionAsync(IMediator mediator)
    {
        var response = await mediator.Send(new GetRoleBasedUserDistributionQuery());
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }

    private static async Task<IResult> GenreStatisticalDistributionAsync(IMediator mediator)
    {
        var response = await mediator.Send(new GetGenreStatisticalDistributionQuery());
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }

    private static async Task<IResult> GetTop5MostListenedToSongsAsync(IMediator mediator)
    {
        var response = await mediator.Send(new Top5MostListenedToSongsQuery());
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }

    private static async Task<IResult> GetStatGridCardsAsync(IMediator mediator)
    {
        var response = await mediator.Send(new GetStatGridCardQuery());
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }
}
