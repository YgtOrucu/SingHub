using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Songs.Commands;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForAdminFeatures.Songs.Handlers.WriteProcess;

public class RemoveSongCommandHandler(
    IGenericRepository<Song> repository,
    IUnitOfWork unitOfWork) : IRequestHandler<RemoveSongCommand, BaseResult<object>>
{
    public async Task<BaseResult<object>> Handle(RemoveSongCommand request, CancellationToken cancellationToken)
    {
        var song = await repository.GetByIdAsync(request.Id);
        if (song == null)
            return BaseResult<object>.Failure("Song not found.");

        repository.Delete(song);
        var result = await unitOfWork.SaveChangesAsync();

        return BaseResult<object>.Success("The value has been successfully deleted.", result);
    }
}