using AutoMapper;
using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Albums.Commands;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForAdminFeatures.Albums.Handlers.WriteProcess;

public class CreateAlbumCommandHandle(IGenericRepository<Album> repository, IMapper mapper, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateAlbumCommand, BaseResult<object>>
{
    public async Task<BaseResult<object>> Handle(CreateAlbumCommand request, CancellationToken cancellationToken)
    {
        var value = mapper.Map<Album>(request);
        await repository.CreateAsync(value);
        var result = await unitOfWork.SaveChangesAsync();

        return BaseResult<object>.Success("The value has been successfully created.", result); ;
    }
}
