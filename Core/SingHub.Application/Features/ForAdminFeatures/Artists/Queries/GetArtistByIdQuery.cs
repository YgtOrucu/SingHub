using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Features.ForAdminFeatures.Artists.Result;

namespace SingHub.Application.Features.ForAdminFeatures.Artists.Queries;

public record GetArtistByIdQuery(int Id) : IRequest<BaseResult<GetArtistByIdQueryResult>>;
