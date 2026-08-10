using FluentValidation;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using SingHub.Application.Behavior;
using SingHub.Application.MailSetting;
using SingHub.Application.Options;
using System.Reflection;

namespace SingHub.Application.Extensions;

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

        services.Configure<JwtTokenOptions>(configuration.GetSection(nameof(JwtTokenOptions)));
        services.Configure<MailSettings>(configuration.GetSection(nameof(MailSettings)));
    }
}
