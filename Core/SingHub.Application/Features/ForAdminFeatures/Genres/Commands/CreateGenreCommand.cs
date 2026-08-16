using MediatR;
using SingHub.Application.Bases;

namespace SingHub.Application.Features.ForAdminFeatures.Genres.Commands;

public class CreateGenreCommand : IRequest<BaseResult<object>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
}
