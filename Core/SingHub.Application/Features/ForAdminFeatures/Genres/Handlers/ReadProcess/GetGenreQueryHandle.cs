using AutoMapper;
using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Genres.Queries;
using SingHub.Application.Features.ForAdminFeatures.Genres.Result;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForAdminFeatures.Genres.Handlers.ReadProcess;

public class GetGenreQueryHandle(IGenericRepository<Genre> repository, IMapper mapper)
    : IRequestHandler<GetGenreQuery, BaseResult<List<GetGenreQueryResult>>>
{
    public async Task<BaseResult<List<GetGenreQueryResult>>> Handle(GetGenreQuery request, CancellationToken cancellationToken)
    {
        var values =  mapper.Map<List<GetGenreQueryResult>>(await repository.GetListAsync());
        return BaseResult<List<GetGenreQueryResult>>.Success(values); 
    }
}
