using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignHub.Persistence.Context;

namespace SignHub.Persistence.Extensions;

public static class ServiceRegistration
{
    public static void AppPersistenceSetting(this IServiceCollection services, IConfiguration builder)
    {
        services.AddDbContext<SignHubContext>((serviceProvider, options) =>
        {
            options.UseSqlServer(builder.GetConnectionString("DefaultConnection"));
        });
    }
}
