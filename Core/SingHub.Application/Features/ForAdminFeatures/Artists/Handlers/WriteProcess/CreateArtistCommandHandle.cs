using AutoMapper;
using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Artists.Commands;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForAdminFeatures.Artists.Handlers.WriteProcess;

public class CreateArtistCommandHandle(IGenericRepository<Artist> repository, IMapper mapper, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateArtistCommand, BaseResult<object>>
{
    public async Task<BaseResult<object>> Handle(CreateArtistCommand request, CancellationToken cancellationToken)
    {
        var value = mapper.Map<Artist>(request);
        await repository.CreateAsync(value);
        var result = await unitOfWork.SaveChangesAsync();

        return BaseResult<object>.Success(value, "The value has been successfully created.", result);
    }
}
