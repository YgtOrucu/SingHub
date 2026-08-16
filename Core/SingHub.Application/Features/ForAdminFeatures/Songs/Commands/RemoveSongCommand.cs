using MediatR;
using SingHub.Application.Bases;

namespace SingHub.Application.Features.ForAdminFeatures.Songs.Commands;

public record RemoveSongCommand(int Id) : IRequest<BaseResult<object>>;
