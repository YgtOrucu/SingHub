using MediatR;
using SingHub.Application.Bases;

namespace SingHub.Application.Features.ForAdminFeatures.Genres.Commands
{
    public class UpdateGenreCommand : IRequest<BaseResult<object>>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
    }
}
