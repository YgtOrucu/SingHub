using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Features.ForAdminFeatures.Genres.Result;

namespace SingHub.Application.Features.ForAdminFeatures.Genres.Queries;

public record GetGenreByIdQuery(int Id) : IRequest<BaseResult<GetGenreByIdQueryResult>>;