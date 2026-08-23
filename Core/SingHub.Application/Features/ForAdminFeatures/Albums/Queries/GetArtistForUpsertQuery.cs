using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Features.ForAdminFeatures.Albums.Result;

namespace SingHub.Application.Features.ForAdminFeatures.Albums.Queries;
public class GetArtistForUpsertQuery:IRequest<BaseResult<List<GetArtistForUpsertQueryResult>>>
{
}
