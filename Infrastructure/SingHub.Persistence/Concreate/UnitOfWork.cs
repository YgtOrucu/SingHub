using SingHub.Application.Contract.Persistence;
using SingHub.Persistence.Context;

namespace SingHub.Persistence.Concreate;

public class UnitOfWork(SingHubContext context) : IUnitOfWork
{
    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
