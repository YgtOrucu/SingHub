using AutoMapper;
using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Artists.Queries;
using SingHub.Application.Features.ForAdminFeatures.Artists.Result;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForAdminFeatures.Artists.Handlers.ReadProcess;

public class GetArtistQueryHandle(IArtistService repository, IMapper mapper)
    : IRequestHandler<GetArtistQuery, BaseResult<List<GetArtistQueryResult>>>
{
    public async Task<BaseResult<List<GetArtistQueryResult>>> Handle(GetArtistQuery request, CancellationToken cancellationToken)
    {
        var values = await repository.GetArtistWithSongAndAlbumCount();
        return BaseResult<List<GetArtistQueryResult>>.Success(values);
    }
}
