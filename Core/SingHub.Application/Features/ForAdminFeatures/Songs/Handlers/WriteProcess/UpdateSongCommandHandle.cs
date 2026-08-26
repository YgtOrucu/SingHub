using AutoMapper;
using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Exceptions;
using SingHub.Application.Features.ForAdminFeatures.Songs.Commands;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForAdminFeatures.Songs.Handlers.WriteProcess;

public class UpdateSongCommandHandler(
    ISongService songService,
    IGenericRepository<Song> repository,
    IMapper mapper,
    IUnitOfWork unitOfWork) : IRequestHandler<UpdateSongCommand, BaseResult<object>>
{
    public async Task<BaseResult<object>> Handle(UpdateSongCommand request, CancellationToken cancellationToken)
    {
        var value = await songService.GetSongWithRolesByIdAsync(request.Id);


        if (value == null)
            throw new BadRequestException("Updated value could not be found");

        mapper.Map(request, value);

        value.SongAppRoles ??= new List<SongAppRole>();
        value.SongAppRoles.Clear();

        if (request.SelectedRoleIds != null && request.SelectedRoleIds.Any())
        {
            foreach (var roleId in request.SelectedRoleIds)
            {
                value.SongAppRoles.Add(new SongAppRole
                {
                    SongId = value.Id,
                    RoleId = roleId
                });
            }
        }


        repository.Update(value);
        var result = await unitOfWork.SaveChangesAsync();

        return BaseResult<object>.Success("The value has been successfully updated.", result);
    }
}
