using SingHub.Application.Features.ForAdminFeatures.Users.Result;

namespace SingHub.Application.Contract.Persistence
{
    public interface IUserRepository
    {
        Task<List<GetAllUsersQueryResult>> GetAllUsersAsync();
    }
}
