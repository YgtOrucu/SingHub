using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignHub.Application.Contract.Persistence;
using SignHub.Domain.Entities;
using SignHub.Persistence.Concreate;
using SignHub.Persistence.Context;
using SignHub.Persistence.IdentityErrors;

namespace SignHub.Persistence.Extensions;

public static class ServiceRegistration
{
    public static void AppPersistenceSetting(this IServiceCollection services, IConfiguration builder)
    {
        //services.AddScoped<AuditDbContextInterceptors>();

        services.AddDbContext<SignHubContext>((serviceProvider, options) =>
        {
            options.UseSqlServer(builder.GetConnectionString("DefaultConnection"));
        });

        services.AddIdentity<AppUser, AppRole>(opt =>
        {
            opt.User.RequireUniqueEmail = true;
            opt.Password.RequireNonAlphanumeric = false;
        })
        .AddEntityFrameworkStores<SignHubContext>()
        .AddErrorDescriber<TurkishIdentityError>();


        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    }
}
