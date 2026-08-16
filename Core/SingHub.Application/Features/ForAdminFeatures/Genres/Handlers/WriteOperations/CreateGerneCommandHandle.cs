using AutoMapper;
using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Genres.Commands;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForAdminFeatures.Genres.Handlers.WriteOperations;

public class CreateGerneCommandHandle(IGenericRepository<Genre> repository, IMapper mapper, IUnitOfWork unitOfWork)
    : IRequestHandler<CreateGenreCommand, BaseResult<object>>
{
    public async Task<BaseResult<object>> Handle(CreateGenreCommand request, CancellationToken cancellationToken)
    {
        var value = mapper.Map<Genre>(request);
        await repository.CreateAsync(value);
        var result = await unitOfWork.SaveChangesAsync();
        return BaseResult<object>.Success(value, "The value has been successfully updated.", result);
    }
}
