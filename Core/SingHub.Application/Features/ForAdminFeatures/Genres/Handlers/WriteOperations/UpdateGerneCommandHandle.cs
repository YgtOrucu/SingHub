using AutoMapper;
using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Exceptions;
using SingHub.Application.Features.ForAdminFeatures.Genres.Commands;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForAdminFeatures.Genres.Handlers.WriteOperations;

public class UpdateGerneCommandHandle(IGenericRepository<Genre> repository, IMapper mapper, IUnitOfWork unitOfWork)
    : IRequestHandler<UpdateGenreCommand, BaseResult<object>>
{
    public async Task<BaseResult<object>> Handle(UpdateGenreCommand request, CancellationToken cancellationToken)
    {
        var value = await repository.GetByIdAsync(request.Id);

        if (value == null)
            throw new BadRequestException("Updated value could not be found");

        mapper.Map(request, value);
        repository.Update(value);

        var result = await unitOfWork.SaveChangesAsync();
        return BaseResult<object>.Success(value, "The value has been successfully created.", result);
    }
}
