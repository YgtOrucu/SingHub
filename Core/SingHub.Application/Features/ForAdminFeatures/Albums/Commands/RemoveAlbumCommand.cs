using MediatR;
using SingHub.Application.Bases;

namespace SingHub.Application.Features.ForAdminFeatures.Albums.Commands;

public record RemoveAlbumCommand(int Id) : IRequest<BaseResult<object>>;