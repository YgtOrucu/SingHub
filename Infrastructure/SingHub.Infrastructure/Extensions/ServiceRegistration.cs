using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using SingHub.Application.Contract.Persistence;
using SingHub.Infrastructure.MailSetting;
using SingHub.Infrastructure.Options;
using SingHub.Infrastructure.Concrete;
using System.Text;

namespace SingHub.Infrastructure.Extensions;

public static class ServiceRegistration
{
    public static void AppInfrastructureSetting(this IServiceCollection services, IConfiguration builder)
    {
        services.AddAuthentication(options =>
        {
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

        }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
        {
            var jwttokenpotions = builder.GetSection(nameof(JwtTokenOptions)).Get<JwtTokenOptions>();

            opt.TokenValidationParameters = new()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ValidIssuer = jwttokenpotions.Issuer,
                ValidAudience = jwttokenpotions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwttokenpotions.Key)),
                ClockSkew = TimeSpan.Zero,
            };
        });

        services.Configure<JwtTokenOptions>(builder.GetSection(nameof(JwtTokenOptions)));
        services.Configure<MailSettings>(builder.GetSection(nameof(MailSettings)));

        services.AddScoped<IJwtService, JwtService>();
        services.AddScoped<IMailService, MailService>();
    }
}
