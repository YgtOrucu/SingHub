using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.GenreStatisticalDistributions.Queries;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.GetIdentityVerificationStatus.Queries;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.GetRoleBasedUserDistribution.Queries;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.StatGridCards.Queries;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.Top5MostListenedToSongs.Queries;

namespace SingHub.Application.Features.ForAdminFeatures.Dashboard.Endpoint;

public static class DashboardEndpoint
{
    public static void RegisterDashboardsEndpoint(this IEndpointRouteBuilder app)
    {
        var dashboard = app.MapGroup("/dashboard").WithTags("Dashboard");   
        dashboard.MapGet("StatGridCard", GetStatGridCardsAsync);
        dashboard.MapGet("Top5MostListenedToSongs", GetTop5MostListenedToSongsAsync);
        dashboard.MapGet("GenreStatisticalDistribution", GenreStatisticalDistributionAsync);
        dashboard.MapGet("RoleBasedUserDistribution", RoleBasedUserDistributionAsync);
        dashboard.MapGet("IdentityVerificationStatus", IdentityVerificationStatusAsync);
    }

    private static async Task<IResult> IdentityVerificationStatusAsync(IMediator mediator)
    {
        var response = await mediator.Send(new GetIdentityVerificationStatusQuery());
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
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
