using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SingHub.Application.Contract.Persistence;
using SingHub.Domain.Entities;
using SingHub.Persistence.Concrete;
using SingHub.Persistence.Context;
using SingHub.Persistence.IdentityErrors;
using SingHub.Persistence.Interceptors;
using SingHub.Persistence.Seeders;

namespace SingHub.Persistence.Extensions;

public static class ServiceRegistration
{
    public static void AppPersistenceSetting(this IServiceCollection services, IConfiguration builder)
    {
        services.AddScoped<AuditDbContextInterceptors>();

        services.AddDbContext<SingHubContext>((serviceProvider, options) =>
        {
            options.UseSqlServer(builder.GetConnectionString("DefaultConnection"));
            var interceptor = serviceProvider.GetRequiredService<AuditDbContextInterceptors>();
            options.AddInterceptors(interceptor);
            //options.UseLazyLoadingProxies();
        });

        services.AddIdentity<AppUser, AppRole>(opt =>
        {
            opt.User.RequireUniqueEmail = true;
            opt.Password.RequireNonAlphanumeric = false;
        })
        .AddEntityFrameworkStores<SingHubContext>()
        .AddDefaultTokenProviders()
        .AddErrorDescriber<TurkishIdentityError>();


        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<IAlbumService, AlbumRepository>();
        services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    }

    public static async Task UseDbSeederAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var service = scope.ServiceProvider;
        await RoleSeeder.SeedRolesAndAdminUserAsync(service);
    }
}
