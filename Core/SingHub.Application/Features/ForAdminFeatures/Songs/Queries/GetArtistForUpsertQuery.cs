using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Features.ForAdminFeatures.Songs.Result;

namespace SingHub.Application.Features.ForAdminFeatures.Songs.Queries;
public class GetArtistForUpsertQuery : IRequest<BaseResult<List<GetArtistForUpsertQueryResult>>> { }