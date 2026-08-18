using AutoMapper;
using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Artists.Queries;
using SingHub.Application.Features.ForAdminFeatures.Artists.Result;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForAdminFeatures.Artists.Handlers.ReadProcess;

public class GetArtistQueryHandle(IGenericRepository<Artist> repository, IMapper mapper)
    : IRequestHandler<GetArtistQuery, BaseResult<List<GetArtistQueryResult>>>
{
    public async Task<BaseResult<List<GetArtistQueryResult>>> Handle(GetArtistQuery request, CancellationToken cancellationToken)
    {
        var values = mapper.Map<List<GetArtistQueryResult>>(await repository.GetListAsync());
        return BaseResult<List<GetArtistQueryResult>>.Success(values);
    }
}
