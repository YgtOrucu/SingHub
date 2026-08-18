using MediatR;
using SingHub.Application.Bases;

namespace SingHub.Application.Features.ForAdminFeatures.Artists.Commands;

public record RemoveArtistCommand(int Id) : IRequest<BaseResult<object>>;
