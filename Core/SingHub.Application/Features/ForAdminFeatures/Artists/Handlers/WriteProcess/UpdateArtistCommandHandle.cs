using AutoMapper;
using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Exceptions;
using SingHub.Application.Features.ForAdminFeatures.Artists.Commands;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForAdminFeatures.Artists.Handlers.WriteProcess;

public class UpdateArtistCommandHandle(IGenericRepository<Artist> repository, IMapper mapper, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateArtistCommand, BaseResult<object>>
{
    public async Task<BaseResult<object>> Handle(UpdateArtistCommand request, CancellationToken cancellationToken)
    {
        var value = await repository.GetByIdAsync(request.Id);

        if (value == null)
            throw new BadRequestException("Updated value could not be found");

        mapper.Map(request, value);
        repository.Update(value);

        var result = await unitOfWork.SaveChangesAsync();
        return BaseResult<object>.Success(value, "The value has been successfully updated.", result);
    }
}
