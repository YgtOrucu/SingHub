using AutoMapper;
using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Exceptions;
using SingHub.Application.Features.ForAdminFeatures.Artists.Commands;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForAdminFeatures.Artists.Handlers.WriteProcess;

public class RemoveArtistCommandHandle(IGenericRepository<Artist> repository, IMapper mapper, IUnitOfWork unitOfWork)
    : IRequestHandler<RemoveArtistCommand, BaseResult<object>>
{
    public async Task<BaseResult<object>> Handle(RemoveArtistCommand request, CancellationToken cancellationToken)
    {
        var value = await repository.GetByIdAsync(request.Id);

        if (value == null)
            throw new BadRequestException("Updated value could not be found");

        repository.Delete(value);
        var result = await unitOfWork.SaveChangesAsync();

        return BaseResult<object>.Success(value, "The value has been successfully deleted.", result);
    }
}
