using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using SingHub.Domain.Bases;

namespace SingHub.Persistence.Interceptors;

public class AuditDbContextInterceptors : SaveChangesInterceptor
{
    private static readonly Dictionary<EntityState, Action<DbContext, AuditableEntity?>> Behaviors = new()
    {
        { EntityState.Added, AddedBehavior },
        { EntityState.Modified, ModifiedBehavior },
        { EntityState.Deleted, DeletedBehavior }
    };

    private static void AddedBehavior(DbContext context, AuditableEntity auditableEntity)
    {
        auditableEntity.CreatedDate = DateTime.Now;
        auditableEntity.IsDeleted = false;

        context.Entry(auditableEntity).Property(x => x.UpdatedDate).IsModified = false;
        context.Entry(auditableEntity).Property(x => x.DeletedDate).IsModified = false;
    }

    private static void ModifiedBehavior(DbContext context, AuditableEntity auditableEntity)
    {
        auditableEntity.UpdatedDate = DateTime.Now;

        context.Entry(auditableEntity).Property(x => x.CreatedDate).IsModified = false;
        context.Entry(auditableEntity).Property(x => x.DeletedDate).IsModified = false;
        context.Entry(auditableEntity).Property(x => x.IsDeleted).IsModified = false;
    }

    private static void DeletedBehavior(DbContext context, AuditableEntity auditableEntity)
    {
        context.Entry(auditableEntity).State = EntityState.Modified;

        auditableEntity.IsDeleted = true;
        auditableEntity.DeletedDate = DateTime.Now;

        context.Entry(auditableEntity).Property(x => x.CreatedDate).IsModified = false;
        context.Entry(auditableEntity).Property(x => x.UpdatedDate).IsModified = false;
    }

    public override ValueTask<InterceptionResult<int>> SavingChangesAsync(DbContextEventData eventData,
        InterceptionResult<int> result, CancellationToken cancellationToken = default)
    {
        var context = eventData.Context;
        if (context == null) return base.SavingChangesAsync(eventData, result, cancellationToken);

        var auditEntries = context.ChangeTracker.Entries<AuditableEntity>();

        foreach (var entry in auditEntries)
        {
            if (Behaviors.TryGetValue(entry.State, out var behavior))
            {
                behavior(context, entry.Entity);
            }
        }
        return base.SavingChangesAsync(eventData, result, cancellationToken);
    }
}
