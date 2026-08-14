using AutoMapper;
using MediatR;
using SingHub.Application.Bases;
using SingHub.Application.Contract.Persistence;
using SingHub.Application.Features.ForAdminFeatures.Users.Queries;
using SingHub.Application.Features.ForAdminFeatures.Users.Result;

namespace SingHub.Application.Features.ForAdminFeatures.Users.Handlers.ReadProcess;

public class GetAllUsersQueryHandle(IUserRepository repository, IMapper mapper)
    : IRequestHandler<GetAllUsersQuery, BaseResult<List<GetAllUsersQueryResult>>>
{
    public async Task<BaseResult<List<GetAllUsersQueryResult>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await repository.GetAllUsersAsync();

        if (users.Count == 0)
        {
            return BaseResult<List<GetAllUsersQueryResult>>.Failure("Users could not be found.");
        }

        return BaseResult<List<GetAllUsersQueryResult>>.Success(users);
    }             
}
