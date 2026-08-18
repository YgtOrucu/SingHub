using AutoMapper;
using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Artists.Queries;
using SingHub.Application.Features.ForAdminFeatures.Artists.Result;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForAdminFeatures.Artists.Handlers.ReadProcess;

public class GetArtistByIdQueryHandle(IGenericRepository<Artist> repository, IMapper mapper)
    : IRequestHandler<GetArtistByIdQuery, BaseResult<GetArtistByIdQueryResult>>
{
    public async Task<BaseResult<GetArtistByIdQueryResult>> Handle(GetArtistByIdQuery request, CancellationToken cancellationToken)
    {
        var value = mapper.Map<GetArtistByIdQueryResult>(await repository.GetByIdAsync(request.Id));
        return BaseResult<GetArtistByIdQueryResult>.Success(value);
    }
}