using AutoMapper;
using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Songs.Commands;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForAdminFeatures.Songs.Handlers.WriteProcess;

public class UpdateSongCommandHandler(
    IGenericRepository<Song> repository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateSongCommand, BaseResult<object>>
{
    public async Task<BaseResult<object>> Handle(UpdateSongCommand request, CancellationToken cancellationToken)
    {
        var song = mapper.Map<Song>(request);
        repository.Update(song);
        var result = await unitOfWork.SaveChangesAsync();

        return BaseResult<object>.Success(song, "The value has been successfully updated.", result);
    }
}
