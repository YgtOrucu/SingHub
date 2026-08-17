using AutoMapper;
using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Genres.Queries;
using SingHub.Application.Features.ForAdminFeatures.Genres.Result;
using SingHub.Domain.Entities;

namespace SingHub.Application.Features.ForAdminFeatures.Genres.Handlers.ReadProcess;

public class GetGenreByIdQueryHandle(IGenericRepository<Genre> repository, IMapper mapper)
    : IRequestHandler<GetGenreByIdQuery, BaseResult<GetGenreByIdQueryResult>>
{
    public async Task<BaseResult<GetGenreByIdQueryResult>> Handle(GetGenreByIdQuery request, CancellationToken cancellationToken)
    {
        var value = mapper.Map<GetGenreByIdQueryResult>(await repository.GetByIdAsync(request.Id));
        return BaseResult<GetGenreByIdQueryResult>.Success(value);
    }
}
