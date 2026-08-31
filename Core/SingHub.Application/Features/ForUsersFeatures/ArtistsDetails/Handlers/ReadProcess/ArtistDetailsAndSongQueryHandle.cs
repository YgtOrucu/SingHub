using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForUsersFeatures.ArtistsDetails.Queries;
using SingHub.Application.Features.ForUsersFeatures.ArtistsDetails.Result;

namespace SingHub.Application.Features.ForUsersFeatures.ArtistsDetails.Handlers.ReadProcess;

public class ArtistDetailsAndSongQueryHandle(IArtistService service)
    : IRequestHandler<ArtistDetailsAndSongQuery, BaseResult<ArtistDetailsAndSongQueryResult>>
{
    public async Task<BaseResult<ArtistDetailsAndSongQueryResult>> Handle(ArtistDetailsAndSongQuery request, CancellationToken cancellationToken)
    {
        var value = await service.GetArtistDetailsAndSongAsync(request.Id);
        return BaseResult<ArtistDetailsAndSongQueryResult>.Success(value);
    }
}
