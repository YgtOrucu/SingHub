using AutoMapper;
using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Exceptions;
using SingHub.Application.Features.ForAdminFeatures.Genres.Commands;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForAdminFeatures.Genres.Handlers.WriteProcess;

public class RemoveGerneCommandHandle(IGenericRepository<Genre> repository, IMapper mapper, IUnitOfWork unitOfWork)
    : IRequestHandler<RemoveGenreCommand, BaseResult<object>>
{
    public async Task<BaseResult<object>> Handle(RemoveGenreCommand request, CancellationToken cancellationToken)
    {
        var value = await repository.GetByIdAsync(request.Id);

        if (value == null)
            throw new BadRequestException("Updated value could not be found");

        repository.Delete(value);
        var result = await unitOfWork.SaveChangesAsync();

        return BaseResult<object>.Success("The value has been successfully deleted.", result);
    }
}
