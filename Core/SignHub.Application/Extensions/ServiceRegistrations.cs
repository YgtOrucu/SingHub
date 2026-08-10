using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SignHub.Application.Behavior;
using System.Reflection;

namespace SignHub.Application.Extensions;

public static class ServiceRegistrations
{
    public static void AppApplicationSetting(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddAutoMapper(opt => opt.AddMaps(Assembly.GetExecutingAssembly()));

        services.AddMediatR(config =>
        {
            config.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly());
            config.AddOpenBehavior(typeof(ValidationBehavior<,>));
        });
        services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());
    }
}
