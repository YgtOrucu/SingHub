using AutoMapper;
using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Songs.Commands;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForAdminFeatures.Songs.Handlers.WriteProcess;

public class CreateSongCommandHandler(
    IGenericRepository<Song> repository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<CreateSongCommand, BaseResult<object>>
{
    public async Task<BaseResult<object>> Handle(CreateSongCommand request, CancellationToken cancellationToken)
    {
        var song = mapper.Map<Song>(request);

        if (request.SelectedRoleIds.Any() && request.SelectedRoleIds != null)
        {
            foreach (var roleId in request.SelectedRoleIds)
            {
                song.SongAppRoles.Add(new SongAppRole
                {
                    RoleId = roleId,
                });
            }
        }

        await repository.CreateAsync(song);
        var result = await unitOfWork.SaveChangesAsync();

        return BaseResult<object>.Success("The value has been successfully created.", result);
    }
}