using AutoMapper;
using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Exceptions;
using SingHub.Application.Features.ForAdminFeatures.Albums.Commands;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForAdminFeatures.Albums.Handlers.WriteProcess
{
    public class RemoveAlbumCommandHandle(IGenericRepository<Album> repository, IMapper mapper, IUnitOfWork unitOfWork)
    : IRequestHandler<RemoveAlbumCommand, BaseResult<object>>
    {
        public async Task<BaseResult<object>> Handle(RemoveAlbumCommand request, CancellationToken cancellationToken)
        {
            var value = await repository.GetByIdAsync(request.Id);

            if (value == null)
                throw new BadRequestException("Updated value could not be found");

            repository.Delete(value);
            var result = await unitOfWork.SaveChangesAsync();

            return BaseResult<object>.Success("The value has been successfully deleted.", result);
        }
    }
}
