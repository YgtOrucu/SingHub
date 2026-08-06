namespace SignHub.Application.Contract.Persistence;
public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync();
}
