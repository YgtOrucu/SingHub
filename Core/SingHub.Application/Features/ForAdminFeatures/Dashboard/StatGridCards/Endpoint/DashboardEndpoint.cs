using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using SingHub.Application.Features.ForAdminFeatures.Dashboard.StatGridCards.Queries;

namespace SingHub.Application.Features.ForAdminFeatures.Dashboard.StatGridCards.Endpoint;

public static class DashboardEndpoint
{
    public static void RegisterDashboardsEndpoint(this IEndpointRouteBuilder app)
    {
        var dashboard = app.MapGroup("/dashboard").WithTags("Dashboard");   
        dashboard.MapGet("StatGridCard", GetStatGridCardsAsync).AllowAnonymous();
        //dashboard.MapGet("{id}", GetDashboardsByIdAsync);
    }

    private static async Task<IResult> GetStatGridCardsAsync(IMediator mediator)
    {
        var response = await mediator.Send(new GetStatGridCardQuery());
        return response != null ? Results.Ok(response) : Results.BadRequest(response);
    }

    //private static async Task<IResult> GetDashboardsByIdAsync(int id, IMediator mediator)
    //{
    //    var response = await mediator.Send(new GetDashboardByIdQuery(id));
    //    return response != null ? Results.Ok(response) : Results.BadRequest(response);
    //}
}
