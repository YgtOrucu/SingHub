using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Features.ForAdminFeatures.Songs.Result;

namespace SingHub.Application.Features.ForAdminFeatures.Songs.Queries;
public class GetAlbumForUpsertQuery : IRequest<BaseResult<List<GetAlbumForUpsertQueryResult>>> { }