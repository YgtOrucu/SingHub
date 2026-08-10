namespace SingHub.Application.Contract.Persistence;
public interface IUnitOfWork
{
    Task<bool> SaveChangesAsync();
}
