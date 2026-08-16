using MediatR;
using SingHub.Application.Bases;

namespace SingHub.Application.Features.ForAdminFeatures.Genres.Commands;
public record RemoveGenreCommand(int Id) : IRequest<BaseResult<object>>;
