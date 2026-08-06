using SignHub.Application.Contract.Persistence;
using SignHub.Persistence.Context;

namespace SignHub.Persistence.Concreate;

public class UnitOfWork(SignHubContext context) : IUnitOfWork
{
    public async Task<bool> SaveChangesAsync()
    {
        return await context.SaveChangesAsync() > 0;
    }
}
