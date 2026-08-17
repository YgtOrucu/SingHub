using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Features.ForAdminFeatures.Genres.Result;

namespace SingHub.Application.Features.ForAdminFeatures.Genres.Queries;

public class GetGenreQuery : IRequest<BaseResult<List<GetGenreQueryResult>>>
{
}
