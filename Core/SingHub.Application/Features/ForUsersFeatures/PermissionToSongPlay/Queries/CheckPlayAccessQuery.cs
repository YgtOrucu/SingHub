using MediatR;
using SingHub.Application.Bases;

namespace SingHub.Application.Features.ForUsersFeatures.PermissionToSongPlay.Queries;

public record CheckPlayAccessQuery(int SongId, string UserName) : IRequest<BaseResult<object>>;
